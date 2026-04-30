using ContactManager.Application.Features.Commands.CreateContact;
using ContactManager.Features.Models.Requests;

namespace ContactManager.Features.Mappers;

public static class CommandMapper
{
    public static CreateContactCommand ToCommand(
        this CreateContactRequest request)
        => new(
            request.FirstName,
            request.Surname,
            request.DateOfBirth,
            request.Address,
            request.PhoneNumber,
            request.IBAN);
}
  