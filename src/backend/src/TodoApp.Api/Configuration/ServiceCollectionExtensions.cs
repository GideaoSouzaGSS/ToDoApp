using Microsoft.Extensions.Configuration;
using TodoApp.Api.Features.Autenticacao.RegistrarUsuario.Endpoint;
using TodoApp.Application;
using TodoApp.Data;
using TodoApp.Data.Context;

namespace TodoApp.Api.Configuration
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddAppServices(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment environment)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            // Verifica se o ambiente é de teste
            if (!environment.IsEnvironment("Test"))
            {
                services.AddPostgreSqlContexts(configuration);
            }

            var applicationAssembly = typeof(ApplicationAssemblyMarker).Assembly;
            var dataAssembly = typeof(DataAssemblyMarker).Assembly;

            services.Scan(scan => scan
                .FromAssemblies(dataAssembly, applicationAssembly)
                .AddClasses(classes => classes.Where(c => c.Name.EndsWith("Repository") && !c.IsAbstract && !c.IsInterface))
                    .AsImplementedInterfaces()
                    .WithScopedLifetime()
                .AddClasses(classes => classes.Where(c => c.Name.EndsWith("Service") && !c.IsAbstract && !c.IsInterface))
                    .AsImplementedInterfaces()
                    .WithScopedLifetime());

            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));

            return services;
        }

        public static IServiceCollection AddPostgreSqlContexts(this IServiceCollection services, IConfiguration configuration)
        {
            // Obtém o caminho do arquivo de senha do secret
            var passwordSecretPath = configuration["DB_PASSWORD_PATH"];

            if (string.IsNullOrEmpty(passwordSecretPath) || !File.Exists(passwordSecretPath))
            {
                throw new InvalidOperationException("O caminho do secret de senha do banco de dados não foi encontrado ou o arquivo não existe.");
            }

            // Lê a senha do arquivo de secret
            var dbPassword = File.ReadAllText(passwordSecretPath).Trim();

            // 1. Contexto "Aplicacao"
            var aplicacaoBase = configuration["DB_CONNECTION_STRING_APLICACAO_BASE"];
            if (string.IsNullOrEmpty(aplicacaoBase))
            {
                throw new InvalidOperationException("A string de conexão base para a aplicação não foi encontrada.");
            }
            var aplicacaoConnectionString = $"{aplicacaoBase}Password={dbPassword}";
            services.AddNpgsql<AppDbContext>(aplicacaoConnectionString);

            // 2. Contexto "Eventos"
            var eventosBase = configuration["DB_CONNECTION_STRING_EVENTOS_BASE"];
            if (string.IsNullOrEmpty(eventosBase))
            {
                throw new InvalidOperationException("A string de conexão base para eventos não foi encontrada.");
            }
            var eventosConnectionString = $"{eventosBase}Password={dbPassword}";
            services.AddNpgsql<EventStoreDbContext>(eventosConnectionString);

            return services;
        }
    }
}