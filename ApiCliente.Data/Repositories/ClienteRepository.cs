using ApiCliente.Data.DatabaseConnection.Interfaces;
using ApiCliente.Data.Repositories.Interfaces;
using ApiCliente.Model.Entities;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiCliente.Data.Repositories
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly IDBSession _dbSession;

        public ClienteRepository(IDBSession dbSession)
        {
            _dbSession = dbSession;
        }

        public async Task<List<ClienteModel>> Listar()
        {
            IDbConnection connection = await _dbSession.GetConnectionAsync("DBCliente");
            string query = @"
						    SELECT
							    *
						    FROM
							    Cliente
						    WHERE
							    Ativo = 1";

            return (await connection.QueryAsync<ClienteModel>(query)).ToList();
        }

        public async Task<List<ClienteModel>> ListarClientesTelefones()
        {
            IDbConnection connection = await _dbSession.GetConnectionAsync("DBCliente");
            string query = @"
						    SELECT 
                                Cliente.Id,
	                            Cliente.Nome,
	                            Cliente.Email,
                                Cliente.Ativo,
                                TelefoneCliente.Id,
                                TelefoneCliente.IdCliente,
                                TelefoneCliente.IdTipoTelefone,
	                            TelefoneCliente.Telefone,
                                TipoTelefone.Id,
	                            TipoTelefone.Tipo
                            FROM
	                            Cliente
		                            LEFT JOIN TelefoneCliente ON
			                            Cliente.Id = TelefoneCliente.IdCliente
		                            LEFT JOIN TipoTelefone ON
			                            TelefoneCliente.IdTipoTelefone = TipoTelefone.Id
                            WHERE 
	                            Cliente.Ativo = 1
                            ORDER BY
	                            Cliente.Nome";

            var lookupCliente = new Dictionary<int, ClienteModel>();

            await connection.QueryAsync<ClienteModel, TelefoneClienteModel, TipoTelefoneModel, ClienteModel>(query,
                (cliente, telefone, tipoTelefone) =>
                {
                    if (!lookupCliente.TryGetValue(cliente.Id, out var clienteExistente))
                    {
                        clienteExistente = cliente;
                        lookupCliente.Add(cliente.Id, clienteExistente);
                    }

                    clienteExistente.ListaTelefones ??= new List<TelefoneClienteModel>();

                    if (telefone != null)
                    {
                        telefone.TipoTelefone = tipoTelefone;
                        clienteExistente.ListaTelefones.Add(telefone);
                    }

                    return null!;

                }, splitOn: "Id");

            return lookupCliente.Values.ToList();
        }

        public async Task<ClienteModel> RetornarId(int idCliente)
        {
            IDbConnection connection = await _dbSession.GetConnectionAsync("DBCliente");
            string query = @"
						    SELECT 
                                Cliente.Id,
	                            Cliente.Nome,
	                            Cliente.Email,
                                Cliente.Ativo,
                                TelefoneCliente.Id,
                                TelefoneCliente.IdCliente,
                                TelefoneCliente.IdTipoTelefone,
	                            TelefoneCliente.Telefone,
                                TipoTelefone.Id,
	                            TipoTelefone.Tipo
                            FROM
	                            Cliente
		                            LEFT JOIN TelefoneCliente ON
			                            Cliente.Id = TelefoneCliente.IdCliente
		                            LEFT JOIN TipoTelefone ON
			                            TelefoneCliente.IdTipoTelefone = TipoTelefone.Id
                            WHERE 
	                            Cliente.Ativo = 1
                                AND Cliente.Id = @idCliente";

            var lookupCliente = new Dictionary<int, ClienteModel>();

            await connection.QueryAsync<ClienteModel, TelefoneClienteModel, TipoTelefoneModel, ClienteModel>(query,
                (cliente, telefone, tipoTelefone) =>
                {

                    if (!lookupCliente.TryGetValue(cliente.Id, out var clienteExistente))
                    {
                        clienteExistente = cliente;
                        lookupCliente.Add(cliente.Id, clienteExistente);
                    }

                    clienteExistente.ListaTelefones ??= new List<TelefoneClienteModel>();

                    if (telefone != null)
                    {
                        telefone.TipoTelefone = tipoTelefone;
                        clienteExistente.ListaTelefones.Add(telefone);
                    }

                    return null!;
                },
                new { idCliente },
                splitOn: "Id");

            return lookupCliente.Values.FirstOrDefault()!;
        }

        public async Task<ClienteModel> RetornarEmail(string email)
        {
            IDbConnection connection = await _dbSession.GetConnectionAsync("DBCliente");
            string query = @"
						    SELECT 
                                Cliente.Id,
	                            Cliente.Nome,
	                            Cliente.Email,
                                Cliente.Ativo,
                                TelefoneCliente.Id,
                                TelefoneCliente.IdCliente,
                                TelefoneCliente.IdTipoTelefone,
	                            TelefoneCliente.Telefone,
                                TipoTelefone.Id,
	                            TipoTelefone.Tipo
                            FROM
	                            Cliente
		                            LEFT JOIN TelefoneCliente ON
			                            Cliente.Id = TelefoneCliente.IdCliente
		                            LEFT JOIN TipoTelefone ON
			                            TelefoneCliente.IdTipoTelefone = TipoTelefone.Id
                            WHERE 
                                Cliente.Email = @email";

            var lookupCliente = new Dictionary<int, ClienteModel>();

            await connection.QueryAsync<ClienteModel, TelefoneClienteModel, TipoTelefoneModel, ClienteModel>(query,
                (cliente, telefone, tipoTelefone) =>
                {

                    if (!lookupCliente.TryGetValue(cliente.Id, out var clienteExistente))
                    {
                        clienteExistente = cliente;
                        lookupCliente.Add(cliente.Id, clienteExistente);
                    }

                    clienteExistente.ListaTelefones ??= new List<TelefoneClienteModel>();

                    if (telefone != null)
                    {
                        telefone.TipoTelefone = tipoTelefone;
                        clienteExistente.ListaTelefones.Add(telefone);
                    }

                    return null!;
                },
                new { email },
                splitOn: "Id");

            return lookupCliente.Values.FirstOrDefault()!;
        }

        public async Task<ClienteModel> RetornarClienteEmail(string email)
        {
            IDbConnection connection = await _dbSession.GetConnectionAsync("DBCliente");
            string query = @"
						    SELECT 
                                Cliente.Id,
	                            Cliente.Nome,
	                            Cliente.Email
                            FROM
	                            Cliente
                            WHERE 
	                            Cliente.Ativo = 1
                                AND Cliente.Email = @email";

            return await connection.QueryFirstOrDefaultAsync<ClienteModel>(query, new { email });
        }

        public async Task<ClienteModel> RetornarClienteTelefone(string telefone)
        {
            IDbConnection connection = await _dbSession.GetConnectionAsync("DBCliente");
            string query = @"
						    SELECT 
                                Cliente.Id,
	                            Cliente.Nome,
	                            Cliente.Email,
                                Cliente.Ativo,
                                TelefoneCliente.Id,
                                TelefoneCliente.IdCliente,
                                TelefoneCliente.IdTipoTelefone,
	                            TelefoneCliente.Telefone,
                                TipoTelefone.Id,
	                            TipoTelefone.Tipo
                            FROM
	                            Cliente
		                            INNER JOIN TelefoneCliente ON
			                            Cliente.Id = TelefoneCliente.IdCliente
		                            INNER JOIN TipoTelefone ON
			                            TelefoneCliente.IdTipoTelefone = TipoTelefone.Id
                            WHERE
                                Cliente.Ativo = 1 
	                            AND TelefoneCliente.Telefone = @telefone";

            var lookupCliente = new Dictionary<int, ClienteModel>();

            await connection.QueryAsync<ClienteModel, TelefoneClienteModel, TipoTelefoneModel, ClienteModel>(query,
                (cliente, telefone, tipoTelefone) =>
                {

                    if (!lookupCliente.TryGetValue(cliente.Id, out var clienteExistente))
                    {
                        clienteExistente = cliente;
                        lookupCliente.Add(cliente.Id, clienteExistente);
                    }

                    clienteExistente.ListaTelefones ??= new List<TelefoneClienteModel>();

                    if (telefone != null)
                    {
                        telefone.TipoTelefone = tipoTelefone;
                        clienteExistente.ListaTelefones.Add(telefone);
                    }

                    return null!;
                },
                new { telefone },
                splitOn: "Id");

            return lookupCliente.Values.FirstOrDefault()!;
        }

        public async Task<int> Cadastrar(ClienteModel cliente)
        {
            IDbConnection connection = await _dbSession.GetConnectionAsync("DBCliente");
            string query = @"
						    INSERT INTO Cliente
							    (Nome, Email)
						    VALUES
							    (@Nome, @Email);
						    SELECT SCOPE_IDENTITY()";

            using (var transaction = connection.BeginTransaction())
            {
                try
                {
                    cliente.Id = await connection.QueryFirstOrDefaultAsync<int>(query, cliente, transaction: transaction);
                    transaction.Commit();
                    return cliente.Id;
                }
                catch
                {
                    transaction.Rollback();
                    return 0;
                }
            }
        }

        public async Task<bool> Alterar(ClienteModel cliente)
        {
            IDbConnection connection = await _dbSession.GetConnectionAsync("DBCliente");
            string query = @"
						    UPDATE
							    Cliente
						    SET
							    Nome = @Nome,
                                Email = @Email
						    WHERE
							    Id = @Id";

            using (var transaction = connection.BeginTransaction())
            {
                try
                {
                    var retorno = await connection.ExecuteAsync(query, cliente, transaction: transaction) > 0;
                    transaction.Commit();
                    return retorno;
                }
                catch
                {
                    transaction.Rollback();
                    return false;
                }
            }
        }

        public async Task<bool> Desativar(int idCliente)
        {
            IDbConnection connection = await _dbSession.GetConnectionAsync("DBCliente");
            string query = @"
                            UPDATE
        	                    Cliente
                            SET
        	                    Ativo = 0
                            WHERE
        	                    Id = @idCliente";

            using (var transaction = connection.BeginTransaction())
            {
                try
                {
                    var retorno = await connection.ExecuteAsync(query, new { idCliente }, transaction: transaction) > 0;
                    transaction.Commit();
                    return retorno;
                }
                catch
                {
                    transaction.Rollback();
                    return false;
                }
            }
        }

        public async Task<bool> DesativarEmail(string email)
        {
            IDbConnection connection = await _dbSession.GetConnectionAsync("DBCliente");
            string query = @"
                            UPDATE
        	                    Cliente
                            SET
        	                    Ativo = 0
                            WHERE
        	                    Email = @email";

            using (var transaction = connection.BeginTransaction())
            {
                try
                {
                    var retorno = await connection.ExecuteAsync(query, new { email }, transaction: transaction) > 0;
                    transaction.Commit();
                    return retorno;
                }
                catch
                {
                    transaction.Rollback();
                    return false;
                }
            }
        }

        public async Task<bool> Deletar(int idCliente)
        {
            IDbConnection connection = await _dbSession.GetConnectionAsync("DBCliente");
            string query = @"
                            DELETE
        	                    Cliente
                            WHERE
        	                    Id = @idCliente";

            using (var transaction = connection.BeginTransaction())
            {
                try
                {
                    var retorno = await connection.ExecuteAsync(query, new { idCliente }, transaction: transaction) > 0;
                    transaction.Commit();
                    return retorno;
                }
                catch
                {
                    transaction.Rollback();
                    return false;
                }
            }
        }

        public async Task<bool> Reativar(string email)
        {
            IDbConnection connection = await _dbSession.GetConnectionAsync("DBCliente");
            string query = @"
                            UPDATE
        	                    Cliente
                            SET
        	                    Ativo = 1
                            WHERE
        	                    Email = @email";

            using (var transaction = connection.BeginTransaction())
            {
                try
                {
                    var retorno = await connection.ExecuteAsync(query, new { email }, transaction: transaction) > 0;
                    transaction.Commit();
                    return retorno;
                }
                catch
                {
                    transaction.Rollback();
                    return false;
                }
            }
        }
    }
}
