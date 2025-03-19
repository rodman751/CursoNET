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


        [HttpGet("UserByID/{id}")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            var usuarios = await _context.Usuarios
                .Include(l => l.ListasTarea)
                .ThenInclude(t => t.Tareas)
                .Select(item => new
                {
                    item.UsuarioID,
                    item.Nombre,
                    item.Email,
                    item.FechaRegistro,
                    ListaTareas = item.ListasTarea.Select(l => new
                    {
                        l.ListaID,
                        l.Nombre,
                        Tareas = l.Tareas.Select(t => new
                        {
                            t.TareaID,
                            t.Descripcion,
                            t.Estado
                        })
                    })

                })
                .FirstOrDefaultAsync(m => m.UsuarioID == id);
            if (usuarios == null)
            {
                return BadRequest();
            }

            return Ok(usuarios);
        }




        [HttpPost("Crear")]
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


        [HttpPost("Edit/{id}")]
        public async Task<IActionResult> Edit(int id,  Usuarios usuarios)
        {
            if (id != usuarios.UsuarioID)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(usuarios);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UsuariosExists(usuarios.UsuarioID))
                    {
                        return BadRequest();
                    }
                    else
                    {
                        throw;
                    }
                }
                return Ok(new {message="Se edito"});
            }
            return BadRequest();
        }


        //// POST: Usuarios/Delete/5
        [HttpPost("Delete/{id}")]

        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var usuarios = await _context.Usuarios.FindAsync(id);
            if (usuarios != null)
            {
                _context.Usuarios.Remove(usuarios);
            }

            await _context.SaveChangesAsync();
            return Ok(new {message="se borro", usuario=usuarios});
        }

        private bool UsuariosExists(int id)
        {
            return _context.Usuarios.Any(e => e.UsuarioID == id);
        }
    }
}
