using MediatR;
using TodoApp.Api.Features.Autenticacao.LogarUsuario.DTOs;

namespace TodoApp.Api.Features.Autenticacao.LogarUsuario.Endpoint
{
    public static class LogarUsuarioEndpoint
    {
        public static void MapLogarUsuarioEndpoint(this WebApplication app)
        {
            app.MapPost("/autenticacao/logar-usuario", async (IMediator mediator, LogarUsuarioRequest request) =>
            {
                var response = await mediator.Send(request);
                return Results.Ok(response);
            })
            .WithName("LogarUsuario")
            .WithTags("Autenticação")
            .Produces<LogarUsuarioResponse>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status500InternalServerError);
        }
    }
}
