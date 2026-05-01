using ContactManager.Application.Features.Models;
using ContactManager.Domain.Entities;

namespace ContactManager.Application.Features.Mappers;

public static class QueryMapper
{
    public static ContactQueryViewModel ToQueryViewModel(
        this Contact contact)
        => new(
            contact.Id,
            contact.FirstName.Value,
            contact.Surname.Value,
            contact.DateOfBirth.Value,
            contact.Address.Value,
            contact.PhoneNumber.Value,
            contact.IBAN.Value);
}