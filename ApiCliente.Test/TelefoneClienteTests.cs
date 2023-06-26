using ApiCliente.Business.Services;
using ApiCliente.Data.Repositories.Interfaces;
using ApiCliente.Model.Entities;
using AutoFixture;
using AutoFixture.AutoMoq;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiCliente.Test
{
    public class TelefoneClienteTests
    {
        private readonly IFixture _fixture;

        public TelefoneClienteTests()
        {
            _fixture = new Fixture().Customize(new AutoMoqCustomization());
            _fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }

        [Fact]
        public async Task ValidTelefone_RetornarTelefonesClienteIsCalled_ReturnValidIdAsync()
        {
            // Arrange
            var addTelefoneModel = _fixture.Create<TelefoneClienteModel>();

            var idTelefone = addTelefoneModel.Id = 1;
            var idCliente = addTelefoneModel.IdCliente = 1;

            var telefoneRepositoryMock = new Mock<ITelefoneClienteRepository>();
            telefoneRepositoryMock
                .Setup(x => x.RetornarTelefoneCliente(idTelefone, idCliente))
                .ReturnsAsync(addTelefoneModel);

            var telefoneService = new TelefoneClienteService(telefoneRepositoryMock.Object);

            // Act
            var resultado = await telefoneService.RetornarTelefoneCliente(idTelefone, idCliente);

            // Assert
            Assert.Equal(addTelefoneModel, resultado);
        }

        [Fact]
        public async Task ValidTelefone_ListarTelefonesClienteIsCalled_ReturnValidAsync()
        {
            // Arrange
            var idCliente = 1;

            var listaTelefones = new List<TelefoneClienteModel>
            {
                new TelefoneClienteModel { Id = 1, IdCliente = idCliente, Telefone = "123456789" },
                new TelefoneClienteModel { Id = 2, IdCliente = idCliente, Telefone = "987654321" }
            };

            var telefoneRepositoryMock = new Mock<ITelefoneClienteRepository>();

            telefoneRepositoryMock
                .Setup(x => x.ListarTelefonesCliente(idCliente))
                .ReturnsAsync(listaTelefones);

            var telefoneService = new TelefoneClienteService(telefoneRepositoryMock.Object);

            // Act
            var resultado = await telefoneService.ListarTelefonesCliente(idCliente);

            // Assert
            Assert.Equal(listaTelefones, resultado);
        }

        [Fact]
        public async Task ValidTelefone_AlterarIsCalled_ReturnValidAsync()
        {
            // Arrange
            var addTelefoneModel = _fixture.Create<TelefoneClienteModel>();

            addTelefoneModel.Id = 1;
            addTelefoneModel.IdTipoTelefone = 1;
            addTelefoneModel.Telefone = "11976587654";

            var telefoneRepositoryMock = new Mock<ITelefoneClienteRepository>();

            telefoneRepositoryMock
                .Setup(x => x.Alterar(addTelefoneModel))
                .ReturnsAsync(true);

            var telefoneService = new TelefoneClienteService(telefoneRepositoryMock.Object);

            // Act
            var resultado = await telefoneService.Alterar(addTelefoneModel);

            // Assert
            Assert.True(resultado);

            telefoneRepositoryMock.Verify(er => er.Alterar(It.IsAny<TelefoneClienteModel>()), Times.Once);
        }

        [Fact]
        public async Task ValidTelefone_AlterarIsCalled_ReturnNotValidAsync()
        {
            // Arrange
            TelefoneClienteModel telefone = null!;

            var telefoneRepositoryMock = new Mock<ITelefoneClienteRepository>();

            var telefoneService = new TelefoneClienteService(telefoneRepositoryMock.Object);

            // Act
            var resultado = await telefoneService.Alterar(telefone);

            // Assert
            Assert.False(resultado);
        }

        [Fact]
        public async Task ValidTelefone_CadastrarIsCalled_ReturnValidIdAsync()
        {
            // Arrange
            var addTelefoneModel = _fixture.Create<TelefoneClienteModel>();

            var telefoneRepositoryMock = new Mock<ITelefoneClienteRepository>();

            telefoneRepositoryMock
               .Setup(x => x.Cadastrar(addTelefoneModel));

            var telefoneService = new TelefoneClienteService(telefoneRepositoryMock.Object);

            // Act
            var result = await telefoneService.Cadastrar(addTelefoneModel);

            // Assert
            Assert.IsType<int>(result);

            telefoneRepositoryMock.Verify(er => er.Cadastrar(It.IsAny<TelefoneClienteModel>()), Times.Once);
        }

        [Fact]
        public async Task ValidTelefone_CadastrarIsCalled_ReturnNotValidAsync()
        {
            // Arrange
            TelefoneClienteModel telefone = null!;

            var telefoneRepositoryMock = new Mock<ITelefoneClienteRepository>();

            var telefoneService = new TelefoneClienteService(telefoneRepositoryMock.Object);

            // Act
            var resultado = await telefoneService.Cadastrar(telefone);

            // Assert
            Assert.IsType<int>(resultado);
        }

        [Fact]
        public async Task ValidTelefone_DesativarIsCalled_ReturnValidAsync()
        {
            // Arrange
            var addTelefoneModel = _fixture.Create<TelefoneClienteModel>();

            var idTelefone = addTelefoneModel.Id = 1;
            var idCliente = addTelefoneModel.IdCliente = 1;

            var telefoneRepositoryMock = new Mock<ITelefoneClienteRepository>();

            telefoneRepositoryMock
                .Setup(x => x.Desativar(idTelefone, idCliente))
                .ReturnsAsync(true);

            var telefoneService = new TelefoneClienteService(telefoneRepositoryMock.Object);

            // Act
            var resultado = await telefoneService.Desativar(idTelefone, idCliente);

            // Assert
            Assert.True(resultado);
        }

        [Fact]
        public async Task ValidTelefone_DesativarIsCalled_ReturnNotValidAsync()
        {
            // Arrange
            var addTelefoneModel = _fixture.Create<TelefoneClienteModel>();

            var idTelefone = addTelefoneModel.Id = 1;
            var idCliente = addTelefoneModel.IdCliente = 1;

            var telefoneRepositoryMock = new Mock<ITelefoneClienteRepository>();

            telefoneRepositoryMock
                .Setup(x => x.Desativar(idTelefone, idCliente))
                .ReturnsAsync(false);

            var telefoneService = new TelefoneClienteService(telefoneRepositoryMock.Object);

            // Act
            var resultado = await telefoneService.Desativar(idTelefone, idCliente);

            // Assert
            Assert.False(resultado);
        }

        [Fact]
        public async Task ValidTelefone_AtivarIsCalled_ReturnValidAsync()
        {
            // Arrange
            var addTelefoneModel = _fixture.Create<TelefoneClienteModel>();

            var idCliente = addTelefoneModel.IdCliente = 1;
            var telefone = addTelefoneModel.Telefone = "11987654532";

            var telefoneRepositoryMock = new Mock<ITelefoneClienteRepository>();

            telefoneRepositoryMock
                .Setup(x => x.Reativar(idCliente, telefone))
                .ReturnsAsync(true);

            var telefoneService = new TelefoneClienteService(telefoneRepositoryMock.Object);

            // Act
            var resultado = await telefoneService.Reativar(idCliente, telefone);

            // Assert
            Assert.True(resultado);
        }

        [Fact]
        public async Task ValidTelefone_AtivarIsCalled_ReturnNotValidAsync()
        {
            // Arrange
            var addTelefoneModel = _fixture.Create<TelefoneClienteModel>();

            var idCliente = addTelefoneModel.IdCliente = 1;
            var telefone = addTelefoneModel.Telefone = "11987654532";

            var telefoneRepositoryMock = new Mock<ITelefoneClienteRepository>();

            telefoneRepositoryMock
                .Setup(x => x.Reativar(idCliente, telefone))
                .ReturnsAsync(false);

            var telefoneService = new TelefoneClienteService(telefoneRepositoryMock.Object);

            // Act
            var resultado = await telefoneService.Reativar(idCliente, telefone);

            // Assert
            Assert.False(resultado);
        }
    }
}
