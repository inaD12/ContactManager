using ContactManager.Application.Features.Sorting;
using ContactManager.Domain.Enums;

namespace ContactManager.Features.Models.Requests;

public record GetAllContactsRequest(
	string FirstName = "",
	string Surname = "",
	DateOnly? MinDateOfBirth = null,
	DateOnly? MaxDateOfBirth = null,
	string Address = "",
	string PhoneNumber = "",
	SortOrder SortOrder = SortOrder.ASC,
	ContactSortField SortBy = ContactSortField.Id,
	int Page = 1,
	int PageSize = 10
);
