using ContactManager.Application.Features.Models;
using ContactManager.Domain.Abstractions.Messaging;

namespace ContactManager.Application.Features.Commands.CreateContact;

public sealed record CreateContactCommand(
    string FirstName,
    string Surname,
    DateOnly DateOfBirth,
    string Address,
    string PhoneNumber,
    string IBAN) : ICommand<ContactCommandViewModel>;
