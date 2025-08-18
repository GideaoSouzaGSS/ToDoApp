using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoApp.Application.Interfaces
{
    public interface IUsuarioService
    {
        Task<Guid> RegistrarNovoUsuarioAsync(string nomeUsuario, string email, string senha);
        Task<string> LogarUsuarioAsync(string email, string senha);
    }
}
