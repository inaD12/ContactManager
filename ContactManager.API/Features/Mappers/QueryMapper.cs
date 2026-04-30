using ContactManager.Application.Features.Models;
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

}
  