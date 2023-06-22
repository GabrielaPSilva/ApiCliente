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
        Task<TelefoneClienteModel> Retornar(int idTelefone);
        Task<int> Cadastrar(TelefoneClienteModel telefone);
        Task<bool> Alterar(TelefoneClienteModel telefone);
        Task<bool> AlterarTelefoneIdCliente(int idCliente, TelefoneClienteModel telefone);
        Task<bool> Desativar(int idTelefone);
    }
}
