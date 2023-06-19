using ApiCliente.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiCliente.Business.Services.Interfaces
{
    public interface ITipoTelefoneService
    {
        Task<List<TipoTelefoneModel>> Listar();
        Task<TipoTelefoneModel> Retornar(int idTipoTelefone);
        Task<bool> Cadastrar(TipoTelefoneModel tipoTelefone);
        Task<bool> Alterar(TipoTelefoneModel tipoTelefone);
        Task<bool> Desativar(int idTipoTelefone);
    }
}
