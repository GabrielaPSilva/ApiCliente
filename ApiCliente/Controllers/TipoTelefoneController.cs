using ApiCliente.Business.Services.Interfaces;
using ApiCliente.Model.Entities;
using Microsoft.AspNetCore.Mvc;

namespace ApiCliente.Controllers
{
    [Route("api/tipotelefone")]
    [ApiController]
    public class TipoTelefoneController : Controller
    {
        private readonly ITipoTelefoneService _tipoTelefoneService;

        public TipoTelefoneController(ITipoTelefoneService tipoTelefoneService)
        {
            _tipoTelefoneService = tipoTelefoneService;
        }
    }
}
