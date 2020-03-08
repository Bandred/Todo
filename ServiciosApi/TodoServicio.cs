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
    public interface ITodoServicio
    {
        Task<RespuestaAux> Borrar(Guid id);
        Task<RespuestaAux<Guid>> Crear(TodoAgregarCommand modeloCommand);
        Task<RespuestaAux<TodoViewModel>> Detalle(Guid id);
        Task<RespuestaAux<Guid>> Editar(Guid id, TodoEditarCommand modeloCommand);
        Task<RespuestaAux<TodoListViewModel>> ListarTodos();
    }

    class TodoServicio : ITodoServicio
    {
        private readonly AppDbContext _context;

        public TodoServicio(AppDbContext context)
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

        public async Task<RespuestaAux<Guid>> Crear(TodoAgregarCommand modeloCommand)
        {
            var result = new RespuestaAux<Guid>();

            try
            {
                var _item = Todo.AgregarTodo(
                    nombre: modeloCommand.Nombre,
                    usuarioId: modeloCommand.UsuarioId);

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

        public async Task<RespuestaAux<TodoViewModel>> Detalle(Guid id)
        {
            var result = new RespuestaAux<TodoViewModel>();

            try
            {
                var _item = await _context.Todos
                    .Include(x => x.Usuario)
                    .FirstOrDefaultAsync(x => x.Id == id);

                result.Exitoso = true;
                result.Result.Item = TodoDto.ProyectarDto(_item);
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

        public async Task<RespuestaAux<Guid>> Editar(Guid id, TodoEditarCommand modeloCommand)
        {
            var result = new RespuestaAux<Guid>();

            try
            {
                var _itemOriginal = await _context.Todos
                        .FirstOrDefaultAsync(x => x.Id == id);

                var _item = Todo.EditarTodo(
                    id: modeloCommand.Id,
                    registradoAt: modeloCommand.RegistradoAt,
                    nombre: modeloCommand.Nombre,
                    activo: modeloCommand.Activo,
                    usuarioId: modeloCommand.UsuarioId);
                ;

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

        public async Task<RespuestaAux<TodoListViewModel>> ListarTodos()
        {
            var result = new RespuestaAux<TodoListViewModel>();

            try
            {
                var _items = await _context.Todos
                    .Include(x => x.Usuario)
                    .OrderBy(x => x.RegistradoAt)
                    .ToListAsync();

                var viewModelDto = _items
                    .Select(TodoDto.ProyectarDto())
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
