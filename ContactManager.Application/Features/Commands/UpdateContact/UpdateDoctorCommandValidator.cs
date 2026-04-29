using ContactManager.Domain.Utilities;
using FluentValidation;

namespace ContactManager.Application.Features.Commands.UpdateContact;

public class UpdateContactCommandValidator : AbstractValidator<UpdateContactCommand>
{
	public UpdateContactCommandValidator()
	{
		RuleFor(x => x)
			.Must(HasAtLeastOneField)
			.WithMessage("At least one field must be changed.");

		RuleFor(x => x.NewAddress)
			.MinimumLength(ContactBusinessConfiguration.ADDRESS_MIN_LENGTH)
			.MaximumLength(ContactBusinessConfiguration.ADDRESS_MAX_LENGTH)
			.When(x => HasValue(x.NewAddress));

		RuleFor(x => x.NewPhoneNumber)
			.MinimumLength(ContactBusinessConfiguration.PHONE_MIN_LENGTH)
			.MaximumLength(ContactBusinessConfiguration.PHONE_MAX_LENGTH)
			.Matches(@"^\+?[0-9]{7,15}$")
			.WithMessage("Phone number format is invalid.")
			.When(x => HasValue(x.NewPhoneNumber));

		RuleFor(x => x.NewIBAN)
			.MinimumLength(ContactBusinessConfiguration.IBAN_MIN_LENGTH)
			.MaximumLength(ContactBusinessConfiguration.IBAN_MAX_LENGTH)
			.Matches(@"^[A-Z]{2}[0-9]{2}[A-Z0-9]{11,30}$")
			.WithMessage("IBAN format is invalid.")
			.When(x => HasValue(x.NewIBAN));
	}

	private bool HasValue(string? value)
		=> !string.IsNullOrWhiteSpace(value);

	private bool HasAtLeastOneField(UpdateContactCommand x)
	{
		return HasValue(x.NewAddress)
		       || HasValue(x.NewPhoneNumber)
		       || HasValue(x.NewIBAN);
	}
}