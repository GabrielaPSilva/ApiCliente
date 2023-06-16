using Microsoft.AspNetCore.Mvc;

namespace ApiCliente.Controllers
{
    public class ClienteController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
