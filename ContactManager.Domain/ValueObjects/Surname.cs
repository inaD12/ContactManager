using ContactManager.Domain.Results;
using ContactManager.Domain.Utilities;

namespace ContactManager.Domain.ValueObjects;



public sealed record Surname
{
    public string Value { get; }

    private Surname(string value)
    {
        Value = value;
    }

    public static Result<Surname> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return Result<Surname>.Failure(ResponseList.SurnameRequired);

        return Result<Surname>.Success(new Surname(value.Trim()));
    }
}