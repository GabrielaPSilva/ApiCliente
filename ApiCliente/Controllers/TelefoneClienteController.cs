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

                var links = new List<Link>
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

        [HttpGet("{idTelefone}")]
        public async Task<IActionResult> Retornar(int idTelefone)
        {
            try
            {
                TelefoneClienteModel tipoTelefone = await _telefoneService.Retornar(idTelefone);

                if (tipoTelefone == null)
                {
                    return NotFound(new { erro = "Telefone não encontrado" });
                }

                var links = new List<Link>
                {
                    new Link
                    {
                        Rel = "self",
                        Href = $"/api/telefonecliente/{idTelefone}",
                        Method = "GET"
                    },
                    new Link
                    {
                        Rel = "list",
                        Href = $"/api/telefonecliente",
                        Method = "GET"
                    }
                };

                var retorno = new RecursoModel<TelefoneClienteModel>(tipoTelefone, links);

                return Ok(retorno);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost("{idCliente}")]
        public async Task<IActionResult> Cadastrar(int idCliente, [FromBody] TelefoneClienteModel telefone)
        {
            try
            {
                //if (await _clienteService.Retornar(idCliente) == null)
                //{
                //    return NotFound(new { erro = "Cliente não encontrado" });
                //}

                var tipoTelefone = await _tipoTelefoneService.Retornar(telefone.IdTipoTelefone);

                if (tipoTelefone == null)
                {
                    return NotFound(new { erro = "Tipo de telefone não encontrado" });
                }

                telefone.TipoTelefone = tipoTelefone;
                telefone.IdCliente = idCliente;

                if (!telefone.EhValido(out string mensagemErro))
                {
                    return BadRequest(new { erro = mensagemErro });
                }

                var retornoCadastro = await _telefoneService.Cadastrar(telefone);

                if (retornoCadastro == 2627 || retornoCadastro == 2601)
                {
                    return BadRequest(new { erro = "Telefone já existente na base" });
                }
                else if (retornoCadastro > 0)
                {
                    var links = new List<Link>
                    {
                        new Link
                        {
                            Rel = "self",
                            Href = $"/api/telefonecliente/{telefone.Id}/cliente/{idCliente}",
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

        [HttpPut("{idTelefone}")]
        public async Task<IActionResult> Alterar(int idTelefone, [FromBody] TelefoneClienteModel telefone)
        {
            try
            {
                //if (await _clienteService.Retornar(telefone.IdCliente) == null)
                //{
                //    return NotFound(new { erro = "Cliente não encontrado" });
                //}

                if (await _telefoneService.Retornar(idTelefone) == null)
                {
                    return NotFound(new { erro = "Telefone não encontrado" });
                }

                var tipoTelefone = await _tipoTelefoneService.Retornar(telefone.IdTipoTelefone);

                if (tipoTelefone == null)
                {
                    return NotFound(new { erro = "Tipo de telefone não encontrado" });
                }

                telefone.Id = idTelefone;
                telefone.TipoTelefone = tipoTelefone;

                if (!telefone.EhValido(out string mensagemErro))
                {
                    return BadRequest(new { erro = mensagemErro });
                }

                if (await _telefoneService.Alterar(telefone))
                {
                    var links = new List<Link>
                    {
                        new Link
                        {
                            Rel = "self",
                            Href = $"/api/telefonecliente/{idTelefone}",
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

        [HttpPut("{idTelefone}/cliente/{idCliente}")]
        public async Task<IActionResult> AlterarTelefoneIdCliente(int idCliente, int idTelefone, [FromBody] TelefoneClienteModel telefone)
        {
            try
            {
                //if (await (_clienteService.Retornar(idCliente) == null)
                //{
                //    return NotFound(new { erro = "Pessoa não encontrada" });
                //}

                if (await _telefoneService.Retornar(idTelefone) == null)
                {
                    return NotFound(new { erro = "Telefone não encontrado" });
                }

                var tipoTelefone = await _tipoTelefoneService.Retornar(telefone.IdTipoTelefone);

                if (tipoTelefone == null)
                {
                    return NotFound(new { erro = "Tipo de telefone não encontrado" });
                }

                telefone.Id = idTelefone;
                telefone.IdCliente = idCliente;
                telefone.TipoTelefone = tipoTelefone;

                if (!telefone.EhValido(out string mensagemErro))
                {
                    return BadRequest(new { erro = mensagemErro });
                }

                if (await _telefoneService.Alterar(telefone))
                {
                    var links = new List<Link>
                    {
                        new Link
                        {
                            Rel = "self",
                            Href = $"/api/telefonecliente/{telefone.Id}",
                            Method = "GET"
                        },
                        new Link
                        {
                            Rel = "list",
                            Href = $"/api/tipostelefone",
                            Method = "GET"
                        }
                    };

                    var retorno = new RecursoModel<TipoTelefoneModel>(tipoTelefone, links);

                    return Ok(retorno);
                }

                return BadRequest(new { erro = "Erro ao alterar tipo de telefone" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        //[HttpDelete("{idTipoTelefone}")]
        //public async Task<IActionResult> Desativar(int idTipoTelefone)
        //{
        //    try
        //    {
        //        if (await _tipoTelefoneService.Retornar(idTipoTelefone) == null)
        //        {
        //            return NotFound(new { erro = "Tipo de telefone não encontrado" });
        //        }

        //        if (await _tipoTelefoneService.Desativar(idTipoTelefone))
        //        {
        //            return NoContent();
        //        }

        //        return BadRequest(new { erro = "Erro ao desativar tipo de telefone" });
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        //    }
        //}

    }
}
