using ApiCliente.Business.Services;
using ApiCliente.Business.Services.Interfaces;
using ApiCliente.Model.Entities;
using Microsoft.AspNetCore.Mvc;

namespace ApiCliente.Controllers
{
    [Route("api/clientes")]
    [ApiController]
    public class ClienteController : Controller
    {
        private readonly IClienteService _clienteService;
        private readonly ITelefoneClienteService _telefoneClienteService;
        private readonly ITipoTelefoneService _tipoTelefoneService;
        public ClienteController(IClienteService clienteService,
                                 ITelefoneClienteService telefoneClienteService,
                                 ITipoTelefoneService tipoTelefoneService)
        {
            _clienteService = clienteService;
            _telefoneClienteService = telefoneClienteService;
            _tipoTelefoneService = tipoTelefoneService;
        }

        [HttpGet]
        public async Task<IActionResult> Listar()
        {
            try
            {
                List<ClienteModel> listaClientes = await _clienteService.Listar();

                if (listaClientes == null)
                {
                    return NotFound(new { erro = "Lista de clientes não encontrada" });
                }

                List<Link> links = new List<Link>
                {
                    new Link
                    {
                        Rel = "self",
                        Href = $"/api/clientes",
                        Method = "GET"
                    }
                };

                listaClientes.ForEach(cliente =>
                {
                    links.Add(new Link
                    {
                        Rel = "related",
                        Href = $"/api/clientes/{cliente.Id}",
                        Method = "GET"
                    });

                    //links.Add(new Link
                    //{
                    //    Rel = "collection",
                    //    Href = $"/api/clientes/{pessoa.Codigo}/contatos",
                    //    Method = "GET"
                    //});
                });

                var retorno = new RecursoModel<List<ClienteModel>>(listaClientes, links);

                return Ok(retorno);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("telefones")]
        public async Task<IActionResult> ListarClientesTelefones()
        {
            try
            {
                List<ClienteModel> listaClientesTelefones = await _clienteService.ListarClientesTelefones();

                if (listaClientesTelefones == null)
                {
                    return NotFound(new { erro = "Lista de clientes não encontrada" });
                }

                List<Link> links = new List<Link>
                {
                    new Link
                    {
                        Rel = "self",
                        Href = $"/api/clientes/telefones",
                        Method = "GET"
                    },
                    new Link
                    {
                        Rel = "list",
                        Href = $"/api/clientes",
                        Method = "GET"
                    }
                };

                listaClientesTelefones.ForEach(cliente =>
                {
                    links.Add(new Link
                    {
                        Rel = "related",
                        Href = $"/api/clientes/{cliente.Id}",
                        Method = "GET"
                    });

                    //links.Add(new Link
                    //{
                    //    Rel = "collection",
                    //    Href = $"/api/clientes/{pessoa.Codigo}/contatos",
                    //    Method = "GET"
                    //});
                });

                var retorno = new RecursoModel<List<ClienteModel>>(listaClientesTelefones, links);

                return Ok(retorno);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("{idCliente}")]
        public async Task<IActionResult> Retornar(int idCliente)
        {
            try
            {
                ClienteModel cliente = await _clienteService.Retornar(idCliente);

                if (cliente == null)
                {
                    return NotFound(new { erro = "Cliente não encontrado" });
                }

                List<Link> links = new List<Link>
                {
                    new Link
                    {
                        Rel = "self",
                        Href = $"/api/clientes/{idCliente}",
                        Method = "GET"
                    },
                    //new Link
                    //{
                    //    Rel = "collection",
                    //    Href = $"/api/pessoas/{codigoPessoa}/contatos",
                    //    Method = "GET"
                    //},
                    new Link
                    {
                        Rel = "list",
                        Href = $"/api/clientes/telefones",
                        Method = "GET"
                    },
                    new Link
                    {
                        Rel = "list",
                        Href = $"/api/clientes",
                        Method = "GET"
                    }
                };

                var retorno = new RecursoModel<ClienteModel>(cliente, links);

                return Ok(retorno);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("telefone/{telefone}")]
        public async Task<IActionResult> RetornarClienteTelefone(string telefone)
        {
            try
            {
                ClienteModel cliente = await _clienteService.RetornarClienteTelefone(telefone);

                if (cliente == null)
                {
                    return NotFound(new { erro = "Cliente não encontrado" });
                }

                List<Link> links = new List<Link>
                {
                    new Link
                    {
                        Rel = "self",
                        Href = $"/api/clientes/telefone/{telefone}",
                        Method = "GET"
                    },
                    new Link
                    {
                        Rel = "related",
                        Href = $"/api/clientes/{cliente.Id}",
                        Method = "GET"
                    },
                    //new Link
                    //{
                    //    Rel = "collection",
                    //    Href = $"/api/pessoas/{codigoPessoa}/contatos",
                    //    Method = "GET"
                    //},
                    new Link
                    {
                        Rel = "list",
                        Href = $"/api/clientes",
                        Method = "GET"
                    }
                };

                var retorno = new RecursoModel<ClienteModel>(cliente, links);

                return Ok(retorno);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Cadastrar([FromBody] ClienteModel cliente)
        {
            try
            {
                if (!cliente.IsTrue(out string mensagemErro))
                {
                    return BadRequest(new { erro = mensagemErro });
                }

                int retornoCadastroCliente = await _clienteService.Cadastrar(cliente);

                if (retornoCadastroCliente > 0)
                {
                    int idCliente = retornoCadastroCliente;

                    foreach (TelefoneClienteModel telefone in cliente.ListaTelefones!)
                    {
                        TipoTelefoneModel tipoTelefone = await _tipoTelefoneService.Retornar(telefone.IdTipoTelefone);

                        if (tipoTelefone == null)
                        {
                            await _clienteService.Deletar(idCliente);
                            return NotFound(new { erro = "Tipo de telefone não encontrado." });
                        }
                        telefone.IdTipoTelefone = tipoTelefone.Id;
                        telefone.IdCliente = idCliente;

                        int retornoCadastroTelefone = await _telefoneClienteService.Cadastrar(telefone);

                        if (retornoCadastroTelefone > 0)
                        {
                            List<Link> links = new List<Link>
                            {
                                new Link
                                {
                                    Rel = "self",
                                    Href = $"/api/clientes/{cliente.Id}",
                                    Method = "GET"
                                },
                                //new Link
                                //{
                                //    Rel = "collection",
                                //    Href = $"/api/clientes/{pessoa.Codigo}/contatos",
                                //    Method = "GET"
                                //},
                                new Link
                                {
                                    Rel = "list",
                                    Href = $"/api/clientes/telefones",
                                    Method = "GET"
                                },
                                new Link
                                {
                                    Rel = "list",
                                    Href = $"/api/clientes",
                                    Method = "GET"
                                }
                            };

                            var retorno = new RecursoModel<ClienteModel>(cliente, links);

                            return Created($"/api/pessoas/{cliente.Id}", retorno);
                        }

                        return BadRequest(new { erro = "Erro ao cadastrar o telefone do cliente" });
                    }
                }

                return BadRequest(new { erro = "Erro ao cadastrar cliente" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPut("{idCliente}")]
        public async Task<IActionResult> Alterar(int idCliente, [FromBody] ClienteModel cliente)
        {
            try
            {
                if (await _clienteService.Retornar(idCliente) == null)
                {
                    return NotFound(new { erro = "Cliente não encontrado" });
                }

                cliente.Id = idCliente;

                foreach (TelefoneClienteModel telefone in cliente.ListaTelefones!)
                {
                    TipoTelefoneModel tipoTelefone = await _tipoTelefoneService.Retornar(telefone.IdTipoTelefone);

                    if (tipoTelefone == null)
                    {
                        return NotFound(new { erro = "Tipo de telefone não encontrado" });
                    }

                    telefone.TipoTelefone = tipoTelefone;
                    telefone.IdCliente = cliente.Id;
                }

                if (!cliente.IsTrue(out string mensagemErro))
                {
                    return BadRequest(new { erro = mensagemErro });
                }

                if (await _clienteService.Alterar(cliente))
                {
                    List<Link> links = new List<Link>
                    {
                        new Link
                        {
                            Rel = "self",
                            Href = $"/api/clientes/{cliente.Id}",
                            Method = "GET"
                        },
                        //new Link
                        //{
                        //    Rel = "collection",
                        //    Href = $"/api/clientes/{pessoa.Codigo}/contatos",
                        //    Method = "GET"
                        //},
                        new Link
                        {
                            Rel = "list",
                            Href = $"/api/clientes/telefones",
                            Method = "GET"
                        },
                        new Link
                        {
                            Rel = "list",
                            Href = $"/api/clientes",
                            Method = "GET"
                        }
                    };

                    var retorno = new RecursoModel<ClienteModel>(cliente, links);

                    return Ok(retorno);
                }

                return BadRequest(new { erro = "Erro ao alterar cliente" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpDelete("{idCliente}")]
        public async Task<IActionResult> Desativar(int idCliente)
        {
            try
            {
                ClienteModel retornoCliente = await _clienteService.Retornar(idCliente);

                if (retornoCliente == null)
                {
                    return NotFound(new { erro = "Cliente não encontrado" });
                }

                List<TelefoneClienteModel> retornoTelefonesCliente = await _telefoneClienteService.ListarTelefonesCliente(idCliente);

                foreach (TelefoneClienteModel telefone in retornoTelefonesCliente)
                {
                    bool desativarTelefone = await _telefoneClienteService.Desativar(telefone.Id);

                    if (!desativarTelefone)
                    {
                        return BadRequest(new { erro = "Erro durante o processo de exclusão dos telefones que pertencem ao cliente." });
                    }
                }

                if (await _clienteService.Desativar(idCliente))
                {
                    return NoContent();
                }
                
                return BadRequest(new { erro = "Erro ao desativar cliente" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpDelete("email/{email}")]
        public async Task<IActionResult> DesativarClienteEmail(string email)
        {
            try
            {
                ClienteModel retornoCliente = await _clienteService.RetornarClienteEmail(email);

                if (retornoCliente == null)
                {
                    return NotFound(new { erro = "Cliente não encontrado" });
                }

                List<TelefoneClienteModel> retornoTelefonesCliente = await _telefoneClienteService.ListarTelefonesCliente(retornoCliente.Id);

                foreach (TelefoneClienteModel telefone in retornoTelefonesCliente)
                {
                    bool desativarTelefone = await _telefoneClienteService.Desativar(telefone.Id);

                    if (!desativarTelefone)
                    {
                        return BadRequest(new { erro = "Erro durante o processo de exclusão dos telefones que pertencem ao cliente." });
                    }
                }

                if (await _clienteService.DesativarEmail(email))
                {
                    return NoContent();
                }

                return BadRequest(new { erro = "Erro ao desativar cliente" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        //[HttpDelete("email/{email}")]
        //public async Task<IActionResult> Reativar(string email)
        //{
        //    try
        //    {
        //        ClienteModel retornoCliente = await _clienteService.RetornarClienteEmail(email);

        //        if (retornoCliente == null)
        //        {
        //            return NotFound(new { erro = "Cliente não encontrado" });
        //        }

        //        List<TelefoneClienteModel> retornoTelefonesCliente = await _telefoneClienteService.RetornarTelefoneIdCliente(retornoCliente.Id);

        //        foreach (TelefoneClienteModel telefone in retornoTelefonesCliente)
        //        {
        //            bool desativarTelefone = await _telefoneClienteService.Desativar(telefone.Id);

        //            if (!desativarTelefone)
        //            {
        //                return BadRequest(new { erro = "Erro durante o processo de exclusão dos telefones que pertencem ao cliente." });
        //            }
        //        }

        //        if (await _clienteService.DesativarEmail(email))
        //        {
        //            return NoContent();
        //        }

        //        return BadRequest(new { erro = "Erro ao desativar cliente" });
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        //    }
        //}
    }
}
