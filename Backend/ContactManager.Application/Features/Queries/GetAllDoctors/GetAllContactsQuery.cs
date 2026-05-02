using ContactManager.Application.Features.Models;
using ContactManager.Domain.Abstractions.Messaging;
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
	string SortPropertyName) : IQuery<ContactPaginatedQueryViewModel>;
