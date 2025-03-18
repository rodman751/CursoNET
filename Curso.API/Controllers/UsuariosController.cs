using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Curso.Entidades;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;

namespace Curso.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : Controller
    {
        private readonly DbContext _context;

        public UsuariosController(DbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var usuarios = await _context.Usuarios.ToListAsync();
            return Ok(usuarios);
        }

        //// GET: Usuarios/Details/5
        //public async Task<IActionResult> Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var usuarios = await _context.Usuarios
        //        .FirstOrDefaultAsync(m => m.UsuarioID == id);
        //    if (usuarios == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(usuarios);
        //}

        //// GET: Usuarios/Create
        //public IActionResult Create()
        //{
        //    return View();
        //}

        // POST: Usuarios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public async Task<IActionResult> Create( Usuarios usuarios)
        {
            if (ModelState.IsValid)
            {
                _context.Add(usuarios);
                await _context.SaveChangesAsync();
                return Ok(new { Message = " Se creo el Usuario", usuario = usuarios });
            }
            return BadRequest(new {Message = "error al crear el usuario"});
        }

        //// GET: Usuarios/Edit/5
        //public async Task<IActionResult> Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var usuarios = await _context.Usuarios.FindAsync(id);
        //    if (usuarios == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(usuarios);
        //}

        //// POST: Usuarios/Edit/5
        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        
        //public async Task<IActionResult> Edit(int id, [Bind("UsuarioID,Nombre,Email,ConstraseñaHash,FechaRegistro")] Usuarios usuarios)
        //{
        //    if (id != usuarios.UsuarioID)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(usuarios);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!UsuariosExists(usuarios.UsuarioID))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(usuarios);
        //}

        //// GET: Usuarios/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var usuarios = await _context.Usuarios
        //        .FirstOrDefaultAsync(m => m.UsuarioID == id);
        //    if (usuarios == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(usuarios);
        //}

        //// POST: Usuarios/Delete/5
        //[HttpPost, ActionName("Delete")]
        
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    var usuarios = await _context.Usuarios.FindAsync(id);
        //    if (usuarios != null)
        //    {
        //        _context.Usuarios.Remove(usuarios);
        //    }

        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        //private bool UsuariosExists(int id)
        //{
        //    return _context.Usuarios.Any(e => e.UsuarioID == id);
        //}
    }
}
