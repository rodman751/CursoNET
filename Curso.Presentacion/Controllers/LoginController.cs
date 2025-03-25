using AspNetCoreHero.ToastNotification.Abstractions;
using Curso.Presentacion.Models;
using Curso.Servicos;
using Curso.Servicos.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Curso.Presentacion.Controllers
{
    public class LoginController : Controller
    {
        private readonly ILoginService _loginService;
        public INotyfService _notifyService { get; }
        public LoginController(ILoginService loginService, INotyfService notifyService)
        {
            _loginService = loginService;
            _notifyService = notifyService;
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Register()
        {
            return View();
        }


        [HttpPost]
        public async Task<ActionResult> PostRegister( RegisterViewModel Data)
        {
            bool resul = await _loginService.RegisterService(Data.Usuario, Data.Email, Data.Contraseña, Data.ConfirmarContraseña);
            if (resul == false)
            {
                _notifyService.Error("Usuario Registrado");
                return RedirectToAction("Register");
            }
            _notifyService.Success($"Hola {Data.Usuario}");
            return RedirectToAction("Index", "Home");
        }
        
    }
}
