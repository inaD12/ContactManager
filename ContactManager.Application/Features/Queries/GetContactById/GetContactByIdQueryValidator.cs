using FluentValidation;

namespace ContactManager.Application.Features.Queries.GetContactById;

public class GetContactByIdQueryValidator : AbstractValidator<GetContactByIdQuery>
{
	public GetContactByIdQueryValidator()
	{
		RuleFor(x => x.Id)
			.NotEmpty()
			.Must(BeAValidGuid)
			.WithMessage("Id must be a valid GUID.");
	}

	private bool BeAValidGuid(string contactId)
	{
		return Guid.TryParse(contactId, out _);
	}
}
