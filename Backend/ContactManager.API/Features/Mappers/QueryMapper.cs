using ContactManager.Application.Features.Models;
using ContactManager.Application.Features.Queries.GetAllDoctors;
using ContactManager.Features.Models.Requests;
using ContactManager.Features.Models.Responses;

namespace ContactManager.Features.Mappers;

public static class QueryMapper
{
    public static ContactQueryResponse ToResponse(
        this ContactQueryViewModel viewModel)
        => new(
            viewModel.Id,
            viewModel.FirstName,
            viewModel.Surname,
            viewModel.DateOfBirth,
            viewModel.Address,
            viewModel.PhoneNumber,
            viewModel.IBAN);

    public static GetAllContactsQuery ToQuery(
        this GetAllContactsRequest request)
        => new(
            request.FirstName,
            request.Surname,
            request.MinDateOfBirth,
            request.MaxDateOfBirth,
            request.Adress,
            request.PhoneNumber,
            request.SortOrder,
            request.Page,
            request.PageSize,
            request.SortBy);
    
    public static ContactPaginatedQueryResponse ToResponse(
        this ContactPaginatedQueryViewModel viewModel)
        => new(
            viewModel.Items,
            viewModel.Page,
            viewModel.PageSize,
            viewModel.TotalCount,
            viewModel.HasNextPage,
            viewModel.HasPreviousPage);

}
  