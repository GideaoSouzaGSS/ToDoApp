using TodoApp.Api.Features.Autenticacao.LogarUsuario.Endpoint;
using TodoApp.Api.Features.Autenticacao.RegistrarUsuario.Endpoint;

namespace TodoApp.Api.Middleware
{
    public static class ApplicationBuilderExtensions
    {
        public static void ConfigureApp(this WebApplication app)
        {
            app.UseMiddleware<ErrorHandlingMiddleware>();
            app.UseSwagger();
            app.UseSwaggerUI();
            app.MapGet("/", context =>
            {
                context.Response.Redirect("/swagger");
                return Task.CompletedTask;
            });
            app.UseHttpsRedirection();
            app.UseCors();

            app.MapRegistrarUsuarioEndpoint();
            app.MapLogarUsuarioEndpoint();
        }
    }
}
