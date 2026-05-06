using ContactManager.Application.Features.Abstractions.Messaging;
using ContactManager.Application.Features.Models;

namespace ContactManager.Application.Features.Queries.GetContactById;

public sealed record GetContactByIdQuery(string Id) : IQuery<ContactQueryViewModel>;
