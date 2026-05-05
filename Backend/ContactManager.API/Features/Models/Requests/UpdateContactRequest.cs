using ContactManager.Domain.Abstractions.Messaging;

namespace ContactManager.Features.Models.Requests;

public sealed record UpdateContactRequest(
    string? NewFirstName,
    string? NewSurname,
    string? NewAddress,
    string? NewPhoneNumber,
    string? NewIBAN) : ICommand;
