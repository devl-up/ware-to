using System.Reflection;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.EntityFrameworkCore;

internal sealed class WareToDbContext : DbContext
{
    public WareToDbContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}