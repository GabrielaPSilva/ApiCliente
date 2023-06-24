using ApiCliente.Model.Entities;

namespace ApiCliente.Test
{
    public class ApiClienteTesteCliente
    {

        [Theory]
        [InlineData(1, "João", "joao@gmail.com", true)]
        [InlineData(2, "Maria", "maria@hotmail.com", true)]
        [InlineData(3, "José", "jose@yahoo.com.br", true)]

        public void TestCliente(int id, string nome, string email, bool ativo)
        {
            // Arrange
            var modelo = new ClienteModel
            {
                Id = id,
                Nome = nome,
                Email = email,
                Ativo = ativo
            };

            // Act
            var idObtido = modelo.Id;
            var nomeObtido = modelo.Nome;
            var emailObtido = modelo.Email;
            var ativoObtido = modelo.Ativo;

            // Assert
            Assert.Equal(id, idObtido);
            Assert.Equal(nome, nomeObtido);
            Assert.Equal(email, emailObtido);
            Assert.Equal(ativo, ativoObtido);
        }
    }
}