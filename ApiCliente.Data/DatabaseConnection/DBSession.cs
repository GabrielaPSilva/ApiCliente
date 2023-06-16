using ApiCliente.Data.DatabaseConnection.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace ApiCliente.Data.DatabaseConnection
{
    public class DBSession : IDBSession
    {
        public IDbConnection? Connection { get; set; }
        public IDbTransaction? Transaction { get; set; }

        public async Task<bool> GenerateSessionDB(string banco)
        {
            Connection = await ConnectionFactory.ConnectionFactory.ConnectionDBAsync(banco);

            if (Connection.State != ConnectionState.Open)
                Connection.Open();

            return Connection != null;
        }

        public async Task<IDbConnection> GetConnectionAsync(string banco)
        {
            if (Connection == null)
            {
                await GenerateSessionDB(banco);
            }

            Connection!.ChangeDatabase(banco);

            return Connection;
        }

        public IDbTransaction GetTransactionAsync() => Connection!.BeginTransaction();
    }
}
