using ContactManager.Domain.Abstractions;
using ContactManager.Domain.Options;
using ContactManager.Infrastructure.Features.DBContexts;
using ContactManager.Infrastructure.Features.Helpers;
using ContactManager.Infrastructure.Features.Repositories;
using ContactManager.Infrastructure.Features.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ContactManager.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
	public static IServiceCollection AddInfrastructureLayer(this IServiceCollection services, IConfiguration configuration)
	{
		services
			.AddScoped<IContactRepository, ContactRepository>()
			.AddScoped<IDatabaseInitializer, DatabaseInitializer>()
			.AddScoped<IUnitOfWork, UnitOfWork<ContactsDbContext>>();

		services
			.AddOptions<DatabaseOptions>()
			.BindConfiguration(nameof(DatabaseOptions))
			.ValidateDataAnnotations()
			.ValidateOnStart();

		var databaseOptions = configuration
			.GetSection(nameof(DatabaseOptions))
			.Get<DatabaseOptions>()!;

		services.AddDbContext<ContactsDbContext>(options =>
		{
			options.UseNpgsql(
				databaseOptions.ConnectionString,
				npgsqlOptions =>
				{
					npgsqlOptions.EnableRetryOnFailure();
				});
		});

		services
			.AddHealthChecks()
			.AddDbContextCheck<ContactsDbContext>();
		
		return services;
	}
}
