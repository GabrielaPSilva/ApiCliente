using ApiCliente.Business.Services;
using ApiCliente.Business.Services.Interfaces;
using ApiCliente.Controllers;
using ApiCliente.Data.Repositories;
using ApiCliente.Data.Repositories.Interfaces;
using ApiCliente.Model.Entities;
using AutoFixture;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiCliente.Test
{
    public class TipoTelefoneTests
    {
        [Fact]
        public async Task ValidTipoTelefone_RetornarIsCalled_ReturnValidIdAsync()
        {
            // Arrange
            var addTipoTelefoneModel = new Fixture().Create<TipoTelefoneModel>();

            var idTipoTelefone = addTipoTelefoneModel.Id = 1;

            var tipoTelefoneRepositoryMock = new Mock<ITipoTelefoneRepository>();
            tipoTelefoneRepositoryMock.Setup(x => x.RetornarId(idTipoTelefone)).ReturnsAsync(addTipoTelefoneModel);

            var tipoTelefoneService = new TipoTelefoneService(tipoTelefoneRepositoryMock.Object);

            // Act
            var resultado = await tipoTelefoneService.Retornar(idTipoTelefone);

            // Assert
            Assert.Equal(addTipoTelefoneModel, resultado);
        }

        [Fact]
        public async Task ValidTipoTelefone_AlterarIsCalled_ReturnValidAsync()
        {
            // Arrange
            var addTipoTelefoneModel = new Fixture().Create<TipoTelefoneModel>();

            addTipoTelefoneModel.Id = 1;
            addTipoTelefoneModel.Tipo = "Fixo";

            var tipoTelefoneRepositoryMock = new Mock<ITipoTelefoneRepository>();

            tipoTelefoneRepositoryMock.Setup(x => x.Alterar(addTipoTelefoneModel)).ReturnsAsync(true);

            var tipoTelefoneService = new TipoTelefoneService(tipoTelefoneRepositoryMock.Object);

            // Act
            var resultado = await tipoTelefoneService.Alterar(addTipoTelefoneModel);

            // Assert
            Assert.True(resultado);

            tipoTelefoneRepositoryMock.Verify(er => er.Alterar(It.IsAny<TipoTelefoneModel>()), Times.Once);
        }

        [Fact]
        public async Task ValidTipoTelefone_AlterarIsCalled_ReturnNotValidAsync()
        {
            // Arrange
            TipoTelefoneModel tipoTelefone = null!;

            var tipoTelefoneRepositoryMock = new Mock<ITipoTelefoneRepository>();

            var tipoTelefoneService = new TipoTelefoneService(tipoTelefoneRepositoryMock.Object);

            // Act
            var resultado = await tipoTelefoneService.Alterar(tipoTelefone);

            // Assert
            Assert.False(resultado);
        }

        [Fact]
        public async Task ValidTipoTelefone_CadastrarIsCalled_ReturnValidIdAsync()
        {
            // Arrange
            var addTipoTelefoneModel = new Fixture().Create<TipoTelefoneModel>();

            var tipoTelefoneRepositoryMock = new Mock<ITipoTelefoneRepository>();

            var employeeService = new TipoTelefoneService(tipoTelefoneRepositoryMock.Object);

            // Act
            var result = await employeeService.Cadastrar(addTipoTelefoneModel);

            // Assert
            Assert.IsType<int>(result);

            tipoTelefoneRepositoryMock.Verify(er => er.Cadastrar(It.IsAny<TipoTelefoneModel>()), Times.Once);
        }

        [Fact]
        public async Task ValidTipoTelefone_CadastrarIsCalled_ReturnNotValidAsync()
        {
            // Arrange
            TipoTelefoneModel tipoTelefone = null!;

            var tipoTelefoneRepositoryMock = new Mock<ITipoTelefoneRepository>();

            var tipoTelefoneService = new TipoTelefoneService(tipoTelefoneRepositoryMock.Object);

            // Act
            var resultado = await tipoTelefoneService.Cadastrar(tipoTelefone);

            // Assert
            Assert.IsType<int>(resultado);
        }
    }
}
