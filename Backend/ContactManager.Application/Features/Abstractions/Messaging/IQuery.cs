using ContactManager.Domain.Results;
using MediatR;

namespace ContactManager.Application.Features.Abstractions.Messaging;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>
{
}
