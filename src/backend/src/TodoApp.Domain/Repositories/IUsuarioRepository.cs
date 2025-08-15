using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoApp.Domain.Entities;

namespace TodoApp.Domain.Repositories;

public interface IUsuarioRepository
{
    Task<bool> ExisteUsuarioComEmailAsync(string email);
    Task<bool> NomeUsuarioExisteAsync(string username);

    Task RegistrarNovoUsuarioAsync(Usuario usuario);
    Task<Usuario?> ObterUsuarioPorEmailAsync(string email);
}
