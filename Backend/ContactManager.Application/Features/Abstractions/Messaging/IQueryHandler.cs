using ContactManager.Domain.Results;
using MediatR;

namespace ContactManager.Application.Features.Abstractions.Messaging;

public interface IQueryHandler<TQuery, TResponse>
	: IRequestHandler<TQuery, Result<TResponse>>
	where TQuery : IQuery<TResponse>
{
}
