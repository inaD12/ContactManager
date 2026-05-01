using ContactManager.Domain.Results;
using MediatR;

namespace ContactManager.Domain.Abstractions.Messaging;

public interface IQueryHandler<TQuery, TResponse>
	: IRequestHandler<TQuery, Result<TResponse>>
	where TQuery : IQuery<TResponse>
{
}
