using ContactManager.Domain.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace ContactManager.Infrastructure.Features.UnitOfWork;

internal class UnitOfWork<TContext>(TContext dbContext) : IUnitOfWork
	where TContext : DbContext
{
	public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
	{
		try
		{
			await dbContext.SaveChangesAsync(cancellationToken);
		}
		catch (DbUpdateConcurrencyException ex)
		{
			throw new Domain.Exceptions.ConcurrencyException("Concurrency exception occurred.", ex);
		}
	}
}