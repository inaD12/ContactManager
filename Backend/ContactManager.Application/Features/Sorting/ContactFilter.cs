using ContactManager.Domain.Enums;

namespace ContactManager.Application.Features.Sorting;

public sealed record ContactFilter(
    string? FirstName,
    string? Surname,
    DateOnly? MinDateOfBirth,
    DateOnly? MaxDateOfBirth,
    string? Address,
    string? PhoneNumber,
    SortOrder SortOrder,
    ContactSortField SortBy,
    int Page,
    int PageSize);