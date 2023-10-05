using AgendaOnline.Models;
using AgendaOnline.Datos;
using Microsoft.AspNetCore.Mvc;

namespace AgendaOnline.Controllers
{
    public class LoginRegisterController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Index(UsuairosModel model)
        {
            bool usuarioEncontrado = LoginDatos.InicioDeSesion(model);
            if(usuarioEncontrado)
            {
                return RedirectToAction("Principal");
            }
            else
            {
                return View();
            }
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
    }
}
