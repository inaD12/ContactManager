using Microsoft.Extensions.DependencyInjection;

namespace ContactManager.Domain.Abstractions;

public interface IDatabaseInitializer
{
	Task ApplyMigrationsAsync(IServiceScope scope);
}