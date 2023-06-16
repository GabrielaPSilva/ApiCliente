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

      //  public async Task<int> Cadastrar(TipoTelefoneModel tipoTelefone)
      //  {
      //      IDbConnection connection = await _dbSession.GetConnectionAsync("Agenda");
      //      string query = @"
						//INSERT INTO TipoContato
						//	(Nome, RegexValidacao)
						//VALUES
						//	(@Nome, @RegexValidacao);
						//SELECT LAST_INSERT_ID();
						//";

      //      using (var transaction = connection.BeginTransaction())
      //      {
      //          try
      //          {
      //              tipoTelefone.Id = await connection.QueryFirstOrDefaultAsync<int>(query, tipoTelefone, transaction: transaction);
      //              transaction.Commit();
      //              return tipoTelefone.Id;
      //          }
      //          catch
      //          {
      //              transaction.Rollback();
      //              throw;
      //          }
      //      }
      //  }

      //  public async Task<bool> Alterar(TipoContato tipoContato)
      //  {
      //      IDbConnection connection = await _dbSession.GetConnectionAsync("Agenda");
      //      string query = @"
						//UPDATE
						//	TipoContato
						//SET
						//	Nome = @Nome,
						//	RegexValidacao = @RegexValidacao
						//WHERE
						//	Codigo = @Codigo;
						//";

      //      return await connection.ExecuteAsync(
      //          query,
      //          tipoContato,
      //          transaction: _dbSession.Transaction) > 0;
      //  }

      //  public async Task<bool> Desativar(int codigoTipoContato)
      //  {
      //      IDbConnection connection = await _dbSession.GetConnectionAsync("Agenda");
      //      string query = @"
						//UPDATE
						//	TipoContato
						//SET
						//	Ativo = 0
						//WHERE
						//	Codigo = @codigoTipoContato;
						//";

      //      return await connection.ExecuteAsync(
      //          query,
      //          new { codigoTipoContato },
      //          transaction: _dbSession.Transaction) > 0;
      //  }
    }
}
