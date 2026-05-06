
using ContactManager.Application.Features.Abstractions.Messaging;

namespace ContactManager.Application.Features.Commands.RemoveContact;

public sealed record RemoveContactCommand(
    string ContactId) : ICommand;