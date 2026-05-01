using ContactManager.Domain.Abstractions.Messaging;

namespace ContactManager.Features.Models.Requests;

public sealed record UpdateContactRequest(
    string? NewAddress,
    string? NewPhoneNumber,
    string? NewIBAN) : ICommand;
