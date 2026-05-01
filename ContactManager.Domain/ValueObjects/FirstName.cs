using ContactManager.Domain.Results;
using ContactManager.Domain.Utilities;

namespace ContactManager.Domain.ValueObjects;

public sealed record FirstName
{
    public string Value { get; }

    private FirstName(string value)
    {
        Value = value;
    }

    public static Result<FirstName> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return Result<FirstName>.Failure(ResponseList.FirstNameRequired);

        if (value.Length > 50)
            return Result<FirstName>.Failure(ResponseList.FirstNameTooLong);

        return Result<FirstName>.Success(new FirstName(value.Trim()));
    }
}