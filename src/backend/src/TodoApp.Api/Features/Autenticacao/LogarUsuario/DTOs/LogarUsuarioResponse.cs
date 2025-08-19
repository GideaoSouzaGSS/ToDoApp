namespace TodoApp.Api.Features.Autenticacao.LogarUsuario.DTOs
{
    public class LogarUsuarioResponse(string token)
    {
        public string Token { get; private set; } = token;
    }
}
