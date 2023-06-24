using ApiCliente.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiCliente.Test
{
    public class ApiClienteTesteTelefoneCliente
    {
        [Theory]
        [InlineData(1, 2, 3, true, "1243456789")]
        [InlineData(4, 5, 6, true, "11987654321")]
        public void TesteTelefoneCliente(int id, int idCliente, int idTipoTelefone, bool ativo, string telefone)
        {
            // Arrange
            var modelo = new TelefoneClienteModel
            {
                Id = id,
                IdCliente = idCliente,
                IdTipoTelefone = idTipoTelefone,
                Ativo = ativo,
                Telefone = telefone
            };

            // Act
            var idObtido = modelo.Id;
            var idClienteObtido = modelo.IdCliente;
            var IdTipoTelefoneObtido = modelo.IdTipoTelefone;
            var ativoObtido = modelo.Ativo;
            var telefoneObtido = modelo.Telefone;

            // Assert
            Assert.Equal(id, idObtido);
            Assert.Equal(idCliente, idClienteObtido);
            Assert.Equal(idTipoTelefone, IdTipoTelefoneObtido);
            Assert.Equal(ativo, ativoObtido);
            Assert.Equal(telefone, telefoneObtido);
        }
    }
}
