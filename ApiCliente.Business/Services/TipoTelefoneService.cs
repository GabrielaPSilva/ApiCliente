using ApiCliente.Business.Services.Interfaces;
using ApiCliente.Data.Repositories.Interfaces;
using ApiCliente.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiCliente.Business.Services
{
    public class TipoTelefoneService : ITipoTelefoneService
    {
        private readonly ITipoTelefoneRepository _tipoTelefoneRepository;

        public TipoTelefoneService(ITipoTelefoneRepository tipoTelefoneRepository)
        {
            _tipoTelefoneRepository = tipoTelefoneRepository;
        }

        public async Task<List<TipoTelefoneModel>> Listar()
        {
            return await _tipoTelefoneRepository.Listar();
        }

        public async Task<TipoTelefoneModel> Retornar(int idTipoTelefone)
        {
            return await _tipoTelefoneRepository.RetornarId(idTipoTelefone);
        }

        public async Task<bool> Cadastrar(TipoTelefoneModel tipoTelefone)
        {
            if (tipoTelefone == null)
                return false;

            return await _tipoTelefoneRepository.Cadastrar(tipoTelefone) > 0;
        }

        public async Task<bool> Alterar(TipoTelefoneModel tipoTelefone)
        {
            if (tipoTelefone == null)
                return false;

            return await _tipoTelefoneRepository.Alterar(tipoTelefone);
        }

        public async Task<bool> Desativar(int idTipoTelefone)
        {
            return await _tipoTelefoneRepository.Desativar(idTipoTelefone);
        }
    }
}
