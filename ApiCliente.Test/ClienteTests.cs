using ApiCliente.Business.Services;
using ApiCliente.Data.Repositories.Interfaces;
using ApiCliente.Model.Entities;
using AutoFixture.AutoMoq;
using AutoFixture;
using Moq;

namespace ApiCliente.Test
{
    public class ClienteTests
    {
        private readonly IFixture _fixture;

        public ClienteTests()
        {
            _fixture = new Fixture().Customize(new AutoMoqCustomization());
            _fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }

        [Fact]
        public async Task ValidCliente_RetornarIsCalled_ReturnValidIdAsync()
        {
            // Arrange
            var addClienteModel = _fixture.Create<ClienteModel>();

            var idCliente = addClienteModel.Id = 1;

            var clienteRepositoryMock = new Mock<IClienteRepository>();

            clienteRepositoryMock
                .Setup(x => x.RetornarId(idCliente))
                .ReturnsAsync(addClienteModel);

            var clienteService = new ClienteService(clienteRepositoryMock.Object);

            // Act
            var resultado = await clienteService.Retornar(idCliente);

            // Assert
            Assert.Equal(addClienteModel, resultado);
        }
    }
}