using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Infrastructure.EntityFrameworkCore.Services;

internal sealed class EfCoreDatabaseService : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;

    public EfCoreDatabaseService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await using var scope = _serviceProvider.CreateAsyncScope();
        var hostEnvironment = scope.ServiceProvider.GetRequiredService<IHostEnvironment>();

        if (!hostEnvironment.IsDevelopment())
        {
            return;
        }

        var dbContext = scope.ServiceProvider.GetRequiredService<WareToDbContext>();
        await dbContext.Database.MigrateAsync(stoppingToken);
    }
}