using Microsoft.AspNetCore.Mvc;

namespace ApiCliente.Controllers
{
    public class TelefoneClienteController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
