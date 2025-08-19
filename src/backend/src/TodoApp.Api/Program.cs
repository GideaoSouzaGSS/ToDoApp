using TodoApp.Api.Configuration;
using TodoApp.Api.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddDockerSecrets(
    "ConnectionStrings__Aplicacao",
    "ConnectionStrings__Eventos",
    "MassTransit__RabbitMQ__Password",
    "ConnectionStrings__AzureBlobStorage",
    "JWT_SECRET_KEY"
);

builder.Configuration.AddEnvironmentVariables();

builder.Services.AddAppServices(builder.Configuration, builder.Environment);

var app = builder.Build();

app.ConfigureApp();

app.Run();
public partial class Program { }