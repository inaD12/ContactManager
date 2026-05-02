using ContactManager.Application.Features.Models;

namespace ContactManager.Features.Models.Responses;

public sealed record ContactPaginatedQueryResponse(
ICollection<ContactQueryViewModel> Items,
	int Page,
	int PageSize,
	int TotalCount,
	bool HasNextPage,
	bool HasPreviousPage);