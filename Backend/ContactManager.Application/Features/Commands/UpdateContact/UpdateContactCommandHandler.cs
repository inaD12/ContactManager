using ContactManager.Application.Features.Abstractions;
using ContactManager.Application.Features.Abstractions.Messaging;
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
        var contact = await contactRepository.GetByIdAsync(request.ContactId, cancellationToken);

        if (contact is null)
            return Result.Failure(ResponseList.ContactNotFound);

        if (request.NewFirstName is not null)
        {
            var firstName = FirstName.Create(request.NewFirstName);
            if (firstName.IsFailure)
                return Result.Failure(firstName.Response);

            var changResult = contact.ChangeFirstName(firstName.Value!);
            if (changResult.IsFailure)
                return Result.Failure(changResult.Response);
        }
        
        if (request.NewSurname is not null)
        {
            var surname = Surname.Create(request.NewSurname);
            if (surname.IsFailure)
                return Result.Failure(surname.Response);

            var changResult = contact.ChangeSurname(surname.Value!);
            if (changResult.IsFailure)
                return Result.Failure(changResult.Response);
        }
        
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
