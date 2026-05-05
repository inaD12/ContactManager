using ContactManager.Application.Features.Abstractions;
using ContactManager.Application.Features.Mappers;
using ContactManager.Application.Features.Models;
using ContactManager.Domain.Abstractions;
using ContactManager.Domain.Abstractions.Messaging;
using ContactManager.Domain.Results;
using ContactManager.Domain.Utilities;

namespace ContactManager.Application.Features.Queries.GetContactById;

public sealed class GetContactByIdQueryHandler(IContactRepository contactRepository)
	: IQueryHandler<GetContactByIdQuery, ContactQueryViewModel>
{
	public async Task<Result<ContactQueryViewModel>> Handle(GetContactByIdQuery request, CancellationToken cancellationToken)
	{
		var contact = await contactRepository.GetByIdAsync(request.Id, cancellationToken);
		if (contact == null)
			return Result<ContactQueryViewModel>.Failure(ResponseList.ContactNotFound);

		var contactQueryViewModel = contact.ToQueryViewModel();
		return Result<ContactQueryViewModel>.Success(contactQueryViewModel);
	}
}
