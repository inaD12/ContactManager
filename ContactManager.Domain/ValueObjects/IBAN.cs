using System.Text.RegularExpressions;
using ContactManager.Domain.Results;
using ContactManager.Domain.Utilities;

namespace ContactManager.Domain.ValueObjects;

public sealed record IBAN
{
    private static readonly Regex IbanRegex =
        new(@"^[A-Z]{2}[0-9]{2}[A-Z0-9]{11,30}$", RegexOptions.Compiled);

    public string Value { get; }

    private IBAN(string value)
    {
        Value = value;
    }

    public static Result<IBAN> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return Result<IBAN>.Failure(ResponseList.IBANRequired);

        value = value.Replace(" ", "").ToUpperInvariant();

        if (!IbanRegex.IsMatch(value))
            return Result<IBAN>.Failure(ResponseList.IBANInvalid);

        return Result<IBAN>.Success(new IBAN(value));
    }
}