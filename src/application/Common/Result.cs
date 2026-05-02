namespace Doyep.Analyzer.Application;

/// <summary>
/// Represents the result of an operation, which can either be a success with a value of type T or a failure with an error of type TError.
/// </summary>
/// <typeparam name="T">The type of the value in case of success.</typeparam>
/// <typeparam name="TError">The type of the error in case of failure.</typeparam>
public class Result<T, TError>
{
    public bool IsSuccess { get; }
    public bool IsFailure => !IsSuccess;

    private readonly T? _value;
    public T Value => IsSuccess
        ? _value!
        : throw new InvalidOperationException("Cannot access Value when the result is a failure.");

    private readonly TError? _error;
    public TError Error => IsFailure
        ? _error!
        : throw new InvalidOperationException("Cannot access Error when the result is a success.");

    private Result(T value)
    {
        IsSuccess = true;
        _value = value;
        _error = default;
    }

    private Result(TError error)
    {
        IsSuccess = false;
        _error = error;
        _value = default;
    }

    public static Result<T, TError> Success(T value)
        => new(value);

    public static Result<T, TError> Failure(TError error)
        => new(error);
}

/// <summary>
/// Represents the result of an operation that can either be a success (with no value) or a failure with an error of type TError.
/// </summary>
/// <typeparam name="TError">The type of the error in case of failure.</typeparam>
public class Result<TError>
{
    public bool IsSuccess { get; }
    public bool IsFailure => !IsSuccess;
    private readonly TError? _error;

    public TError Error => IsFailure
        ? _error!
        : throw new InvalidOperationException("Cannot access Error when the result is a success.");

    private Result()
    {
        IsSuccess = true;
        _error = default;
    }

    private Result(TError error)
    {
        IsSuccess = false;
        _error = error;
    }

    public static Result<TError> Success()
        => new();

    public static Result<TError> Failure(TError error)
        => new(error);
}
