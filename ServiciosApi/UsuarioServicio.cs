using Compartida.Compartido;
using Microsoft.EntityFrameworkCore;
using Modelos;
using ModelosDto;
using Persistencia;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ServiciosApi
{
    public interface IUsuarioServicio
    {
        Task<RespuestaAux> Borrar(Guid id);
        Task<RespuestaAux<Guid>> Crear(UsuarioAgregarCommand modeloCommand);
        Task<UsuarioViewModel> Detalle(Guid id);
        Task<RespuestaAux<Guid>> Editar(Guid id, UsuarioEditarCommand modeloCommand);
        Task<UsuarioListViewModel> ListarTodos();
    }

    public class UsuarioServicio : IUsuarioServicio
    {
        private readonly AppDbContext _context;

        public UsuarioServicio(AppDbContext context)
        {
            _context = context;
        }

        public async Task<RespuestaAux> Borrar(Guid id)
        {
            var result = new RespuestaAux();

            try
            {
                var _item = await _context.Usuarios
                     .FirstOrDefaultAsync(x => x.Id == id);

                _context.Remove(_item);

                await _context.SaveChangesAsync();

                result.Exitoso = true;
            }
            catch (Exception e)
            {
                result.Exitoso = false;
                result.Mensaje = e.Message;
            }

            return result;
        }

        public async Task<RespuestaAux<Guid>> Crear(UsuarioAgregarCommand modeloCommand)
        {
            var result = new RespuestaAux<Guid>();

            try
            {
                var _item = Usuario.AgregarUsuario(
                    nombre: modeloCommand.Nombre,
                    tipoRhId: modeloCommand.TipoRhId,
                    epsId: modeloCommand.EpsId);

                if (_item.Exitoso == true)
                {
                    await _context.AddAsync(_item.Result);
                    await _context.SaveChangesAsync();

                    result.Result = _item.Result.Id;
                    result.Exitoso = true;
                }
                else
                {
                    result.Exitoso = _item.Exitoso;
                    result.Mensaje = _item.Mensaje;
                }
            }
            catch (Exception e)
            {
                result.Exitoso = false;
                result.Mensaje = e.Message;
            }

            return result;
        }

        public async Task<UsuarioViewModel> Detalle(Guid id)
        {
            var result = new UsuarioViewModel();

            try
            {
                var _item = await _context.Usuarios
                    .Include(x => x.Eps)
                    .FirstOrDefaultAsync(x => x.Id == id);

                result.Item = UsuarioDto.ProyectarDto(_item);
                result.HabilitarEditar = true;
                result.HabilitarBorrar = true;
            }
            catch (Exception e)
            {

            }

            return result;
        }

        public async Task<RespuestaAux<Guid>> Editar(Guid id, UsuarioEditarCommand modeloCommand)
        {
            var result = new RespuestaAux<Guid>();

            try
            {
                var _item = Usuario.EditarUsuario(
                    id: modeloCommand.Id,
                    nombre: modeloCommand.Nombre,
                    tipoRhId: modeloCommand.TipoRhId,
                    epsId: modeloCommand.EpsId);


                if (_item.Exitoso == true)
                {
                    _context.Usuarios.Update(_item.Result);
                    await _context.SaveChangesAsync();

                    result.Result = _item.Result.Id;
                    result.Exitoso = true;
                }
                else
                {
                    result.Exitoso = _item.Exitoso;
                    result.Mensaje = _item.Mensaje;
                }
            }
            catch (Exception e)
            {
                result.Exitoso = false;
                result.Mensaje = e.Message;
            }

            return result;
        }

        public async Task<UsuarioListViewModel> ListarTodos()
        {
            var result = new UsuarioListViewModel();

            try
            {
                var _items = await _context.Usuarios
                    .Include(x => x.Eps)
                    .OrderBy(x => x.RegistradoAt)
                    .ToListAsync();

                var viewModelDto = _items
                     .Select(UsuarioDto.ProyectarDto())
                     .ToList();

                result.Items = viewModelDto;
                result.HabilitarCrear = true;
            }
            catch (Exception e)
            {

            }

            return result;
        }
    }
}
