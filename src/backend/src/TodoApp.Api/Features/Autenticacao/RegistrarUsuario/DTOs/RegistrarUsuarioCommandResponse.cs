namespace TodoApp.Api.Features.Autenticacao.RegistrarUsuario.DTOs
{
    public class RegistrarUsuarioCommandResponse
    {
        public string Token { get; set; }
        public RegistrarUsuarioCommandResponse(string token)
        {
            Token = token;
        }
    }
}
