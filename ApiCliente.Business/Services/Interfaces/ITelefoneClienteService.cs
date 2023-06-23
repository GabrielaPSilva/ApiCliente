using ApiCliente.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiCliente.Business.Services.Interfaces
{
    public interface ITelefoneClienteService
    {
        Task<List<TelefoneClienteModel>> Listar();
        Task<List<TelefoneClienteModel>> ListarTelefonesCliente(int idCliente);
        Task<TelefoneClienteModel> RetornarTelefoneCliente(int idTelefone, int idCliente);
        Task<int> Cadastrar(TelefoneClienteModel telefone);
        Task<bool> Alterar(TelefoneClienteModel telefone);
        Task<bool> Desativar(int idTelefone);
        Task<bool> Reativar(string telefone);
    }
}
