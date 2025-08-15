using MediatR;

namespace TodoApp.Api.Features.Autenticacao.RegistrarUsuario.DTOs
{
    public class RegistrarUsuarioCommandRequest : IRequest<RegistrarUsuarioCommandResponse>
    {
        public required string NomeUsuario { get; set; }
        public required string Email { get; set; }
        public required string Senha { get; set; }
    } 
}