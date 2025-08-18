namespace TodoApp.Api.Features.Autenticacao.LogarUsuario.DTOs
{
    public class LogarUsuarioCommandResponse
    {
        public string Token { get; set; }
        public LogarUsuarioCommandResponse(string token)
        {
            Token = token;
        }
    }
}
