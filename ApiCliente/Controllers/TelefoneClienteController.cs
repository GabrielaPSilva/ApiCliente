using ApiCliente.Business.Services;
using ApiCliente.Business.Services.Interfaces;
using ApiCliente.Model.Entities;
using Microsoft.AspNetCore.Mvc;

namespace ApiCliente.Controllers
{
    [Route("api/telefonecliente")]
    [ApiController]
    public class TelefoneClienteController : Controller
    {
        private readonly ITelefoneClienteService _telefoneService;
        private readonly ITipoTelefoneService _tipoTelefoneService;
        private readonly IClienteService _clienteService;

        public TelefoneClienteController(ITelefoneClienteService telefoneService,
                                         ITipoTelefoneService tipoTelefoneService,
                                         IClienteService clienteService)
        {
            _telefoneService = telefoneService;
            _tipoTelefoneService = tipoTelefoneService;
            _clienteService = clienteService;
        }

        [HttpGet]
        public async Task<IActionResult> Listar()
        {
            try
            {
                List<TelefoneClienteModel> listaTelefoneCliente = await _telefoneService.Listar();

                if (listaTelefoneCliente == null)
                {
                    return NotFound(new { erro = "Lista de telefones não encontrada" });
                }

                List<Link> links = new List<Link>
                {
                    new Link
                    {
                        Rel = "self",
                        Href = $"/api/telefonecliente",
                        Method = "GET"
                    }
                };

                listaTelefoneCliente.ForEach(tipoTelefone =>
                {
                    links.Add(new Link
                    {
                        Rel = "related",
                        Href = $"/api/telefonecliente/{tipoTelefone.Id}",
                        Method = "GET"
                    });
                });

                var retorno = new RecursoModel<List<TelefoneClienteModel>>(listaTelefoneCliente, links);

                return Ok(retorno);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("cliente/{idCliente}")]
        public async Task<IActionResult> ListarIdCliente(int idCliente)
        {
            try
            {
                List<TelefoneClienteModel> listaTelefoneCliente = await _telefoneService.ListarTelefonesCliente(idCliente);

                if (listaTelefoneCliente == null)
                {
                    return NotFound(new { erro = "Lista de telefones não encontrada" });
                }

                List<Link> links = new List<Link>
                {
                    new Link
                    {
                        Rel = "self",
                        Href = $"/api/telefonecliente/cliente/{idCliente}",
                        Method = "GET"
                    },
                    new Link
                    {
                        Rel = "list",
                        Href = $"/api/telefonecliente",
                        Method = "GET"
                    }
                };

                listaTelefoneCliente.ForEach(tipoTelefone =>
                {
                    links.Add(new Link
                    {
                        Rel = "related",
                        Href = $"/api/telefonecliente/{tipoTelefone.Id}",
                        Method = "GET"
                    });
                });

                var retorno = new RecursoModel<List<TelefoneClienteModel>>(listaTelefoneCliente, links);

                return Ok(retorno);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("cliente/{idCliente}/telefone/{idTelefone}")]
        public async Task<IActionResult> RetornarTelefones(int idTelefone, int idCliente)
        {
            try
            {
                TelefoneClienteModel telefone = await _telefoneService.RetornarTelefoneCliente(idTelefone, idCliente);

                if (telefone == null)
                {
                    return NotFound(new { erro = "Telefone não encontrado" });
                }

                List<Link> links = new List<Link>
                {
                    new Link
                    {
                        Rel = "self",
                        Href = $"/api/telefonecliente/cliente/{idCliente}/telefone/{idTelefone}",
                        Method = "GET"
                    },
                    new Link
                    {
                        Rel = "list",
                        Href = $"/api/telefonecliente/cliente/{idCliente}",
                        Method = "GET"
                    },
                    new Link
                    {
                        Rel = "list",
                        Href = $"/api/telefonecliente",
                        Method = "GET"
                    }
                };

                var retorno = new RecursoModel<TelefoneClienteModel>(telefone, links);

                return Ok(retorno);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost("cliente/{idCliente}")]
        public async Task<IActionResult> Cadastrar(int idCliente, [FromBody] TelefoneClienteModel telefone)
        {
            try
            {
                if (await _clienteService.Retornar(idCliente) == null)
                {
                    return NotFound(new { erro = "Cliente não encontrado" });
                }

                TipoTelefoneModel tipoTelefone = await _tipoTelefoneService.Retornar(telefone.IdTipoTelefone);

                if (tipoTelefone == null)
                {
                    return NotFound(new { erro = "Tipo de telefone não encontrado" });
                }

                telefone.TipoTelefone = tipoTelefone;
                telefone.IdCliente = idCliente;

                if (!telefone.IsValid(out string mensagemErro))
                {
                    return BadRequest(new { erro = mensagemErro });
                }

                int retornoCadastro = await _telefoneService.Cadastrar(telefone);

                if (retornoCadastro > 0)
                {
                    List<Link> links = new List<Link>
                    {
                        new Link
                        {
                            Rel = "self",
                            Href = $"/api/telefonecliente/cliente/{idCliente}/telefone/{telefone.Id}",
                            Method = "GET"
                        },
                        new Link
                        {
                            Rel = "list",
                            Href = $"/api/telefonecliente/cliente/{idCliente}",
                            Method = "GET"
                        },
                        new Link
                        {
                            Rel = "list",
                            Href = $"/api/telefonecliente",
                            Method = "GET"
                        }
                    };

                    var retorno = new RecursoModel<TelefoneClienteModel>(telefone, links);

                    return Created($"/api/telefonecliente/{telefone.Id}", retorno);
                }

                return BadRequest(new { erro = "Erro ao cadastrar telefone" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPut("cliente/{idCliente}/telefone/{idTelefone}")]
        public async Task<IActionResult> Alterar(int idCliente, int idTelefone, [FromBody] TelefoneClienteModel telefone)
        {
            try
            {
                if (await _clienteService.Retornar(idCliente) == null)
                {
                    return NotFound(new { erro = "Cliente não encontrado" });
                }

                if (await _telefoneService.RetornarTelefoneCliente(idTelefone, idCliente) == null)
                {
                    return NotFound(new { erro = "Telefone não encontrado" });
                }

                TipoTelefoneModel tipoTelefone = await _tipoTelefoneService.Retornar(telefone.IdTipoTelefone);

                if (tipoTelefone == null)
                {
                    return NotFound(new { erro = "Telefone não encontrado" });
                }

                telefone.Id = idTelefone;
                telefone.IdCliente = idCliente;
                telefone.TipoTelefone = tipoTelefone;

                if (!telefone.IsValid(out string mensagemErro))
                {
                    return BadRequest(new { erro = mensagemErro });
                }

                if (await _telefoneService.Alterar(telefone))
                {
                    List<Link> links = new List<Link>
                    {
                        new Link
                        {
                            Rel = "self",
                            Href = $"/api/telefonecliente/cliente/{idCliente}/telefone/{telefone.Id}",
                            Method = "GET"
                        },
                        new Link
                        {
                            Rel = "list",
                            Href = $"/api/telefonecliente/cliente/{idCliente}",
                            Method = "GET"
                        },
                        new Link
                        {
                            Rel = "list",
                            Href = $"/api/telefonecliente",
                            Method = "GET"
                        }
                    };

                    var retorno = new RecursoModel<TelefoneClienteModel>(telefone, links);

                    return Ok(retorno);
                }

                return BadRequest(new { erro = "Erro ao alterar telefone" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpDelete("cliente/{idCliente}/telefone/{idTelefone}")]
        public async Task<IActionResult> Desativar(int idCliente, int idTelefone)
        {
            try
            {
                if (await _clienteService.Retornar(idCliente) == null)
                {
                    return NotFound(new { erro = "Cliente não encontrado" });
                }

                if (await _telefoneService.RetornarTelefoneCliente(idTelefone, idCliente) == null)
                {
                    return NotFound(new { erro = "Telefone não encontrado" });
                }

                if (await _telefoneService.Desativar(idTelefone, idCliente))
                {
                    return NoContent();
                }

                return BadRequest(new { erro = "Erro ao desativar telefone" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPut("cliente/{idCliente}/nometelefone/{telefone}")]
        public async Task<IActionResult> Reativar(int idCliente, string telefone)
        {
            try
            {
                if (await _clienteService.Retornar(idCliente) == null)
                {
                    return NotFound(new { erro = "Cliente não encontrado" });
                }

                List<TelefoneClienteModel> retornoTelefonesCliente = await _telefoneService.ListarTelefonesCliente(idCliente);

                var telefoneCliente = retornoTelefonesCliente.Where(c => c.Telefone == telefone);

                if (telefoneCliente == null)
                {
                    return NotFound(new { erro = "Telefone não encontrado" });
                }

                if (await _telefoneService.Reativar(idCliente, telefone))
                {
                    return NoContent();
                }

                return BadRequest(new { erro = "Erro ao reativar telefone" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }
    }
}
