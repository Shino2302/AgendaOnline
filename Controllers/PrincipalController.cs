using Microsoft.AspNetCore.Mvc;

namespace AgendaOnline.Controllers
{
    public class PrincipalController : Controller
    {
        public IActionResult Principal()
        {
            return View();
        }
    }
}
