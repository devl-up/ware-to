using Application.Catalog.Queries;
using Application.Spaces.Queries;
using Domain.Catalog.Repositories;
using Domain.Spaces.Repositories;
using Infrastructure.EntityFrameworkCore.Queries;
using Infrastructure.EntityFrameworkCore.Repositories;
using Infrastructure.EntityFrameworkCore.Services;
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

        services.AddTransient<IProductRepository, ProductRepository>();
        services.AddTransient<ISpaceRepository, SpaceRepository>();
        services.AddTransient<IProductQueries, ProductQueries>();
        services.AddTransient<ISpaceQueries, SpaceQueries>();

        services.AddHostedService<EfCoreDatabaseService>();
    }
}