using ContactManager.Domain.Abstractions;
using Serilog;

namespace ContactManager.Extensions;

public static class HostExtensions
{
    public static void ConfigureSerilog(this IHostBuilder hostBuilder)
    {
        hostBuilder.UseSerilog((context, configuration) =>
            configuration
                .ReadFrom.Configuration(context.Configuration)
                .Enrich.FromLogContext()
        );
    }
    
    public static async Task SetUpDatabaseAsync(this IHost host)
    {
        using var scope = host.Services.CreateScope();

        var databaseInitializer = scope.ServiceProvider.GetRequiredService<IDatabaseInitializer>();

        await databaseInitializer.ApplyMigrationsAsync(scope);
    }
}