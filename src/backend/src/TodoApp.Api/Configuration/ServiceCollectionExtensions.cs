using Microsoft.Extensions.Configuration;
using TodoApp.Api.Features.Autenticacao.RegistrarUsuario.Endpoint;
using TodoApp.Application;
using TodoApp.Data;
using TodoApp.Data.Context;

namespace TodoApp.Api.Configuration
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddAppServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            
            services.AddPostgreSqlContexts(configuration);

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

        private static IServiceCollection AddPostgreSqlContexts(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddNpgsql<AppDbContext>(configuration.GetConnectionString("Aplicacao"));
            services.AddNpgsql<EventStoreDbContext>(configuration.GetConnectionString("Eventos"));
            return services;
        }
    }
}