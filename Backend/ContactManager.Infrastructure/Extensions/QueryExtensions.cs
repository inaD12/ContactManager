using ContactManager.Application.Features.Sorting;
using ContactManager.Domain.Entities;
using ContactManager.Domain.Enums;

namespace ContactManager.Infrastructure.Extensions;

public static class QueryExtensions
{
    public static IQueryable<Contact> ApplySorting(
        this IQueryable<Contact> query,
        ContactSortField sortBy,
        SortOrder sortOrder)
    {
        return sortBy switch
        {
            ContactSortField.Id  =>
                sortOrder == SortOrder.ASC
                    ? query.OrderBy(x => x.Id)
                    : query.OrderByDescending(x => x.Id),
            
            ContactSortField.FirstName =>
                sortOrder == SortOrder.ASC
                    ? query.OrderBy(x => x.FirstName.Value)
                    : query.OrderByDescending(x => x.FirstName.Value),

            ContactSortField.Surname =>
                sortOrder == SortOrder.ASC
                    ? query.OrderBy(x => x.Surname.Value)
                    : query.OrderByDescending(x => x.Surname.Value),

            ContactSortField.DateOfBirth =>
                sortOrder == SortOrder.ASC
                    ? query.OrderBy(x => x.DateOfBirth.Value)
                    : query.OrderByDescending(x => x.DateOfBirth.Value),

            ContactSortField.Address =>
                sortOrder == SortOrder.ASC
                    ? query.OrderBy(x => x.Address.Value)
                    : query.OrderByDescending(x => x.Address.Value),
            
            ContactSortField.PhoneNumber =>
                sortOrder == SortOrder.ASC
                    ? query.OrderBy(x => x.PhoneNumber.Value)
                    : query.OrderByDescending(x => x.PhoneNumber.Value),
            
            _ => throw new ArgumentOutOfRangeException(nameof(sortBy), sortBy, null)
        };
    }
}	