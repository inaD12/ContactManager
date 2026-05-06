namespace ContactManager.Application.Features.Abstractions;

public interface IUnitOfWork
{
	Task SaveChangesAsync(CancellationToken cancellationToken = default);
}