using ContactManager.Application.Features.Abstractions.Messaging;
using ContactManager.Application.Features.Models;

namespace ContactManager.Application.Features.Commands.CreateContact;

public sealed record CreateContactCommand(
    string FirstName,
    string Surname,
    DateOnly DateOfBirth,
    string Address,
    string PhoneNumber,
    string IBAN) : ICommand<ContactCommandViewModel>;
