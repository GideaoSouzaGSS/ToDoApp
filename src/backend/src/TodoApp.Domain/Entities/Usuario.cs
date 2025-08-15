using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoApp.Domain.Entities
{
    public class Usuario
    {
        public Guid Id { get; set; }
        public string NomeUsuario { get; set; }
        public string NomeCompleto { get; set; }
        public string Email { get; set; }
        public DateTime? DataNascimento { get; set; }
        public string Senha { get; set; }
        public bool EmailConfirmado { get; set; }
        public string? CodigoConfirmacaoEmail { get; set; }
        public DateTime? DataGeracaoCodigoEmail { get; set; }

        public Usuario(string nomeUsuario, string email, string senha)
        {
            NomeUsuario = nomeUsuario;
            Email = email;
            Senha = HashSenha(senha); // Gerar hash no construtor
            EmailConfirmado = false; // Por padrão, email não está confirmado
            GerarCodigoConfirmacaoEmail(); // Gera código de confirmação inicial
        }

        private static string HashSenha(string senha)
        {
            // Implementação do hash (ex: BCrypt)
            return BCrypt.Net.BCrypt.HashPassword(senha);
        }

        public bool VerificarSenha(string senhaTentativa)
        {
            return BCrypt.Net.BCrypt.Verify(senhaTentativa, Senha);
        }

        public void GerarCodigoConfirmacaoEmail()
        {
            CodigoConfirmacaoEmail = Guid.NewGuid().ToString("N");
            DataGeracaoCodigoEmail = DateTime.UtcNow;
        }

    }
}
