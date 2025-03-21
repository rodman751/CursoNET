using Curso.Entidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Curso.Data;
namespace Curso.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ListaTareasController : ControllerBase
    {
        private readonly Data.DbContext _context;

        public ListaTareasController(Data.DbContext context)
        {
            _context = context;
        }

        // GET: api/ListaTareas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ListaTareas>>> GetListasTareas() =>


             await _context.ListasTareas.ToListAsync();


        // GET: api/ListaTareas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ListaTareas>> GetListaTareas(int id)
        {
            var listaTareas = await _context.ListasTareas
                .Include(t => t.Tareas)
                .Where(t => t.ListaID == id)
                .Select(item => new
                {
                    item.ListaID,
                    item.UsuarioID,
                    item.Nombre,
                    Tareas = item.Tareas.Select(t => new
                    {
                        t.TareaID,
                        t.ListaID,
                        t.Descripcion,
                        t.Estado
                    }),
                    Usuario = new
                    {
                        item.Usuario.UsuarioID,
                        item.Usuario.Nombre,
                        item.Usuario.Email
                    }
                })
                .FirstOrDefaultAsync();

            if (listaTareas == null)
            {
                return BadRequest();
            }

            return Ok(listaTareas);
        }

        // PUT: api/ListaTareas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutListaTareas(int id, ListaTareas listaTareas)
        {
            if (id != listaTareas.ListaID)
            {
                return BadRequest();
            }

            _context.Entry(listaTareas).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ListaTareasExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/ListaTareas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ListaTareas>> PostListaTareas(ListaTareas listaTareas)
        {
            _context.ListasTareas.Add(listaTareas);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetListaTareas", new { id = listaTareas.ListaID }, listaTareas);
        }

        // DELETE: api/ListaTareas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteListaTareas(int id)
        {
            var listaTareas = await _context.ListasTareas.FindAsync(id);
            if (listaTareas == null)
            {
                return NotFound();
            }

            _context.ListasTareas.Remove(listaTareas);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ListaTareasExists(int id)
        {
            return _context.ListasTareas.Any(e => e.ListaID == id);
        }
    }
}
