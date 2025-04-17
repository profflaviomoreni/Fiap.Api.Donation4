using Fiap.Api.Donation4.Controllers;
using Fiap.Api.Donation4.Models;
using Fiap.Api.Donation4.Repository;
using Fiap.Api.Donation4.Repository.Interfaces;
using Fiap.Api.Donation4.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Fiap.Api.Donation4Test
{
    public class UsuarioControllerTest : BaseTest
    {

        private readonly Mock<IUsuarioRepository> _mockUsuarioRepository;

        public UsuarioControllerTest()
        {
            _mockUsuarioRepository = new Mock<IUsuarioRepository>();        
        }

        [Fact]
        public async Task GetUsuarioResultOkWithUsuarios()
        {
            // Arrange
            var usuarios = new List<UsuarioModel> {
                new UsuarioModel { UsuarioId = 1, NomeUsuario = "Usuario 1" },
                new UsuarioModel { UsuarioId = 2, NomeUsuario = "Usuario 2" }
            };

            _mockUsuarioRepository.Setup( r => r.FindAllAsync() ).ReturnsAsync( usuarios );

            // Act
            var controller = new UsuarioController(_mockUsuarioRepository.Object, _mapper);
            var getResult = await controller.Get();


            // Assert
            var resultType = Assert.IsType<OkObjectResult>(getResult.Result);
            var resultValue = Assert.IsType<List<UsuarioResponseViewModel>>(resultType.Value);

            Assert.Equal(2, resultValue.Count());
            Assert.Equal("Usuario 1", resultValue[0].NomeUsuario);
            Assert.Equal("Usuario 2", resultValue[1].NomeUsuario);
        }

        [Fact]
        public async Task GetUsuarioResultOkWith3Usuarios()
        {
            // Arrange
            var usuarios = new List<UsuarioModel> {
                new UsuarioModel { UsuarioId = 1, NomeUsuario = "Usuario 1" },
                new UsuarioModel { UsuarioId = 2, NomeUsuario = "Usuario 2" },
                new UsuarioModel { UsuarioId = 3, NomeUsuario = "Usuario 3" }
            };

            _mockUsuarioRepository.Setup(r => r.FindAllAsync()).ReturnsAsync(usuarios);

            // Act
            var controller = new UsuarioController(_mockUsuarioRepository.Object, _mapper);
            var getResult = await controller.Get();


            // Assert
            var resultType = Assert.IsType<OkObjectResult>(getResult.Result);
            var resultValue = Assert.IsType<List<UsuarioResponseViewModel>>(resultType.Value);

            Assert.Equal(3, resultValue.Count());
            Assert.NotEqual(2, resultValue.Count());
            Assert.NotNull(resultValue[0].NomeUsuario);

        }


        [Fact]
        public async Task GetUsuarioResultNoContent()
        {
            // Arrange
            var usuarios = new List<UsuarioModel>();


            _mockUsuarioRepository.Setup(r => r.FindAllAsync()).ReturnsAsync(usuarios);

            // Act
            var controller = new UsuarioController(_mockUsuarioRepository.Object, _mapper);
            var getResult = await controller.Get();


            // Assert
            var resultType = Assert.IsType<NoContentResult>(getResult.Result);

        }


    }
}
