using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoApp.Api.Features.Autenticacao.LogarUsuario.Handlers;
using TodoApp.Api.Features.Autenticacao.LogarUsuario.DTOs;
using TodoApp.Application.Interfaces;

namespace TodoApp.TodoTest.Unit
{
    public class LogarUsuarioHandlerTests
    {
        [Fact]
        public async Task Handle_ValidCredentials_ReturnsToken()
        {
            var mockService = new Mock<IUsuarioService>();

            var expectedToken = "simulated_jwt_token";
            mockService.Setup(s => s.LogarUsuarioAsync("email@teste.com", "Senha123!"))
                       .ReturnsAsync(expectedToken);
            var handler = new LogarUsuarioHandler(mockService.Object);
            var request = new LogarUsuarioRequest("email@teste.com", "Senha123!");

            var response = await handler.Handle(request, CancellationToken.None);

            mockService.Verify(s => s.LogarUsuarioAsync(request.Email, request.Senha), Times.Once);

            Assert.NotNull(response);
            Assert.Equal(expectedToken, response.Token);
        }

        [Fact]
        public async Task Handle_InvalidCredentials_ReturnsNull()
        {
            var mockService = new Mock<IUsuarioService>();

            var expectedToken = "simulated_jwt_token";
            mockService.Setup(s => s.LogarUsuarioAsync("email2@teste.com", "Senha123!"))
                       .ReturnsAsync(expectedToken);

            var handler = new LogarUsuarioHandler(mockService.Object);
            var request = new LogarUsuarioRequest("email@teste.com", "Senha123!");

            var response = await handler.Handle(request, CancellationToken.None);

            mockService.Verify(s => s.LogarUsuarioAsync(request.Email, request.Senha), Times.Once);
            Assert.Null(response.Token);
        }
    }
}
