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
        private readonly static string? _serverAddress =
            Environment.GetEnvironmentVariable("MYSQL_ADDRESS");
        private readonly static string? _port =
            Environment.GetEnvironmentVariable("MYSQL_PORT");
        private readonly static string? _user =
            Environment.GetEnvironmentVariable("MYSQL_USER");
        private readonly static string? _password =
            Environment.GetEnvironmentVariable("MYSQL_PASSWORD");

        private async Task<SqlConnection> GetConnectionDBAsync(string banco = "railway")
        {
            if (string.IsNullOrEmpty(_serverAddress)
                || string.IsNullOrEmpty(_port)
                || string.IsNullOrEmpty(_user)
                || string.IsNullOrEmpty(_password))
            {
                throw new Exception("Configure as variáveis de ambiente \"MYSQL_ADDRESS\", \"MYSQL_PORT\", \"MYSQL_USER\" e \"MYSQL_PASSWORD\" e reinicie o Visual Studio");
            }

            string connectionString = $"Server={_serverAddress}; Port={_port}; Database={banco}; Uid={_user}; Pwd={_password};";

            SqlConnection con = new SqlConnection(connectionString);

            await con.OpenAsync();

            return con;
        }

        public static async Task<IDbConnection> ConnectionDBAsync(string banco)
        {
            return await new ConnectionFactory().GetConnectionDBAsync(banco);
        }
    }
}
