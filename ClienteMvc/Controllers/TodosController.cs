using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ModelosDto;
using ServiciosApi;
using System;
using System.Threading.Tasks;

namespace ClienteMvc.Controllers
{
    public class TodosController : Controller
    {
        private readonly ITodoServicio _todoServicio;
        private readonly IUsuarioServicio _usuarioServicio;


        public TodosController(
            ITodoServicio todoServicio,
            IUsuarioServicio usuarioServicio)
        {
            _todoServicio = todoServicio;
            _usuarioServicio = usuarioServicio;
        }

        public async Task<ActionResult> Index()
        {
            return View(await _todoServicio.ListarTodos());
        }

        public async Task<IActionResult> Details(Guid id)
        {
            var result = await _todoServicio.Detalle(id);

            return View(result);
        }

        public async Task<ActionResult> Create()
        {
            var usuarioList = await _usuarioServicio.ListarTodos();

            ViewData["UsuarioId"] = new SelectList(usuarioList.Items, "Id", "Nombre");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TodoAgregarCommand modelo)
        {
            if (!ModelState.IsValid)
            {
                return View(modelo);
            }

            var _item = await _todoServicio.Crear(modelo);

            if (_item.Exitoso == false)
            {
                return BadRequest(ModelState);
            }

            return RedirectToAction(nameof(Details), new { id = _item.Result });
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            var _item = await _todoServicio.Detalle(id);

            var item = new TodoEditarCommand
            {
                Id = _item.Item.Id,
                Nombre = _item.Item.Nombre,
                Activo = _item.Item.Activo,
                UsuarioId = _item.Item.UsuarioId,
            };

            var usuarioList = await _usuarioServicio.ListarTodos();

            ViewData["UsuarioId"] = new SelectList(usuarioList.Items, "Id", "Nombre", item.UsuarioId);

            return View(item);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, TodoEditarCommand modelo)
        {
            var _item = await _todoServicio.Editar(id, modelo);

            if (_item.Exitoso == false)
            {
                return BadRequest(ModelState);
            }

            return RedirectToAction(nameof(Details), new { id = _item.Result });
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            var _item = await _todoServicio.Detalle(id);

            return View(_item);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var _item = await _todoServicio.Borrar(id);

            if (_item.Exitoso == false)
            {
                return BadRequest(ModelState);
            }

            return RedirectToAction(nameof(Index));
        }
    }
}