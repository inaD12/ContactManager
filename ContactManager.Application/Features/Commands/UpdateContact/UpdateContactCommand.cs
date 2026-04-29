using ContactManager.Domain.Abstractions.Messaging;

namespace ContactManager.Application.Features.Commands.UpdateContact;

public sealed record UpdateContactCommand(
    string ContactId,
    string? NewAddress,
    string? NewPhoneNumber,
    string? NewIBAN) : ICommand;
