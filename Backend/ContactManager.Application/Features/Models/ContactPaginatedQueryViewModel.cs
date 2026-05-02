namespace ContactManager.Application.Features.Models;

public sealed record ContactPaginatedQueryViewModel(
	ICollection<ContactQueryViewModel> Items,
	int Page,
	int PageSize,
	int TotalCount,
	bool HasNextPage,
	bool HasPreviousPage);
