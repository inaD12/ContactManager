using ContactManager.Application.Features.Models;
using ContactManager.Application.Features.Queries.GetAllDoctors;
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
    
    public static ContactFilter ToFilter(
        this GetAllContactsQuery query)
        => new(
            query.FirstName,
            query.Surname,
            query.MinDateOfBirth,
            query.MaxDateOfBirth,
            query.Address,
            query.PhoneNumber,
            query.SortOrder,
            query.SortPropertyName,
            query.Page,
            query.PageSize);
    
    public static ContactPaginatedQueryViewModel ToViewModel(
        this PagedList<Contact> pagedList)
        => new(
            pagedList.Items.Select(i => i.ToQueryViewModel()).ToList(),
            pagedList.Page,
            pagedList.PageSize,
            pagedList.TotalCount,
            pagedList.HasNextPage,
            pagedList.HasPreviousPage);
}