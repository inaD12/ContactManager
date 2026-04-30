using ContactManager.Application.Features.Models;
using ContactManager.Domain.Abstractions.Messaging;

namespace ContactManager.Application.Features.Queries.GetContactById;

public sealed record GetContactByIdQuery(string Id) : IQuery<ContactQueryViewModel>;
