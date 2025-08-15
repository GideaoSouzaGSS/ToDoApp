using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoApp.Application.Interfaces;
using TodoApp.Domain.Entities;
using TodoApp.Domain.Repositories;

namespace TodoApp.Application.Services;

public class UsuarioService(IUsuarioRepository usuarioRepository) : IUsuarioService
{
    private readonly IUsuarioRepository _usuarioRepository = usuarioRepository;

    public async Task<Guid> RegistrarNovoUsuarioAsync(string nomeUsuario, string email, string senha)
    {
        var emailEmUso = await _usuarioRepository.ExisteUsuarioComEmailAsync(email);
        if (emailEmUso)
        {
            throw new InvalidOperationException("Email já está em uso.");
        }

        var nomeUsuarioEmUso = await _usuarioRepository.NomeUsuarioExisteAsync(nomeUsuario);
        if (nomeUsuarioEmUso)
        {
            throw new InvalidOperationException("Nome de usuário já está em uso.");
        }

        var usuario = new Usuario(nomeUsuario, email, senha);

        await _usuarioRepository.RegistrarNovoUsuarioAsync(usuario);
        return usuario.Id;
    }
}

