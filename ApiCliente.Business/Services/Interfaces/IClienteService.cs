using ApiCliente.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiCliente.Business.Services.Interfaces
{
    public interface IClienteService
    {
        Task<List<ClienteModel>> Listar();
        Task<List<ClienteModel>> ListarClientesTelefones();
        Task<ClienteModel> Retornar(int idCliente);
        Task<ClienteModel> RetornarClienteTelefone(string telefone);
        Task<int> Cadastrar(ClienteModel cliente);
        Task<bool> Alterar(ClienteModel cliente);
        Task<bool> Desativar(int idTelefone);
        Task<bool> DesativarEmail(string email);
        Task<bool> Deletar(int idCliente);
    }
}
