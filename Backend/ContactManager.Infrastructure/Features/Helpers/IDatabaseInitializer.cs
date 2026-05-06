using Microsoft.Extensions.DependencyInjection;

namespace ContactManager.Infrastructure.Features.Helpers;

public interface IDatabaseInitializer
{
	Task ApplyMigrationsAsync(IServiceScope scope);
}