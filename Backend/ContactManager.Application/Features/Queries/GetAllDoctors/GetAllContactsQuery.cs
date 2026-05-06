using ContactManager.Application.Features.Abstractions.Messaging;
using ContactManager.Application.Features.Models;
using ContactManager.Application.Features.Sorting;
using ContactManager.Domain.Enums;

namespace ContactManager.Application.Features.Queries.GetAllDoctors;

public sealed record GetAllContactsQuery(
	string? FirstName,
	string? Surname,
	DateOnly? MinDateOfBirth,
	DateOnly? MaxDateOfBirth,
	string? Address,
	string? PhoneNumber,
	SortOrder SortOrder,
	int Page,
	int PageSize,
	ContactSortField SortBy) : IQuery<ContactPaginatedQueryViewModel>;
