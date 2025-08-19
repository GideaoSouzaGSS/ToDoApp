using MediatR;

namespace TodoApp.Api.Features.Autenticacao.LogarUsuario.DTOs
{
    public class LogarUsuarioRequest(string email, string senha) : IRequest<LogarUsuarioResponse>
    {
        public string Email { get; private set; } = email;
        public string Senha { get; private set; } = senha;
    }
}
