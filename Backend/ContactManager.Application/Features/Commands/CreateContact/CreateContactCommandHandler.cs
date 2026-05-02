using ContactManager.Application.Features.Abstractions;
using ContactManager.Application.Features.Models;
using ContactManager.Domain.Abstractions;
using ContactManager.Domain.Abstractions.Messaging;
using ContactManager.Domain.Results;
using ContactManager.Domain.Utilities;

namespace ContactManager.Application.Features.Commands.CreateContact;

public sealed class CreateContactCommandHandler(
    ContactFactory factory,
    IContactRepository contactRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<CreateContactCommand, ContactCommandViewModel>
{
    public async Task<Result<ContactCommandViewModel>> Handle(
        CreateContactCommand request,
        CancellationToken cancellationToken)
    {
        var contactResult = await factory.CreateAsync(
            request,
            contactRepository,
            cancellationToken);

        if (contactResult.IsFailure)
            return Result<ContactCommandViewModel>.Failure(contactResult.Response);

        var contact = contactResult.Value!;

        await contactRepository.AddAsync(contact, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<ContactCommandViewModel>.Success(
            new ContactCommandViewModel(contact.Id),
            ResponseList.ContactCreated
        );
    }
}
