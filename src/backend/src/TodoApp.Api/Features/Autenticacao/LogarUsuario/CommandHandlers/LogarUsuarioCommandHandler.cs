using MediatR;
using TodoApp.Api.Features.Autenticacao.LogarUsuario.DTOs;
using TodoApp.Application.Interfaces;

namespace TodoApp.Api.Features.Autenticacao.LogarUsuario.CommandHandlers;
public class LogarUsuarioCommandHandler(IUsuarioService _service) : IRequestHandler<LogarUsuarioCommandRequest, LogarUsuarioCommandResponse>
{
    private readonly IUsuarioService service = _service;
    public async Task<LogarUsuarioCommandResponse> Handle(LogarUsuarioCommandRequest request, CancellationToken cancellationToken)
    {
        var token = await service.LogarUsuarioAsync(request.Email, request.Senha);
        return new LogarUsuarioCommandResponse(token);
    }
}
