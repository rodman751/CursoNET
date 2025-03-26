using Curso.ConsumeAPI;
using Curso.Entidades;
using Curso.Presentacion.Models;
using Curso.Servicos.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Curso.Presentacion.Controllers
{
    public class ListasTareasController : Controller
    {
        private readonly IAPIService _aPIService;
        private readonly Data.DbContext _dbContext;
        public ListasTareasController(IAPIService aPIService, Data.DbContext dbContext)
        {
            _aPIService = aPIService;
            _dbContext = dbContext;
        }
        // GET: ListasTareasController
        public async Task<ActionResult> Index()
        {
            string userNameClaim = User.FindFirst(ClaimTypes.Name).Value;
            var usuario = await _dbContext.Usuarios.FirstOrDefaultAsync(u => u.Email == userNameClaim);
            
            int id = usuario.UsuarioID;
            var data = CRUD<ListaTareas>.Read(_aPIService.GetApiUrl() + "/ListaTareas/ByID"+id);
            return View(data);
        }

        // GET: ListasTareasController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ListasTareasController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ListasTareasController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ListasTareasController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ListasTareasController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ListasTareasController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ListasTareasController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
