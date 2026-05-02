using ContactManager.Application.Features.Abstractions;
using ContactManager.Domain.Abstractions;
using ContactManager.Domain.Abstractions.Messaging;
using ContactManager.Domain.Results;
using ContactManager.Domain.Utilities;

namespace ContactManager.Application.Features.Commands.RemoveContact;

public sealed class RemoveContactCommandHandler(
    IContactRepository contactRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<RemoveContactCommand>
{
    public async Task<Result> Handle(RemoveContactCommand request, CancellationToken cancellationToken)
    {
        var contact = await contactRepository.GetByIdAsync(request.ContactId, cancellationToken);
        if (contact == null)
            return Result.Failure(ResponseList.ContactNotFound);
        
        contactRepository.Delete(contact);
        
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return Result.Success(ResponseList.ContactDeleted);
    }
}
