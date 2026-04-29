using ContactManager.Domain.Results;
using ContactManager.Domain.Utilities;

namespace ContactManager.Domain.ValueObjects;

public sealed record DateOfBirth
{
    public DateTime Value { get; }

    private DateOfBirth(DateTime value)
    {
        Value = value.Date;
    }

    public static Result<DateOfBirth> Create(DateTime value)
    {
        if (value > DateTime.UtcNow)
            return Result<DateOfBirth>.Failure(ResponseList.DateOfBirthInFuture);

        if (value < DateTime.UtcNow.AddYears(-120))
            return Result<DateOfBirth>.Failure(ResponseList.DateOfBirthTooOld);

        return Result<DateOfBirth>.Success(new DateOfBirth(value));
    }
}