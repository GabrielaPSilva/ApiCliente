using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiCliente.Data.ConnectionFactory
{
    public class ConnectionFactory
    {
        IConfiguration _configuration;

        public ConnectionFactory(IConfiguration configuration)
        {
            _configuration = configuration;
            DefaultTypeMap.MatchNamesWithUnderscores = true;
        }

        public string GetConnectionDBCliente()
        {
            var connection = _configuration.GetSection("ConnectionStrings").GetSection("ContextDBCliente").Value;
            return connection!;
        }

        public async Task<IDbConnection> AbrirConexaoDBClienteAsync(bool webConfig = false)
        {
            var con = GetConnectionDBCliente();

            var connection = new SqlConnection(con);

            await connection.OpenAsync();

            return connection!;
        }
    }
}
