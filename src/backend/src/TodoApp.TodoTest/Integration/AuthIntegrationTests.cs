using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using System.Net;
using System.Net.Http.Json;
using TodoApp.Api;
using TodoApp.Api.Features.Autenticacao.LogarUsuario.DTOs;
using TodoApp.Data;
using TodoApp.Data.Context;
using TodoApp.Domain.Entities;
using TodoApp.TodoTest; 
using Xunit;

namespace TodoApp.TodoTest.Integration;

public class AuthIntegrationTests : IClassFixture<CustomWebApplicationFactory<Program>>
{
    private readonly CustomWebApplicationFactory<Program> _factory;

    public AuthIntegrationTests(CustomWebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }

    [Fact]
    public async Task Post_Login_WithValidCredentials_ReturnsSuccess()
    {
        var client = _factory.CreateClient();

        using (var scope = _factory.Services.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            var testUser = new Usuario("user01010101","test@example.com", "validPassword123!");
            dbContext.Usuarios.Add(testUser);
            await dbContext.SaveChangesAsync();
        }

        var requestBody = new LogarUsuarioRequest("test@example.com", "validPassword123!");

        var response = await client.PostAsJsonAsync("/autenticacao/logar-usuario", requestBody);

        response.EnsureSuccessStatusCode();
        var responseBody = await response.Content.ReadFromJsonAsync<LogarUsuarioResponse>();
        Assert.NotNull(responseBody.Token);
    }

    [Fact]
    public async Task Post_Login_WithInvalidCredentials_ReturnsUnauthorized()
    {
        var client = _factory.CreateClient();
        var requestBody = new LogarUsuarioRequest("invalid@example.com", "wrongPassword");

        var response = await client.PostAsJsonAsync("/autenticacao/logar-usuario", requestBody);

        Assert.NotEqual(HttpStatusCode.OK, response.StatusCode);
    }
}