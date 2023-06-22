using ApiCliente.Business.Services.Interfaces;
using ApiCliente.Model.Entities;
using Microsoft.AspNetCore.Mvc;

namespace ApiCliente.Controllers
{
    [Route("api/tipostelefone")]
    [ApiController]
    public class TipoTelefoneController : Controller
    {
        private readonly ITipoTelefoneService _tipoTelefoneService;

        public TipoTelefoneController(ITipoTelefoneService tipoTelefoneService)
        {
            _tipoTelefoneService = tipoTelefoneService;
        }

        [HttpGet]
        public async Task<IActionResult> Listar()
        {
            try
            {
                List<TipoTelefoneModel> listaTiposTelefone = await _tipoTelefoneService.Listar();

                if (listaTiposTelefone == null)
                {
                    return NotFound(new { erro = "Lista de tipos de telefone não encontrada" });
                }

                var links = new List<Link>
                {
                    new Link
                    {
                        Rel = "self",
                        Href = $"/api/tipostelefone",
                        Method = "GET"
                    }
                };

                listaTiposTelefone.ForEach(tipoTelefone =>
                {
                    links.Add(new Link
                    {
                        Rel = "related",
                        Href = $"/api/tipostelefone/{tipoTelefone.Id}",
                        Method = "GET"
                    });
                });

                var retorno = new RecursoModel<List<TipoTelefoneModel>>(listaTiposTelefone, links);

                return Ok(retorno);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        [HttpGet("{idTipoTelefone}")]
        public async Task<IActionResult> Retornar(int idTipoTelefone)
        {
            try
            {
                TipoTelefoneModel tipoTelefone = await _tipoTelefoneService.Retornar(idTipoTelefone);

                if (tipoTelefone == null)
                {
                    return NotFound(new { erro = "Tipo de telefone não encontrado" });
                }

                var links = new List<Link>
                {
                    new Link
                    {
                        Rel = "self",
                        Href = $"/api/tipostelefone/{idTipoTelefone}",
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
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Cadastrar([FromBody] TipoTelefoneModel tipoTelefone)
        {
            try
            {
                if (!tipoTelefone.EhValido(out string mensagemErro))
                {
                    return BadRequest(new { erro = mensagemErro });
                }

                var retornoCadastro = await _tipoTelefoneService.Cadastrar(tipoTelefone);

                if (retornoCadastro == 2627 || retornoCadastro == 2601)
                {
                    return BadRequest(new { erro = "Tipo telefone já existente na base" });
                }
                else if (retornoCadastro > 0)
                {
                    var links = new List<Link>
                    {
                        new Link
                        {
                            Rel = "self",
                            Href = $"/api/tipostelefone/{tipoTelefone.Id}",
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

                    return Created($"/api/tipostelefone/{tipoTelefone.Id}", retorno);
                }

                return BadRequest(new { erro = "Erro ao cadastrar tipo de telefone" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPut("{idTipoTelefone}")]
        public async Task<IActionResult> Alterar(int idTipoTelefone, [FromBody] TipoTelefoneModel tipoTelefone)
        {
            try
            {
                if (!tipoTelefone.EhValido(out string mensagemErro))
                {
                    return BadRequest(new { erro = mensagemErro });
                }

                if (await _tipoTelefoneService.Retornar(idTipoTelefone) == null)
                {
                    return NotFound(new { erro = "Tipo de telefone não encontrado" });
                }

                tipoTelefone.Id = idTipoTelefone;

                if (await _tipoTelefoneService.Alterar(tipoTelefone))
                {
                    var links = new List<Link>
                    {
                        new Link
                        {
                            Rel = "self",
                            Href = $"/api/tipostelefone/{tipoTelefone.Id}",
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

        [HttpDelete("{idTipoTelefone}")]
        public async Task<IActionResult> Deletar(int idTipoTelefone)
        {
            try
            {
                if (await _tipoTelefoneService.Retornar(idTipoTelefone) == null)
                {
                    return NotFound(new { erro = "Tipo de telefone não encontrado" });
                }

                if (await _tipoTelefoneService.Deletar(idTipoTelefone))
                {
                    return NoContent();
                }

                return BadRequest(new { erro = "Erro ao desativar tipo de telefone" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
