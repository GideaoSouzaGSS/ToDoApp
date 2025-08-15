using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TodoApp.Api.Features.Autenticacao.RegistrarUsuario.DTOs;
using TodoApp.Application;
using TodoApp.Application.Interfaces;

namespace TodoApp.Api.Features.Autenticacao.RegistrarUsuario.Commands;
public class RegistrarUsuarioCommandHandler : IRequestHandler<RegistrarUsuarioCommandRequest, RegistrarUsuarioCommandResponse>
{
    private readonly IUsuarioService _usuarioService;
    private readonly IJwtService _jwtService;

    public RegistrarUsuarioCommandHandler(IUsuarioService usuarioService, IJwtService jwtService)
    {
        _usuarioService = usuarioService;
        _jwtService = jwtService;
    }

    public async Task<RegistrarUsuarioCommandResponse> Handle(RegistrarUsuarioCommandRequest request, CancellationToken cancellationToken)
    {
        var usuarioId = await _usuarioService.RegistrarNovoUsuarioAsync(request.NomeUsuario, request.Email, request.Senha);

        var token = _jwtService.GerarToken(usuarioId, request.Email, request.NomeUsuario);

        return new RegistrarUsuarioCommandResponse(token);

    }
}