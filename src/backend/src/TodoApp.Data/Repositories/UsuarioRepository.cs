using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoApp.Data.Context;
using TodoApp.Domain.Entities;
using TodoApp.Domain.Repositories;

namespace TodoApp.Data.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly AppDbContext context;
        public UsuarioRepository(AppDbContext _context)
        {
            context = _context;
        }
        public async Task RegistrarNovoUsuarioAsync(Usuario usuario)
        {
            await context.Usuarios.AddAsync(usuario);
            await context.SaveChangesAsync();
        }

        public async Task<bool> ExisteUsuarioComEmailAsync(string email)
        {
            var result = await context.Usuarios.AnyAsync(u => u.Email == email);
            return result;
        }

        public Task<bool> NomeUsuarioExisteAsync(string username)
        {
            return context.Usuarios.AnyAsync(u => u.NomeUsuario == username);
        }

        public async Task<Usuario?> ObterUsuarioPorEmailAsync(string email)
        {
            var user = await context.Usuarios.FirstOrDefaultAsync(u => u.Email == email);
            return user;
        }
    }
}
