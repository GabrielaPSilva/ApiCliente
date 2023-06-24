using ApiCliente.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiCliente.Data.Repositories.Interfaces
{
    public interface IClienteRepository
    {
        Task<List<ClienteModel>> Listar();
        Task<List<ClienteModel>> ListarClientesTelefones();
        Task<ClienteModel> RetornarId(int idCliente);
        Task<ClienteModel> RetornarEmail(string email);
        Task<ClienteModel> RetornarClienteEmail(string email);
        Task<ClienteModel> RetornarClienteTelefone(string telefone);
        Task<int> Cadastrar(ClienteModel cliente);
        Task<bool> Alterar(ClienteModel cliente);
        Task<bool> Desativar(int idCliente);
        Task<bool> DesativarEmail(string email);
        Task<bool> Deletar(int idCliente);
        Task<bool> Reativar(string email);
    }
}
