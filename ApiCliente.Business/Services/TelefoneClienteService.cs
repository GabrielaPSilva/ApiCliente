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

        public async Task<List<TelefoneClienteModel>> ListarTelefonesCliente(int idCliente)
        {
            return await _telefoneRepository.ListarTelefonesCliente(idCliente);
        }

        public async Task<TelefoneClienteModel> RetornarTelefoneCliente(int idTelefone, int idCliente)
        {
            return await _telefoneRepository.RetornarTelefoneCliente(idTelefone, idCliente);
        }

        public async Task<List<TelefoneClienteModel>> RetornarTelefonesClientesInativos(int idCliente)
        {
            return await _telefoneRepository.RetornarTelefonesClientesInativos(idCliente);
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

        public async Task<bool> Desativar(int idTelefone, int idCliente)
        {
            return await _telefoneRepository.Desativar(idTelefone, idCliente);
        }

        public async Task<bool> Reativar(int idCliente, string telefone)
        {
            return await _telefoneRepository.Reativar(idCliente, telefone);
        }
    }
}
