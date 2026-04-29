using ContactManager.Domain.Abstractions;
using ContactManager.Domain.Abstractions.Messaging;
using ContactManager.Domain.Results;
using ContactManager.Domain.Utilities;
using ContactManager.Domain.ValueObjects;

namespace ContactManager.Application.Features.Commands.UpdateContact;

public sealed class UpdateContactCommandHandler(
    IContactRepository contactRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<UpdateContactCommand>
{
    public async Task<Result> Handle(UpdateContactCommand request, CancellationToken cancellationToken)
    {
        var contact = await contactRepository.GetByIdAsync(request.Id, cancellationToken);

        if (contact is null)
            return Result.Failure(ResponseList.ContactNotFound);

        if (request.NewAddress is not null)
        {
            var address = Address.Create(request.NewAddress);
            if (address.IsFailure)
                return Result.Failure(address.Response);

            var changResult = contact.ChangeAddress(address.Value!);
            if (changResult.IsFailure)
                return Result.Failure(changResult.Response);
        }

        if (request.NewPhoneNumber is not null)
        {
            var phoneNumber = PhoneNumber.Create(request.NewPhoneNumber);
            if (phoneNumber.IsFailure)
                return Result.Failure(phoneNumber.Response);

            var changResult = contact.ChangePhoneNumber(phoneNumber.Value!);
            if (changResult.IsFailure)
                return Result.Failure(changResult.Response);
        }
        
        if (request.NewIBAN is not null)
        {
            var iban = IBAN.Create(request.NewIBAN);
            if (iban.IsFailure)
                return Result.Failure(iban.Response);

            var changResult = contact.ChangeIBAN(iban.Value!);
            if (changResult.IsFailure)
                return Result.Failure(changResult.Response);
        }

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success(ResponseList.ContactUpdated);
    }
}
