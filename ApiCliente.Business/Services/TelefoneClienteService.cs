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
    public class TelefoneClienteService : ITelefoneClienteService
    {
        private readonly ITelefoneClienteRepository _telefoneRepository;

        public TelefoneClienteService(ITelefoneClienteRepository telefoneRepository)
        {
            _telefoneRepository = telefoneRepository;
        }

        public async Task<List<TelefoneClienteModel>> Listar()
        {
            return await _telefoneRepository.Listar();
        }

        public async Task<TelefoneClienteModel> Retornar(int idTelefone)
        {
            return await _telefoneRepository.RetornarId(idTelefone);
        }

        public async Task<int> Cadastrar(TelefoneClienteModel telefone)
        {
            if (telefone == null)
                return 0;

            return await _telefoneRepository.Cadastrar(telefone);
        }

        public async Task<bool> Alterar(TelefoneClienteModel telefone)
        {
            if (telefone == null)
                return false;

            return await _telefoneRepository.Alterar(telefone);
        }

        public async Task<bool> AlterarTelefoneIdCliente(int idCliente, TelefoneClienteModel telefone)
        {
            if (telefone == null)
                return false;

            return await _telefoneRepository.AlterarTelefoneIdCliente(idCliente, telefone);
        }

        public async Task<bool> Desativar(int idTelefone)
        {
            return await _telefoneRepository.Desativar(idTelefone);
        }
    }
}
