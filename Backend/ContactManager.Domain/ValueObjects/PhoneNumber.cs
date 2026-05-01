using System.Text.RegularExpressions;
using ContactManager.Domain.Results;
using ContactManager.Domain.Utilities;

namespace ContactManager.Domain.ValueObjects;


public sealed record PhoneNumber
{
    private static readonly Regex PhoneRegex =
        new(@"^\+?[0-9]{7,15}$", RegexOptions.Compiled);

    public string Value { get; }

    private PhoneNumber(string value)
    {
        Value = value;
    }

    public static Result<PhoneNumber> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return Result<PhoneNumber>.Failure(ResponseList.PhoneNumberRequired);

        value = value.Replace(" ", "");

        if (!PhoneRegex.IsMatch(value))
            return Result<PhoneNumber>.Failure(ResponseList.PhoneNumberInvalid);

        return Result<PhoneNumber>.Success(new PhoneNumber(value));
    }
}