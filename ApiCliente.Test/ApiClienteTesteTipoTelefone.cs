using ApiCliente.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiCliente.Test
{
    public class ApiClienteTesteTipoTelefone
    {
        [Fact]
        public void TesteTipoTelefone()
        {
            // Arrange
            var modelo = new TipoTelefoneModel
            {
                Id = 1,
                Tipo = "Tipo1"
            };

            // Act
            var idObtido = modelo.Id;
            var tipoObtido = modelo.Tipo;

            // Assert
            Assert.Equal(1, idObtido);
            Assert.Equal("Tipo1", tipoObtido);
        }
    }
}
