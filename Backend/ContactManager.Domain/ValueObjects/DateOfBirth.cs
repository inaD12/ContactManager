using ContactManager.Domain.Results;
using ContactManager.Domain.Utilities;

namespace ContactManager.Domain.ValueObjects;

public sealed record DateOfBirth
{
    public DateOnly Value { get; }

    private DateOfBirth(DateOnly value)
    {
        Value = value;
    }

    public static Result<DateOfBirth> Create(DateOnly value)
    {
        if (value > DateOnly.FromDateTime(DateTime.UtcNow))
            return Result<DateOfBirth>.Failure(ResponseList.DateOfBirthInFuture);

        if (value < DateOnly.FromDateTime(DateTime.UtcNow.AddYears(-120)))
            return Result<DateOfBirth>.Failure(ResponseList.DateOfBirthTooOld);

        return Result<DateOfBirth>.Success(new DateOfBirth(value));
    }
}