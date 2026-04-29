using System.Text.RegularExpressions;
using ContactManager.Domain.Results;
using ContactManager.Domain.Utilities;

namespace ContactManager.Domain.ValueObjects;


public sealed record PhoneNumber
{
    public string Value { get; }

    private PhoneNumber(string value)
    {
        Value = value;
    }

    public static Result<PhoneNumber> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return Result<PhoneNumber>.Failure(ResponseList.PhoneNumberRequired);

        if (!Regex.IsMatch(value, @"^\+?[0-9]{7,15}$"))
            return Result<PhoneNumber>.Failure(ResponseList.PhoneNumberInvalid);

        return Result<PhoneNumber>.Success(new PhoneNumber(value));
    }
}