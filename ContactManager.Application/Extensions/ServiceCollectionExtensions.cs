using ContactManager.Application.Features.Commands.CreateContact;
using ContactManager.Application.Features.PipelineBehaviors;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace ContactManager.Application.Extensions;

public static class ServiceCollectionExtensions
{
	public static IServiceCollection AddApplicationLayer(this IServiceCollection services)
	{
		var currentAssembly = typeof(ServiceCollectionExtensions).Assembly;

		services
			.AddScoped<ContactFactory>();
		
		services
			.AddValidatorsFromAssembly(currentAssembly)
			.AddMediatR(
				cf =>
				{
					cf.RegisterServicesFromAssembly(currentAssembly);

					cf.AddOpenBehavior(typeof(ValidationPipelineBehavior<,>));
				});
		
		return services;
	}
}
