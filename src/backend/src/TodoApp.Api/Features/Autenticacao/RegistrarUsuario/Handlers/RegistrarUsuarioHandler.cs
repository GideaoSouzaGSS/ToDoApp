using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TodoApp.Api.Features.Autenticacao.RegistrarUsuario.DTOs;
using TodoApp.Application;
using TodoApp.Application.Interfaces;

namespace TodoApp.Api.Features.Autenticacao.RegistrarUsuario.Handlers;
public class RegistrarUsuarioHandler(IUsuarioService usuarioService, IJwtService jwtService) : IRequestHandler<RegistrarUsuarioRequest, RegistrarUsuarioResponse>
{
    private readonly IUsuarioService _usuarioService = usuarioService;
    private readonly IJwtService _jwtService = jwtService;

    public async Task<RegistrarUsuarioResponse> Handle(RegistrarUsuarioRequest request, CancellationToken cancellationToken)
    {
        var usuarioId = await _usuarioService.RegistrarNovoUsuarioAsync(request.NomeUsuario, request.Email, request.Senha);

        var token = _jwtService.GerarToken(usuarioId, request.Email, request.NomeUsuario);

        return new RegistrarUsuarioResponse(token);

    }
}