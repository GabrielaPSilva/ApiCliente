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
    public class TelefoneClienteRepository : ITelefoneClienteRepository
    {
        private readonly IDBSession _dbSession;

        public TelefoneClienteRepository(IDBSession dbSession)
        {
            _dbSession = dbSession;
        }

        public async Task<List<TelefoneClienteModel>> Listar()
        {
            IDbConnection connection = await _dbSession.GetConnectionAsync("DBCliente");
            string query = @"
						    SELECT 
                                TelefoneCliente.Id,
                                TelefoneCliente.IdCliente,  
                                TelefoneCliente.IdTipoTelefone,
	                            TelefoneCliente.Telefone,
                                TelefoneCliente.Ativo,
                                TipoTelefone.Id,
                                TipoTelefone.Tipo
                            FROM
	                            TelefoneCliente
		                            INNER JOIN TipoTelefone ON
			                            TelefoneCliente.IdTipoTelefone = TipoTelefone.Id
                            WHERE 
	                            TelefoneCliente.Ativo = 1";

            var lookupTelefone = new Dictionary<int, TelefoneClienteModel>();

            await connection.QueryAsync<TelefoneClienteModel, TipoTelefoneModel, TelefoneClienteModel>(query,
                (telefone, tipoTelefone) =>
                {
                    if (!lookupTelefone.TryGetValue(telefone.Id, out var telefoneExistente))
                    {
                        telefoneExistente = telefone;
                        lookupTelefone.Add(telefone.Id, telefoneExistente);
                    }

                    telefoneExistente.TipoTelefone = tipoTelefone;

                    return null!;
                },
                splitOn: "Id");

            return lookupTelefone.Values.ToList();
        }

        public async Task<TelefoneClienteModel> RetornarTelefoneCliente(int idTelefone, int idCliente)
        {
            IDbConnection connection = await _dbSession.GetConnectionAsync("DBCliente");
            string query = @"
						    SELECT 
                                TelefoneCliente.Id,
                                TelefoneCliente.IdCliente,  
                                TelefoneCliente.IdTipoTelefone,
	                            TelefoneCliente.Telefone,
                                TelefoneCliente.Ativo,
                                TipoTelefone.Id,
                                TipoTelefone.Tipo
                            FROM
	                            TelefoneCliente
		                            INNER JOIN TipoTelefone ON
			                            TelefoneCliente.IdTipoTelefone = TipoTelefone.Id
                            WHERE 
	                            TelefoneCliente.Ativo = 1
                                AND TelefoneCliente.Id = @idTelefone
                                AND TelefoneCliente.IdCliente = @idCliente";

            return (await connection.QueryAsync<TelefoneClienteModel, TipoTelefoneModel, TelefoneClienteModel>(
                query,
                (telefone, tipoTelefone) =>
                {
                    telefone.TipoTelefone = tipoTelefone;
                    return telefone;
                },
                new { idTelefone, idCliente },
                splitOn: "Id")).FirstOrDefault()!;
        }

        public async Task<List<TelefoneClienteModel>> ListarTelefonesCliente(int idCliente)
        {
            IDbConnection connection = await _dbSession.GetConnectionAsync("DBCliente");
            string query = @"
						   SELECT 
                                TelefoneCliente.Id,
                                TelefoneCliente.IdCliente,  
                                TelefoneCliente.IdTipoTelefone,
	                            TelefoneCliente.Telefone,
                                TelefoneCliente.Ativo,
                                TipoTelefone.Id,
                                TipoTelefone.Tipo
                            FROM
	                            TelefoneCliente
		                            INNER JOIN TipoTelefone ON
			                            TelefoneCliente.IdTipoTelefone = TipoTelefone.Id
                            WHERE 
	                            TelefoneCliente.Ativo = 1
                                AND TelefoneCliente.IdCliente = @idCliente";

            var lookupTelefone = new Dictionary<int, TelefoneClienteModel>();

            await connection.QueryAsync<TelefoneClienteModel, TipoTelefoneModel, TelefoneClienteModel>(query,
                (telefone, tipoTelefone) =>
                {
                    if (!lookupTelefone.TryGetValue(telefone.Id, out var telefoneExistente))
                    {
                        telefoneExistente = telefone;
                        lookupTelefone.Add(telefone.Id, telefoneExistente);
                    }

                    telefoneExistente.TipoTelefone = tipoTelefone;

                    return null!;
                },
                new { idCliente },
                splitOn: "Id");

            return lookupTelefone.Values.ToList();
        }

        public async Task<int> Cadastrar(TelefoneClienteModel telefone)
        {
            IDbConnection connection = await _dbSession.GetConnectionAsync("DBCliente");
            string query = @"
						    INSERT INTO TelefoneCliente
							    (IdCliente, IdTipoTelefone, Telefone)
						    VALUES
							    (@IdCliente, @IdTipoTelefone, @Telefone);
						    SELECT SCOPE_IDENTITY()";

            using (var transaction = connection.BeginTransaction())
            {
                try
                {
                    telefone.Id = await connection.QueryFirstOrDefaultAsync<int>(query, telefone, transaction: transaction);
                    transaction.Commit();
                    return telefone.Id;
                }
                catch (System.Data.SqlClient.SqlException ex)
                {
                    if (ex.Number == 2627 || ex.Number == 2601)
                    {
                        transaction.Rollback();
                        return ex.Number;
                    }

                    transaction.Rollback();
                    return 0;
                }
            }
        }

        public async Task<bool> Alterar(TelefoneClienteModel telefone)
        {
            IDbConnection connection = await _dbSession.GetConnectionAsync("DBCliente");
            string query = @"
						    UPDATE
							    TelefoneCliente
						    SET
							    IdCliente = @IdCliente,
                                IdTipoTelefone = @IdTipoTelefone,
                                Telefone = @Telefone
						    WHERE
							    Id = @Id";

            using (var transaction = connection.BeginTransaction())
            {
                try
                {
                    var retorno = await connection.ExecuteAsync(query, telefone, transaction: transaction) > 0;
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

        public async Task<bool> Desativar(int idTelefone)
        {
            IDbConnection connection = await _dbSession.GetConnectionAsync("DBCliente");
            string query = @"
                            UPDATE
        	                    TelefoneCliente
                            SET
        	                    Ativo = 0
                            WHERE
        	                    Id = @idTelefone";

            using (var transaction = connection.BeginTransaction())
            {
                try
                {
                    var retorno = await connection.ExecuteAsync(query, new { idTelefone }, transaction: transaction) > 0;
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

        public async Task<bool> Reativar(string telefone)
        {
            IDbConnection connection = await _dbSession.GetConnectionAsync("DBCliente");
            string query = @"
                            UPDATE
        	                    TelefoneCliente
                            SET
        	                    Ativo = 1
                            WHERE
        	                    Id = @telefone";

            using (var transaction = connection.BeginTransaction())
            {
                try
                {
                    var retorno = await connection.ExecuteAsync(query, new { telefone }, transaction: transaction) > 0;
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
