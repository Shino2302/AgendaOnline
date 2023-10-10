using AgendaOnline.Models;
using AgendaOnline.Datos;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;

namespace AgendaOnline.Controllers
{
    public class LoginRegisterController : Controller
    {
        
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Index(string userName, string pass)
        {
            UsuairosModel userActual = LoginDatos.InicioDeSesion(userName, pass);
            if(userActual.IdUsuario == 0)
            {
                ViewData["Mensaje"] = "El correo o la clave son incorrectos";
                return View();
            }
            List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name,userActual.Nombre),
                new Claim(ClaimTypes.Actor,userActual.IdUsuario.ToString())
            };
            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims,CookieAuthenticationDefaults.AuthenticationScheme);
            AuthenticationProperties properties = new AuthenticationProperties()
            {
                AllowRefresh = true,
            };
            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity), properties);
                
            return RedirectToAction("Principal","Principal");
        }
        public IActionResult Registro() 
        {
            return View();
        }
        [HttpPost]
        public IActionResult Registro(UsuairosModel model)
        {
            bool usuarioCreado = LoginDatos.CrearUsuario(model);
            if (usuarioCreado)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }
        public IActionResult Eliminar(int idUsuario) 
        {
            bool usuarioEliminado = LoginDatos.EliminarUsuario(idUsuario);
            if (usuarioEliminado)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Index");
            }
        }
        public async Task<IActionResult> CerrarSesion()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index");
        }
    }
}
