using FluentValidation;

namespace ContactManager.Application.Features.Commands.RemoveContact;

public sealed class RemoveContactCommandValidator 
	: AbstractValidator<RemoveContactCommand>
{
	public RemoveContactCommandValidator()
	{
		RuleFor(x => x.ContactId)
			.NotEmpty()
			.Must(BeAValidGuid)
			.WithMessage("ContactId must be a valid GUID.");
	}

	private bool BeAValidGuid(string contactId)
	{
		return Guid.TryParse(contactId, out _);
	}
}