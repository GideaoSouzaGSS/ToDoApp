using TodoApp.Api.Features.Autenticacao.RegistrarUsuario.DTOs;
using TodoApp.Application.Interfaces;

namespace TodoApp.Api.Features.Autenticacao.RegistrarUsuario.Endpoint
{
    public static class RegistrarUsuarioEndoint
    {
        public static void MapRegistrarUsuarioEndpoint(this IEndpointRouteBuilder app)
        {
            app.MapPost("/api/registrar-usuario", async (RegistrarUsuarioCommandRequest command, IUsuarioService usuarioService) =>
            {
                var usuarioId = await usuarioService.RegistrarNovoUsuarioAsync(command.NomeUsuario, command.Email, command.Senha);
                return Results.Ok(usuarioId);
            })
            .WithName("RegistrarUsuario")
            .WithTags("Autenticação");
        }


    }
}
