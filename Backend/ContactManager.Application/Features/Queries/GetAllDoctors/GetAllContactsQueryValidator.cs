using ContactManager.Domain.Entities;
using ContactManager.Domain.Utilities;
using FluentValidation;

namespace ContactManager.Application.Features.Queries.GetAllDoctors;

public class GetAllContactsQueryValidator : AbstractValidator<GetAllContactsQuery>
{
	public GetAllContactsQueryValidator()
	{
		RuleFor(q => q.FirstName)
			.MaximumLength(ContactBusinessConfiguration.FIRST_NAME_MAX_LENGTH)
			.When(q => !string.IsNullOrWhiteSpace(q.FirstName));

		RuleFor(q => q.Surname)
			.MaximumLength(ContactBusinessConfiguration.SURNAME_MAX_LENGTH)
			.When(q => !string.IsNullOrWhiteSpace(q.Surname));

		RuleFor(q => q.Address)
			.MaximumLength(ContactBusinessConfiguration.ADDRESS_MAX_LENGTH)
			.When(q => !string.IsNullOrWhiteSpace(q.Address));

		RuleFor(q => q.PhoneNumber)
			.MaximumLength(ContactBusinessConfiguration.PHONE_MAX_LENGTH)
			.When(q => !string.IsNullOrWhiteSpace(q.PhoneNumber));

		RuleFor(q => q)
			.Must(q => !q.MinDateOfBirth.HasValue ||
			           !q.MaxDateOfBirth.HasValue ||
			           q.MinDateOfBirth <= q.MaxDateOfBirth)
			.WithMessage("MinDateOfBirth cannot be greater than MaxDateOfBirth");

		RuleFor(q => q.Page)
			.GreaterThan(0)
			.WithMessage("Page must be greater than 0");

		RuleFor(q => q.PageSize)
			.InclusiveBetween(1, 100)
			.WithMessage("PageSize must be between 1 and 100");

		RuleFor(q => q.SortBy)
			.IsInEnum();
	}
}