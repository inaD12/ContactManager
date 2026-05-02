using ContactManager.Application.Features.Abstractions;
using ContactManager.Domain.Abstractions;
using ContactManager.Domain.Entities;
using ContactManager.Domain.Results;
using ContactManager.Domain.Utilities;
using ContactManager.Domain.ValueObjects;

namespace ContactManager.Application.Features.Commands.CreateContact;

public sealed class ContactFactory
{
    public async Task<Result<Contact>> CreateAsync(
        CreateContactCommand request,
        IContactRepository repository,
        CancellationToken cancellationToken = default)
    {
        if (await repository.ExistsByPhoneAsync(request.PhoneNumber, cancellationToken))
            return Result<Contact>.Failure(ResponseList.PhoneNumberAlreadyExists);

        if (await repository.ExistsByIBANAsync(request.IBAN, cancellationToken))
            return Result<Contact>.Failure(ResponseList.IBANAlreadyExists);


        var firstName = FirstName.Create(request.FirstName);
        if (firstName.IsFailure)
            return Result<Contact>.Failure(firstName.Response);

        var surname = Surname.Create(request.Surname);
        if (surname.IsFailure)
            return Result<Contact>.Failure(surname.Response);

        var dob = DateOfBirth.Create(request.DateOfBirth);
        if (dob.IsFailure)
            return Result<Contact>.Failure(dob.Response);

        var address = Address.Create(request.Address);
        if (address.IsFailure)
            return Result<Contact>.Failure(address.Response);

        var phone = PhoneNumber.Create(request.PhoneNumber);
        if (phone.IsFailure)
            return Result<Contact>.Failure(phone.Response);

        var iban = IBAN.Create(request.IBAN);
        if (iban.IsFailure)
            return Result<Contact>.Failure(iban.Response);


        return Contact.Create(
            firstName.Value!,
            surname.Value!,
            dob.Value!,
            address.Value!,
            phone.Value!,
            iban.Value!
        );
    }
}