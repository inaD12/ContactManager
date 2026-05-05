using ContactManager.Application.Features.Abstractions;
using ContactManager.Application.Features.Mappers;
using ContactManager.Application.Features.Models;
using ContactManager.Domain.Abstractions.Messaging;
using ContactManager.Domain.Results;
using ContactManager.Domain.Utilities;

namespace ContactManager.Application.Features.Queries.GetAllDoctors;

public sealed class GetAllContactsQueryHandler(
	IContactRepository doctorRepository)
	: IQueryHandler<GetAllContactsQuery, ContactPaginatedQueryViewModel>
{

	public async Task<Result<ContactPaginatedQueryViewModel>> Handle(GetAllContactsQuery request, CancellationToken cancellationToken)
	{
		var contactPagedListQuery = request.ToFilter();
		var contactsPagedList = await doctorRepository.GetAllAsync(contactPagedListQuery, cancellationToken);
		if (!contactsPagedList.Items.Any())
			return Result<ContactPaginatedQueryViewModel>.Failure(ResponseList.ContactsNotFound);

		var contactPaginatedQueryViewModel = contactsPagedList.ToViewModel();
		return Result<ContactPaginatedQueryViewModel>.Success(contactPaginatedQueryViewModel);
	}
}
