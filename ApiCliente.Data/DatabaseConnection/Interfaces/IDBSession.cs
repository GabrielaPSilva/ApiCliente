using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiCliente.Data.DatabaseConnection.Interfaces
{
    public interface IDBSession
    {
        Task<IDbConnection> GetConnectionAsync(string banco);
    }
}
