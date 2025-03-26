using AspNetCoreHero.ToastNotification.Abstractions;
using Curso.Presentacion.Models;
using Curso.Servicos;
using Curso.Servicos.Interfaces;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using System.Text;

namespace Curso.Presentacion.Controllers
{
    public class LoginController : Controller
    {
        private readonly ILoginService _loginService;
        private readonly IEmailService _emailService;
        public INotyfService _notifyService { get; }
        
        public LoginController(ILoginService loginService, INotyfService notifyService, IEmailService emailService)
        {
            _loginService = loginService;
            _notifyService = notifyService;
            _emailService = emailService;
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

        [HttpPost]
        public async Task<ActionResult> PostLogin(ViewModelLogin Data)
        {
            bool result = await _loginService.PostLogin(Data.Contraseña, Data.Usuario);
            if (result == false)
            {
                _notifyService.Error("Usuario o Contraseña Incorrectos");
                return RedirectToAction("Index");
            }
            var claims = new List<Claim>
            {
            new Claim(ClaimTypes.Name, Data.Usuario),
             //new Claim(ClaimTypes.Role, rol)
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

            string vista= Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "VISTACORREO.html");
            string content= System.IO.File.ReadAllText(vista, Encoding.UTF8);


            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal);
            _emailService.SendEmail(Data.Usuario, "Prueba", content);
            _notifyService.Success("Bienvenido a la plataforma");
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Salir()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Login");
        }

    }
}
