using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoApp.Domain.Entities;

namespace TodoApp.Data.Context;
public class EventStoreDbContext : DbContext
{
    public DbSet<EventData> Events { get; set; }

    public EventStoreDbContext(DbContextOptions<EventStoreDbContext> options) : base(options) { }
}