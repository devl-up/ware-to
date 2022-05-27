using Infrastructure.Exceptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.EntityFrameworkCore.Extensions;

internal static class ServiceCollectionExtensions
{
    public static void AddEntityFrameworkCore(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Postgres");

        if (string.IsNullOrWhiteSpace(connectionString))
        {
            throw new InfrastructureException("Connection string for postgres not found.");
        }

        services.AddDbContext<WareToDbContext>(options => options.UseNpgsql(connectionString));
    }
}