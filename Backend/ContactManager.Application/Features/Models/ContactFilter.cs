using ContactManager.Domain.Enums;

namespace ContactManager.Application.Features.Models;

public sealed record ContactFilter(
    string? FirstName,
    string? Surname,
    DateOnly? MinDateOfBirth,
    DateOnly? MaxDateOfBirth,
    string? Address,
    string? PhoneNumber,
    SortOrder SortOrder,
    string? SortPropertyName,
    int Page,
    int PageSize);