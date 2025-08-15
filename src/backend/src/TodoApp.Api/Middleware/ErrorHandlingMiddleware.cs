using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using System.Net;
using System.Text.Json;

namespace TodoApp.Api.Middleware
{
    public class ErrorHandlingMiddleware(
            RequestDelegate next,
            ILogger<ErrorHandlingMiddleware> logger)
    {
        private readonly RequestDelegate _next = next;
        private readonly ILogger<ErrorHandlingMiddleware> _logger = logger;

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            _logger.LogError(exception, "Erro não tratado: {RequestPath} - {Message}",
                context.Request.Path, exception.Message);

            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            context.Response.ContentType = "application/problem+json";

            HttpStatusCode status;
            string message;
            var stackTrace = string.Empty;


            if (exception is DbUpdateException dbEx)
            {
                if (dbEx.InnerException is PostgresException pgEx && pgEx.SqlState == "23505")
                {
                    // Código de erro para violação de unicidade no PostgreSQL
                    status = HttpStatusCode.Conflict; // 409
                    message = "O e-mail ou nome de usuário já está em uso.";
                }
                else
                {
                    status = HttpStatusCode.InternalServerError; // 500
                    message = "Ocorreu um erro no banco de dados.";
                }
            }
            else
            {
                // Tipo de erro de negócio. Neste caso, o email em uso.
                if (exception is InvalidOperationException)
                {
                    status = HttpStatusCode.Conflict; // 409
                    message = exception.Message;
                }
                // Exemplo de outro erro.
                else if (exception is KeyNotFoundException)
                {
                    status = HttpStatusCode.NotFound; // 404
                    message = exception.Message;
                }
                else
                {
                    status = HttpStatusCode.InternalServerError; // 500
                    message = "Ocorreu um erro interno ao processar sua solicitação.";
                    stackTrace = exception.StackTrace; // Não mostrar em produção
                }
            }
            // Define o tipo de conteúdo da resposta
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)status;

            // Cria o objeto JSON da resposta
            var result = JsonSerializer.Serialize(new
            {
                status = (int)status,
                message,
                stackTrace
            });

            return context.Response.WriteAsync(result);
        }
    }
}
