using ContactManager.Domain.Results;
using MediatR;

namespace ContactManager.Domain.Abstractions.Messaging;

public interface ICommandHandler<TCommand> : IRequestHandler<TCommand, Result>
	where TCommand : ICommand
{
}

public interface ICommandHandler<TCommand, TResponse>
	: IRequestHandler<TCommand, Result<TResponse>>
	where TCommand : ICommand<TResponse>
{
}
