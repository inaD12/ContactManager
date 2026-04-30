using ContactManager.Features.ExceptionHandlers;
using ContactManager.Features.Options;
using ContactManager.Features.Utilities;
using Microsoft.OpenApi.Models;

namespace ContactManager.Extensions;

public static class ServiceCollectionExtensions
{
	public static IServiceCollection AddAPILayer(this IServiceCollection serviceCollection, IConfiguration configuration)
	{
		serviceCollection
			.AddSwagger()
			.ConfigureCors(configuration)
			.AddExceptionHandling()
			.AddEndpointsApiExplorer();

		return serviceCollection;
	}
	
	private static IServiceCollection AddSwagger(this IServiceCollection services)
	{
		services.AddSwaggerGen(options =>
		{
			options.SwaggerDoc("v1", new OpenApiInfo
			{
				Title = "Contact Manager API",
				Version = "v1"
			});
		});

		return services;
	}
	
	private static IServiceCollection ConfigureCors(this IServiceCollection services, IConfiguration configuration)
	{
		services
			.AddOptions<CorsOptions>()
			.BindConfiguration(nameof(CorsOptions))
			.ValidateDataAnnotations()
			.ValidateOnStart();

		var corsOptions = configuration
			.GetSection(nameof(CorsOptions))
			.Get<CorsOptions>()!;

		services.AddCors(options =>
		{
			options.AddDefaultPolicy(builder =>
			{
				builder.AllowAnyOrigin()
					.AllowAnyMethod()
					.AllowAnyHeader();
			});

			options.AddPolicy(AppPolicies.CorsPolicy, builder =>
				builder.WithOrigins(corsOptions.AllowedOrigins.Split(", "))
					.AllowAnyHeader()
					.AllowAnyMethod());
		});

		return services;
	}
	
	private static IServiceCollection AddExceptionHandling(this IServiceCollection services)
	{
		services
			.AddExceptionHandler<ValidationExceptionHandler>()
			.AddExceptionHandler<GlobalExceptionHandler>()
			.AddProblemDetails();

		return services;
	}
}
