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
using System.Transactions;

namespace ApiCliente.Data.Repositories
{
    public class TipoTelefoneRepository : ITipoTelefoneRepository
    {
        private readonly IDBSession _dbSession;

        public TipoTelefoneRepository(IDBSession dbSession)
        {
            _dbSession = dbSession;
        }

        public async Task<List<TipoTelefoneModel>> Listar()
        {
            IDbConnection connection = await _dbSession.GetConnectionAsync("DBCliente");
            string query = @"
						    SELECT
							    *
						    FROM
							    TipoTelefone
						    WHERE
							    Ativo = 1";

            return (await connection.QueryAsync<TipoTelefoneModel>(query)).ToList();
        }

        public async Task<TipoTelefoneModel> RetornarId(int idTipoTelefone)
        {
            IDbConnection connection = await _dbSession.GetConnectionAsync("DBCliente");
            string query = @"
						    SELECT
							    *
						    FROM
							    TipoTelefone
						    WHERE
							    Ativo = 1
                                AND Id = @idTipoTelefone";

            return await connection.QueryFirstOrDefaultAsync<TipoTelefoneModel>(query, new { idTipoTelefone });
        }

        public async Task<int> Cadastrar(TipoTelefoneModel tipoTelefone)
        {
            IDbConnection connection = await _dbSession.GetConnectionAsync("DBCliente");
            string query = @"
						    INSERT INTO TipoTelefone
							    (Tipo)
						    VALUES
							    (@Tipo);
						    SELECT LAST_INSERT_ID()";

            using (var transaction = connection.BeginTransaction())
            {
                try
                {
                    tipoTelefone.Id = await connection.QueryFirstOrDefaultAsync<int>(query, tipoTelefone, transaction: transaction);
                    transaction.Commit();
                    return tipoTelefone.Id;
                }
                catch
                {
                    transaction.Rollback();
                    return 0;
                }
            }
        }

        public async Task<bool> Alterar(TipoTelefoneModel tipoTelefone)
        {
            IDbConnection connection = await _dbSession.GetConnectionAsync("DBCliente");
            string query = @"
						    UPDATE
							    TipoTelefone
						    SET
							    Tipo = @Tipo
						    WHERE
							    Id = @Id";

            using (var transaction = connection.BeginTransaction())
            {
                try
                {
                   var retorno = await connection.ExecuteAsync(query, tipoTelefone, transaction: transaction) > 0;
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

        public async Task<bool> Desativar(int idTipoTelefone)
        {
            IDbConnection connection = await _dbSession.GetConnectionAsync("DBCliente");
            string query = @"
                            UPDATE
        	                    TipoTelefone
                            SET
        	                    Ativo = 0
                            WHERE
        	                    Id = @idTipoTelefone";

            using (var transaction = connection.BeginTransaction())
            {
                try
                {
                    var retorno = await connection.ExecuteAsync(query, new { idTipoTelefone }, transaction: transaction) > 0;
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
