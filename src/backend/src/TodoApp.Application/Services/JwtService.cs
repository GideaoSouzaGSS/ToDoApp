using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using TodoApp.Application.Interfaces;

namespace TodoApp.Application.Services
{
    public class JwtService : IJwtService
    {
        private readonly string _secretKey;
        private const int ExpiracaoEmMinutos = 60;

        public JwtService(IConfiguration configuration)
        {
            _secretKey = configuration["JWT_SECRET_KEY"] 
                         ?? throw new ArgumentNullException("JWT_SECRET_KEY não configurada.");
        }

        public string GerarToken(Guid usuarioId, string email, string nomeUsuario)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, usuarioId.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, email),
                new Claim("nomeUsuario", nomeUsuario)
            };

            var token = new JwtSecurityToken(
                issuer: "TodoApp",
                audience: "TodoApp",
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(ExpiracaoEmMinutos),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}