using ContactManager.Domain.Utilities;
using FluentValidation;

namespace ContactManager.Application.Features.Commands.CreateContact;

public class CreateContactCommandValidator : AbstractValidator<CreateContactCommand>
{
	public CreateContactCommandValidator()
	{
		RuleFor(x => x.FirstName)
			.NotEmpty()
			.MinimumLength(ContactBusinessConfiguration.FIRST_NAME_MIN_LENGTH)
			.MaximumLength(ContactBusinessConfiguration.FIRST_NAME_MAX_LENGTH);

		RuleFor(x => x.Surname)
			.NotEmpty()
			.MinimumLength(ContactBusinessConfiguration.LAST_NAME_MIN_LENGTH)
			.MaximumLength(ContactBusinessConfiguration.LAST_NAME_MAX_LENGTH);

		RuleFor(x => x.DateOfBirth)
			.Must(dob => dob < DateOnly.FromDateTime(DateTime.UtcNow))
			.WithMessage("Date of birth must be in the past.");

		RuleFor(x => x.Address)
			.NotEmpty()
			.MinimumLength(ContactBusinessConfiguration.ADDRESS_MIN_LENGTH)
			.MaximumLength(ContactBusinessConfiguration.ADDRESS_MAX_LENGTH);

		RuleFor(x => x.PhoneNumber)
			.NotEmpty()
			.MinimumLength(ContactBusinessConfiguration.PHONE_MIN_LENGTH)
			.MaximumLength(ContactBusinessConfiguration.PHONE_MAX_LENGTH)
			.Matches(@"^\+?[0-9]{7,15}$")
			.WithMessage("Phone number format is invalid.");

		RuleFor(x => x.IBAN)
			.NotEmpty()
			.MinimumLength(ContactBusinessConfiguration.IBAN_MIN_LENGTH)
			.MaximumLength(ContactBusinessConfiguration.IBAN_MAX_LENGTH)
			.Matches(@"^[A-Z]{2}[0-9]{2}[A-Z0-9]{11,30}$")
			.WithMessage("IBAN format is invalid.");
	}
}