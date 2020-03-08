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
        Task<RespuestaAux<UsuarioViewModel>> Detalle(Guid id);
        Task<RespuestaAux<Guid>> Editar(Guid id, UsuarioEditarCommand modeloCommand);
        Task<RespuestaAux<UsuarioListViewModel>> ListarTodos();
    }

    class UsuarioServicio : IUsuarioServicio
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
                var _item = await _context.Todos
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
                    await _context.AddAsync(_item);
                    await _context.SaveChangesAsync();

                    result.Result = _item.Result.Id;
                    result.Exitoso = true;
                }

                result.Exitoso = _item.Exitoso;
                result.Mensaje = _item.Mensaje;
            }
            catch (Exception e)
            {
                result.Exitoso = false;
                result.Mensaje = e.Message;
            }

            return result;
        }

        public async Task<RespuestaAux<UsuarioViewModel>> Detalle(Guid id)
        {
            var result = new RespuestaAux<UsuarioViewModel>();

            try
            {
                var _item = await _context.Usuarios
                    .Include(x => x.Eps)
                    .FirstOrDefaultAsync(x => x.Id == id);

                result.Exitoso = true;
                result.Result.Item = UsuarioDto.ProyectarDto(_item);
                result.Result.HabilitarEditar = true;
                result.Result.HabilitarBorrar = true;
            }
            catch (Exception e)
            {
                result.Exitoso = false;
                result.Mensaje = e.Message;
            }

            return result;
        }

        public async Task<RespuestaAux<Guid>> Editar(Guid id, UsuarioEditarCommand modeloCommand)
        {
            var result = new RespuestaAux<Guid>();

            try
            {
                var _itemOriginal = await _context.Usuarios
                        .FirstOrDefaultAsync(x => x.Id == id);

                var _item = Usuario.EditarUsuario(
                    id: modeloCommand.Id,
                    registradoAt: modeloCommand.RegistradoAt,
                    nombre: modeloCommand.Nombre,
                    tipoRhId: modeloCommand.TipoRhId,
                    epsId: modeloCommand.EpsId);

                if (_item.Exitoso == true)
                {
                    _context.Update(_item);
                    await _context.SaveChangesAsync();

                    result.Result = _item.Result.Id;
                    result.Exitoso = true;
                }

                result.Exitoso = _item.Exitoso;
                result.Mensaje = _item.Mensaje;
            }
            catch (Exception e)
            {
                result.Exitoso = false;
                result.Mensaje = e.Message;
            }

            return result;
        }

        public async Task<RespuestaAux<UsuarioListViewModel>> ListarTodos()
        {
            var result = new RespuestaAux<UsuarioListViewModel>();

            try
            {
                var _items = await _context.Usuarios
                    .Include(x => x.Eps)
                    .OrderBy(x => x.RegistradoAt)
                    .ToListAsync();

                var viewModelDto = _items
                    .Select(UsuarioDto.ProyectarDto())
                    .ToList();

                result.Exitoso = true;
                result.Result.Items = viewModelDto;
                result.Result.HabilitarCrear = true;
            }
            catch (Exception e)
            {
                result.Exitoso = false;
                result.Mensaje = e.Message;
            }

            return result;
        }
    }
}
