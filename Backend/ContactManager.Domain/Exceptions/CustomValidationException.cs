using FluentValidation.Results;

namespace ContactManager.Domain.Exceptions;

public class CustomValidationException : Exception
{
	public IReadOnlyCollection<ValidationError> Errors { get; }

	public CustomValidationException(IEnumerable<ValidationFailure> failures)
		: base(CreateMessage(failures))
	{
		if (failures == null)
			throw new ArgumentNullException(nameof(failures));

		Errors = failures
			.Select(f => new ValidationError(f.PropertyName, f.ErrorMessage))
			.ToArray();
	}
	private static string CreateMessage(IEnumerable<ValidationFailure> failures)
	{
		var errors = failures?
			.Select(f => $"{f.PropertyName}: {f.ErrorMessage}")
			.ToArray() ?? Array.Empty<string>();

		return errors.Length > 0
			? $"Validation failed: {string.Join("; ", errors)}"
			: "Validation failed.";
	}

	public record ValidationError(string Property, string Message);
}
