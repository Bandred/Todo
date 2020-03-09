﻿//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.Rendering;
//using Microsoft.EntityFrameworkCore;
//using Modelos;
//using Persistencia;

//namespace ClienteMvc.Controllers
//{
//    public class UsuariosController : Controller
//    {
//        private readonly AppDbContext _context;

//        public UsuariosController(AppDbContext context)
//        {
//            _context = context;
//        }

//        // GET: Usuarios
//        public async Task<IActionResult> Index()
//        {
//            var appDbContext = _context.Usuarios.Include(u => u.Eps);
//            return View(await appDbContext.ToListAsync());
//        }

//        // GET: Usuarios/Details/5
//        public async Task<IActionResult> Details(Guid? id)
//        {
//            if (id == null)
//            {
//                return NotFound();
//            }

//            var usuario = await _context.Usuarios
//                .Include(u => u.Eps)
//                .FirstOrDefaultAsync(m => m.Id == id);
//            if (usuario == null)
//            {
//                return NotFound();
//            }

//            return View(usuario);
//        }

//        // GET: Usuarios/Create
//        public IActionResult Create()
//        {
//            ViewData["EpsId"] = new SelectList(_context.Eps, "Id", "Id");
//            return View();
//        }

//        // POST: Usuarios/Create
//        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
//        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> Create([Bind("Id,RegistradoAt,ActualizadoAt,Nombre,TipoRhId,EpsId")] Usuario usuario)
//        {
//            if (ModelState.IsValid)
//            {
//                usuario.Id = Guid.NewGuid();
//                _context.Add(usuario);
//                await _context.SaveChangesAsync();
//                return RedirectToAction(nameof(Index));
//            }
//            ViewData["EpsId"] = new SelectList(_context.Eps, "Id", "Id", usuario.EpsId);
//            return View(usuario);
//        }

//        // GET: Usuarios/Edit/5
//        public async Task<IActionResult> Edit(Guid? id)
//        {
//            if (id == null)
//            {
//                return NotFound();
//            }

//            var usuario = await _context.Usuarios.FindAsync(id);
//            if (usuario == null)
//            {
//                return NotFound();
//            }
//            ViewData["EpsId"] = new SelectList(_context.Eps, "Id", "Id", usuario.EpsId);
//            return View(usuario);
//        }

//        // POST: Usuarios/Edit/5
//        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
//        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> Edit(Guid id, [Bind("Id,RegistradoAt,ActualizadoAt,Nombre,TipoRhId,EpsId")] Usuario usuario)
//        {
//            if (id != usuario.Id)
//            {
//                return NotFound();
//            }

//            if (ModelState.IsValid)
//            {
//                try
//                {
//                    _context.Update(usuario);
//                    await _context.SaveChangesAsync();
//                }
//                catch (DbUpdateConcurrencyException)
//                {
//                    if (!UsuarioExists(usuario.Id))
//                    {
//                        return NotFound();
//                    }
//                    else
//                    {
//                        throw;
//                    }
//                }
//                return RedirectToAction(nameof(Index));
//            }
//            ViewData["EpsId"] = new SelectList(_context.Eps, "Id", "Id", usuario.EpsId);
//            return View(usuario);
//        }

//        // GET: Usuarios/Delete/5
//        public async Task<IActionResult> Delete(Guid? id)
//        {
//            if (id == null)
//            {
//                return NotFound();
//            }

//            var usuario = await _context.Usuarios
//                .Include(u => u.Eps)
//                .FirstOrDefaultAsync(m => m.Id == id);
//            if (usuario == null)
//            {
//                return NotFound();
//            }

//            return View(usuario);
//        }

//        // POST: Usuarios/Delete/5
//        [HttpPost, ActionName("Delete")]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> DeleteConfirmed(Guid id)
//        {
//            var usuario = await _context.Usuarios.FindAsync(id);
//            _context.Usuarios.Remove(usuario);
//            await _context.SaveChangesAsync();
//            return RedirectToAction(nameof(Index));
//        }

//        private bool UsuarioExists(Guid id)
//        {
//            return _context.Usuarios.Any(e => e.Id == id);
//        }
//    }
//}