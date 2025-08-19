using MediatR;
using TodoApp.Api.Features.Autenticacao.LogarUsuario.DTOs;
using TodoApp.Application.Interfaces;

namespace TodoApp.Api.Features.Autenticacao.LogarUsuario.Handlers;
public class LogarUsuarioHandler(IUsuarioService _service) : IRequestHandler<LogarUsuarioRequest, LogarUsuarioResponse>
{
    private readonly IUsuarioService service = _service;
    public async Task<LogarUsuarioResponse> Handle(LogarUsuarioRequest request, CancellationToken cancellationToken)
    {
        var token = await service.LogarUsuarioAsync(request.Email, request.Senha);
        return new LogarUsuarioResponse(token);
    }
}
