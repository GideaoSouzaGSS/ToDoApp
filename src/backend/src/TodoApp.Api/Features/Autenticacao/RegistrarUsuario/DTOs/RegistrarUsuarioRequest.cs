using MediatR;

namespace TodoApp.Api.Features.Autenticacao.RegistrarUsuario.DTOs
{
    public class RegistrarUsuarioRequest : IRequest<RegistrarUsuarioResponse>
    {
        public required string NomeUsuario { get; set; }
        public required string Email { get; set; }
        public required string Senha { get; set; }
    } 
}