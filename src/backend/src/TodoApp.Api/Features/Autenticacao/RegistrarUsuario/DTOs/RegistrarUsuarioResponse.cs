namespace TodoApp.Api.Features.Autenticacao.RegistrarUsuario.DTOs
{
    public class RegistrarUsuarioResponse
    {
        public string Token { get; set; }
        public RegistrarUsuarioResponse(string token)
        {
            Token = token;
        }
    }
}
