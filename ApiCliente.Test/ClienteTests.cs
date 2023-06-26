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

        [Fact]
        public async Task ValidCliente_AlterarIsCalled_ReturnValidAsync()
        {
            // Arrange
            var addClienteModel = new ClienteModel
            {
                Id = 1,
                Nome = "Teste",
                Email = "teste@gmail.com",
                ListaTelefones = new List<TelefoneClienteModel>
                {
                    new TelefoneClienteModel { Id = 1, IdCliente = 1, Telefone = "123456789" },
                    new TelefoneClienteModel { Id = 2, IdCliente = 1, Telefone = "987654321" }
                }
            };

            var clienteRepositoryMock = new Mock<IClienteRepository>();

            clienteRepositoryMock
                .Setup(x => x.Alterar(addClienteModel))
                .ReturnsAsync(true);

            var clienteService = new ClienteService(clienteRepositoryMock.Object);

            // Act
            var resultado = await clienteService.Alterar(addClienteModel);

            // Assert
            Assert.True(resultado);

            clienteRepositoryMock.Verify(er => er.Alterar(It.IsAny<ClienteModel>()), Times.Once);
        }

        [Fact]
        public async Task ValidCliente_AlterarIsCalled_ReturnNotValidAsync()
        {
            // Arrange
            ClienteModel cliente = null!;

            var clienteRepositoryMock = new Mock<IClienteRepository>();

            var clienteService = new ClienteService(clienteRepositoryMock.Object);

            // Act
            var resultado = await clienteService.Alterar(cliente);

            // Assert
            Assert.False(resultado);
        }

        [Fact]
        public async Task ValidCliente_CadastrarIsCalled_ReturnValidAsync()
        {
            // Arrange
            var addClienteModel = _fixture.Create<ClienteModel>();

            var clienteRepositoryMock = new Mock<IClienteRepository>();

            clienteRepositoryMock
               .Setup(x => x.Cadastrar(addClienteModel));

            var clienteService = new ClienteService(clienteRepositoryMock.Object);

            // Act
            var result = await clienteService.Cadastrar(addClienteModel);

            // Assert
            Assert.IsType<int>(result);

            clienteRepositoryMock.Verify(er => er.Cadastrar(It.IsAny<ClienteModel>()), Times.Once);
        }

        [Fact]
        public async Task ValidCliente_CadastrarIsCalled_ReturnNotValidAsync()
        {
            // Arrange
            ClienteModel cliente = null!;

            var clienteRepositoryMock = new Mock<IClienteRepository>();

            var clienteService = new ClienteService(clienteRepositoryMock.Object);

            // Act
            var resultado = await clienteService.Cadastrar(cliente);

            // Assert
            Assert.IsType<int>(resultado);
        }

        [Fact]
        public async Task ValidCliente_DesativarIsCalled_ReturnValidAsync()
        {
            // Arrange
            var addClienteModel = _fixture.Create<ClienteModel>();

            var idCliente = addClienteModel.Id = 1;

            var clienteRepositoryMock = new Mock<IClienteRepository>();

            clienteRepositoryMock
                .Setup(x => x.Desativar(idCliente))
                .ReturnsAsync(true);

            var clienteService = new ClienteService(clienteRepositoryMock.Object);

            // Act
            var resultado = await clienteService.Desativar(idCliente);

            // Assert
            Assert.True(resultado);
        }

        [Fact]
        public async Task ValidCliente_DesativarIsCalled_ReturnNotValidAsync()
        {
            // Arrange
            var addClienteModel = _fixture.Create<ClienteModel>();

            var idCliente = addClienteModel.Id = 1;

            var clienteRepositoryMock = new Mock<IClienteRepository>();

            clienteRepositoryMock
                .Setup(x => x.Desativar(idCliente))
                .ReturnsAsync(false);

            var clienteService = new ClienteService(clienteRepositoryMock.Object);

            // Act
            var resultado = await clienteService.Desativar(idCliente);

            // Assert
            Assert.False(resultado);
        }

        [Fact]
        public async Task ValidCliente_AtivarIsCalled_ReturnValidAsync()
        {
            // Arrange
            var addClienteModel = _fixture.Create<ClienteModel>();

            var email = addClienteModel.Email = "teste@gmail.com";

            var clienteRepositoryMock = new Mock<IClienteRepository>();

            clienteRepositoryMock
                .Setup(x => x.Reativar(email))
                .ReturnsAsync(true);

            var clienteService = new ClienteService(clienteRepositoryMock.Object);

            // Act
            var resultado = await clienteService.Reativar(email);

            // Assert
            Assert.True(resultado);
        }

        [Fact]
        public async Task ValidCliente_AtivarIsCalled_ReturnNotValidAsync()
        {
            // Arrange
            var addClienteModel = _fixture.Create<ClienteModel>();

            var email = addClienteModel.Email = "teste@gmail.com";

            var clienteRepositoryMock = new Mock<IClienteRepository>();

            clienteRepositoryMock
                .Setup(x => x.Reativar(email))
                .ReturnsAsync(false);

            var clienteService = new ClienteService(clienteRepositoryMock.Object);

            // Act
            var resultado = await clienteService.Reativar(email);

            // Assert
            Assert.False(resultado);
        }
    }
}