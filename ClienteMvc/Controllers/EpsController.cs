using Microsoft.AspNetCore.Mvc;
using ModelosDto;
using ServiciosApi;
using System;
using System.Threading.Tasks;

namespace ClienteMvc.Controllers
{
    public class EpsController : Controller
    {
        private readonly IEpsServicio _epsServicio;

        public EpsController(IEpsServicio epsServicio)
        {
            _epsServicio = epsServicio;
        }

        public async Task<ActionResult> Index()
        {
            return View(await _epsServicio.ListarTodos());
        }

        public async Task<IActionResult> Details(Guid id)
        {
            var result = await _epsServicio.Detalle(id);

            return View(result);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EpsAgregarCommand modelo)
        {
            if (!ModelState.IsValid)
            {
                return View(modelo);
            }

            var _item = await _epsServicio.Crear(modelo);

            if (_item.Exitoso == false)
            {
                return BadRequest(ModelState);
            }

            return RedirectToAction(nameof(Details), new { id = _item.Result });
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            var _item = await _epsServicio.Detalle(id);

            var item = new EpsEditarCommand
            {
                Nombre = _item.Item.Nombre
            };

            return View(item);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, EpsEditarCommand modelo)
        {
            var _item = await _epsServicio.Editar(id, modelo);

            if (_item.Exitoso == false)
            {
                return BadRequest(ModelState);
            }

            return RedirectToAction(nameof(Details), new { id = _item.Result });
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            var _item = await _epsServicio.Detalle(id);

            return View(_item);
        }

        // POST: Eps/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var _item = await _epsServicio.Borrar(id);

            if (_item.Exitoso == false)
            {
                return BadRequest(ModelState);
            }

            return RedirectToAction(nameof(Index));
        }
    }
}