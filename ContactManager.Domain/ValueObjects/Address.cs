using ContactManager.Domain.Results;
using ContactManager.Domain.Utilities;

namespace ContactManager.Domain.ValueObjects;

public sealed record Address
{
    public string Value { get; }

    private Address(string value)
    {
        Value = value;
    }

    public static Result<Address> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return Result<Address>.Failure(ResponseList.AddressRequired);

        if (value.Length > 250)
            return Result<Address>.Failure(ResponseList.AddressTooLong);

        return Result<Address>.Success(new Address(value.Trim()));
    }
}