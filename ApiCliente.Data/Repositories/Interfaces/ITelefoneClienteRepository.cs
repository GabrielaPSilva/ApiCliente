using ApiCliente.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiCliente.Data.Repositories.Interfaces
{
    public interface ITelefoneClienteRepository
    {
        Task<List<TelefoneClienteModel>> Listar();
        Task<List<TelefoneClienteModel>> ListarTelefonesCliente(int idCliente);
        Task<TelefoneClienteModel> RetornarTelefoneCliente(int idTelefone, int idCliente);
        Task<int> Cadastrar(TelefoneClienteModel telefone);
        Task<bool> Alterar(TelefoneClienteModel telefone);
        Task<bool> Desativar(int idTelefone, int idCliente);
        Task<bool> Reativar(int idCliente, string telefone);
    }
}
