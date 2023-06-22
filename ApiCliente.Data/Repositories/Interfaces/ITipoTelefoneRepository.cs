using ApiCliente.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiCliente.Data.Repositories.Interfaces
{
    public interface ITipoTelefoneRepository
    {
        Task<List<TipoTelefoneModel>> Listar();
        Task<TipoTelefoneModel> RetornarId(int idTipoTelefone);
        Task<int> Cadastrar(TipoTelefoneModel tipoTelefone);
        Task<bool> Alterar(TipoTelefoneModel tipoTelefone);
        Task<bool> Deletar(int idTipoTelefone);
    }
}
