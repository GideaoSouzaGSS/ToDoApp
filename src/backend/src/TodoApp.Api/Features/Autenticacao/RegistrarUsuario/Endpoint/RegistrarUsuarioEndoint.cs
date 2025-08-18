using MediatR;
using TodoApp.Api.Features.Autenticacao.RegistrarUsuario.DTOs;
using TodoApp.Application.Interfaces;

namespace TodoApp.Api.Features.Autenticacao.RegistrarUsuario.Endpoint
{
    public static class RegistrarUsuarioEndoint
    {
        public static void MapRegistrarUsuarioEndpoint(this IEndpointRouteBuilder app)
        {
            app.MapPost("/autenticacao/registrar-usuario", async (IMediator mediator, RegistrarUsuarioCommandRequest request, IUsuarioService usuarioService) =>
            {
                var response = await mediator.Send(request);
                return Results.Ok(response);

            })
            .WithName("RegistrarUsuario")
            .WithTags("Autenticação");
        }


    }
}
