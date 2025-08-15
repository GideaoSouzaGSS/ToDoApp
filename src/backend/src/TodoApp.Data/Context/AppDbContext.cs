using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using TodoApp.Domain.Entities;

namespace TodoApp.Data.Context;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public virtual DbSet<Usuario> Usuarios { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Usuario>(entity =>
        {
            // Mapeia a entidade 'Usuario' para a tabela 'Usuarios'
            entity.ToTable("Usuarios");

            // Configura a chave primária
            entity.HasKey(e => e.Id);

            // Adiciona restrições de unicidade
            entity.HasIndex(e => e.Email).IsUnique();
            entity.HasIndex(e => e.NomeUsuario).IsUnique();
        });
    }
}
