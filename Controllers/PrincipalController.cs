using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using AgendaOnline.Datos;

namespace AgendaOnline.Controllers
{
    public class PrincipalController : Controller
    {
        string usuarioNombre = "";
        string idUsuario = "";
        public IActionResult Principal()
        {
            ClaimsPrincipal claimsUser = HttpContext.User;
            if (claimsUser.Identity.IsAuthenticated)
            {
                usuarioNombre = claimsUser.Claims.Where(c => c.Type == ClaimTypes.Name)
                    .Select(c => c.Value).SingleOrDefault();
                idUsuario = claimsUser.Claims.Where(c => c.Type == ClaimTypes.Actor)
                    .Select(c => c.Value).SingleOrDefault();
            }
            ViewData["Mensaje"] = usuarioNombre;
            //En el return le enviamos el parametro del id para activar nuestra lista
            if(usuarioNombre == "")
            {
                return View();
            }
            else
            {
                return View(NotasDatos.GetList(Convert.ToInt32(idUsuario)));
            }
        }
        [HttpPost]
        public IActionResult BorrarNota(int idNota)
        {
            bool notaEliminada = NotasDatos.EliminarNota(idNota);
            if (notaEliminada)
            {
                return RedirectToAction("Principal");
            }
            else
            {
                return RedirectToAction("Principal");
            }
        }
        public IActionResult AgregarNota()
        {
            ViewData["Mensaje"] = usuarioNombre;
            //En el return le enviamos el parametro del id para activar nuestra lista
            return View();
        }
        /*
        [HttpPost]
        public IActionResult AgregarNota(string nota, string indice, int idUsuario)
        {

        }*/
    }
}
