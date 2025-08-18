using MediatR;

namespace TodoApp.Api.Features.Autenticacao.LogarUsuario.DTOs
{
    public class LogarUsuarioCommandRequest : IRequest<LogarUsuarioCommandResponse>
    {
        public required string Email { get; set; }
        public required string Senha { get; set; }
        public LogarUsuarioCommandRequest(string email, string senha)
        {
            Email = email;
            Senha = senha;
        }
    }
}
