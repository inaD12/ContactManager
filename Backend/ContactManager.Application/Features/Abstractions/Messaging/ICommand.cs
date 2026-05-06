using ContactManager.Domain.Results;
using MediatR;

namespace ContactManager.Application.Features.Abstractions.Messaging;

public interface ICommand : IRequest<Result>
{
}

public interface ICommand<TResponse> : IRequest<Result<TResponse>>
{
}
