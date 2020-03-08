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
    public interface IEpsServicio
    {
        Task<RespuestaAux> Borrar(Guid id);
        Task<RespuestaAux<Guid>> Crear(EpsAgregarCommand modeloCommand);
        Task<RespuestaAux<EpsViewModel>> Detalle(Guid id);
        Task<RespuestaAux<Guid>> Editar(Guid id, EpsEditarCommand modeloCommand);
        Task<RespuestaAux<EpsListViewModel>> ListarTodos();
    }

    public class EpsServicio : IEpsServicio
    {
        private readonly AppDbContext _context;

        public EpsServicio(AppDbContext context)
        {
            _context = context;
        }

        public async Task<RespuestaAux> Borrar(Guid id)
        {
            var result = new RespuestaAux();

            try
            {
                var _item = await _context.Eps
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

        public async Task<RespuestaAux<Guid>> Crear(EpsAgregarCommand modeloCommand)
        {
            var result = new RespuestaAux<Guid>();

            try
            {
                var _item = Eps.AgregarEps(nombre: modeloCommand.Nombre);

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

        public async Task<RespuestaAux<EpsViewModel>> Detalle(Guid id)
        {
            var result = new RespuestaAux<EpsViewModel>();

            try
            {
                var _item = await _context.Eps
                    .FirstOrDefaultAsync(x => x.Id == id);

                result.Exitoso = true;
                result.Result.Item = EpsDto.ProyectarDto(_item);
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

        public async Task<RespuestaAux<Guid>> Editar(Guid id, EpsEditarCommand modeloCommand)
        {
            var result = new RespuestaAux<Guid>();

            try
            {
                var _itemOriginal = await _context.Eps
                        .FirstOrDefaultAsync(x => x.Id == id);

                var _item = Eps.EditarEps(
                    id: _itemOriginal.Id,
                    registradoAt: _itemOriginal.RegistradoAt,
                    nombre: modeloCommand.Nombre);

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

        public async Task<RespuestaAux<EpsListViewModel>> ListarTodos()
        {
            var result = new RespuestaAux<EpsListViewModel>();

            try
            {
                var _items = await _context.Eps
                    .OrderBy(x => x.RegistradoAt)
                    .ToListAsync();

                var viewModelDto = _items
                    .Select(EpsDto.ProyectarDto())
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
