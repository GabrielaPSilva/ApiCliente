using ApiCliente.Business.Services.Interfaces;
using ApiCliente.Data.Repositories;
using ApiCliente.Data.Repositories.Interfaces;
using ApiCliente.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiCliente.Business.Services
{
    public class ClienteService : IClienteService
    {
        private readonly IClienteRepository _clienteRepository;

        public ClienteService(IClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }

        public async Task<List<ClienteModel>> Listar()
        {
            return await _clienteRepository.Listar();
        }

        public async Task<List<ClienteModel>> ListarClientesTelefones()
        {
            return await _clienteRepository.ListarClientesTelefones();
        }

        public async Task<ClienteModel> Retornar(int idCliente)
        {
            return await _clienteRepository.RetornarId(idCliente);
        }

        public async Task<ClienteModel> RetornarClienteTelefone(string telefone)
        {
            return await _clienteRepository.RetornarClienteTelefone(telefone);
        }

        public async Task<int> Cadastrar(ClienteModel cliente)
        {
            if (cliente == null)
                return 0;

            return await _clienteRepository.Cadastrar(cliente);
        }

        public async Task<bool> Alterar(ClienteModel cliente)
        {
            if (cliente == null)
                return false;

            return await _clienteRepository.Alterar(cliente);
        }

        public async Task<bool> Desativar(int idTelefone)
        {
            return await _clienteRepository.Desativar(idTelefone);
        }

        public async Task<bool> DesativarEmail(string email)
        {
            return await _clienteRepository.DesativarEmail(email);
        }

        public async Task<bool> Deletar(int idCliente)
        {
            return await _clienteRepository.Deletar(idCliente);
        }
    }
}
