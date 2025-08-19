using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using TodoApp.Data.Context;


namespace TodoApp.TodoTest;
public class CustomWebApplicationFactory<TProgram> : WebApplicationFactory<TProgram>, IDisposable where TProgram : class
{
    // 2. Crie um campo para guardar o caminho do arquivo temporário
    private readonly string _jwtSecretFilePath;
    public CustomWebApplicationFactory()
    {
        // 3. Crie o arquivo temporário e escreva o segredo nele
        _jwtSecretFilePath = Path.GetTempFileName();
        File.WriteAllText(_jwtSecretFilePath, "minha-chave-secreta-para-testes-que-e-bem-longa-e-segura");

    }
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {

        builder.ConfigureServices(services =>
        {
            // Remove a configuração do DbContext da aplicação principal
            var descriptor = services.SingleOrDefault(
                d => d.ServiceType ==
                    typeof(DbContextOptions<AppDbContext>)); // <-- Use o nome do seu DbContext aqui

            if (descriptor != null)
            {
                services.Remove(descriptor);
            }

            // Adiciona um DbContext usando um banco de dados em memória para os testes
            services.AddDbContext<AppDbContext>(options => // <-- Use o nome do seu DbContext aqui
            {
                options.UseInMemoryDatabase("InMemoryDbForTesting");
            });

            // Aqui você pode adicionar outros serviços mockados se precisar
        });
        builder.ConfigureAppConfiguration((context, config) =>
        {
            // Adiciona uma coleção de configurações em memória para os testes
            config.AddInMemoryCollection(new Dictionary<string, string?>
            {
                // Forneça aqui a chave que o seu código está esperando.
                // Se o erro é sobre JWT_SECRET_PATH, use essa chave.
                // Se você padronizar para JWT_SECRET_KEY, use a outra.
                // O valor aqui pode ser qualquer string, já que é só para teste.
                ["JWT_SECRET_KEY"] = "uma-chave-secreta-super-segura-para-testes-nao-use-em-prod-123456",
                ["JWT_SECRET_PATH"] = _jwtSecretFilePath
            });
        });
        builder.UseEnvironment("Test");
    }
    protected override void Dispose(bool disposing)
    {
        base.Dispose(disposing);
        if (File.Exists(_jwtSecretFilePath))
        {
            File.Delete(_jwtSecretFilePath);
        }
    }
}