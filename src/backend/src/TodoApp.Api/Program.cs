using TodoApp.Api.Configuration;
using TodoApp.Api.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAppServices(builder.Configuration);

var app = builder.Build();

app.ConfigureApp();

app.Run();