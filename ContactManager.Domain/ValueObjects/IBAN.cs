using ContactManager.Domain.Results;
using ContactManager.Domain.Utilities;

namespace ContactManager.Domain.ValueObjects;

public sealed record IBAN
{
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

        if (value.Length < 15 || value.Length > 34)
            return Result<IBAN>.Failure(ResponseList.IBANInvalid);

        return Result<IBAN>.Success(new IBAN(value));
    }
}