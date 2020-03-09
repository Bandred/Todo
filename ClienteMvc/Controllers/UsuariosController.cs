using Compartida.Compartido;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ModelosDto;
using ServiciosApi;
using System;
using System.Threading.Tasks;

namespace ClienteMvc.Controllers
{
    public class UsuariosController : Controller
    {
        private readonly IUsuarioServicio _usuarioServicio;
        private readonly IEpsServicio _epsServicio;

        public UsuariosController(
            IUsuarioServicio usuarioServicio,
            IEpsServicio epsServicio)
        {
            _usuarioServicio = usuarioServicio;
            _epsServicio = epsServicio;
        }

        public async Task<ActionResult> Index()
        {
            return View(await _usuarioServicio.ListarTodos());
        }

        public async Task<IActionResult> Details(Guid id)
        {
            var result = await _usuarioServicio.Detalle(id);

            return View(result);
        }

        public async Task<ActionResult> Create()
        {
            var epsList = await _epsServicio.ListarTodos();
            var rhList = TipoRhEnum.List();

            ViewData["EpsId"] = new SelectList(epsList.Items, "Id", "Nombre");
            ViewData["TipoRhId"] = new SelectList(rhList, "Id", "Nombre");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(UsuarioAgregarCommand modelo)
        {
            if (!ModelState.IsValid)
            {
                return View(modelo);
            }

            var _item = await _usuarioServicio.Crear(modelo);

            if (_item.Exitoso == false)
            {
                return BadRequest(ModelState);
            }

            return RedirectToAction(nameof(Details), new { id = _item.Result });
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            var _item = await _usuarioServicio.Detalle(id);

            var item = new UsuarioEditarCommand
            {
                Id = _item.Item.Id,
                Nombre = _item.Item.Nombre,
                TipoRhId = _item.Item.TipoRhId,
                EpsId = _item.Item.EpsId,
            };

            var epsList = await _epsServicio.ListarTodos();
            var rhList = TipoRhEnum.List();

            ViewData["EpsId"] = new SelectList(epsList.Items, "Id", "Nombre", item.EpsId);
            ViewData["TipoRhId"] = new SelectList(rhList, "Id", "Nombre", item.TipoRhId);

            return View(item);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, UsuarioEditarCommand modelo)
        {
            var _item = await _usuarioServicio.Editar(id, modelo);

            if (_item.Exitoso == false)
            {
                return BadRequest(ModelState);
            }

            return RedirectToAction(nameof(Details), new { id = _item.Result });
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            var _item = await _usuarioServicio.Detalle(id);

            return View(_item);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var _item = await _usuarioServicio.Borrar(id);

            if (_item.Exitoso == false)
            {
                return BadRequest(ModelState);
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
