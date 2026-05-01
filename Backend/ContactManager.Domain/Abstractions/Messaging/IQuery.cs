using ContactManager.Domain.Results;
using MediatR;

namespace ContactManager.Domain.Abstractions.Messaging;

public interface IQuery<Tresponse> : IRequest<Result<Tresponse>>
{
}
