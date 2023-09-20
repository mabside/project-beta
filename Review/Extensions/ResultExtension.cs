using Review.Models.Bases;

namespace Review.Extensions;

public static class ResultExtension
{
    public static Result<T> Ensure<T>(this Result<T> result, Func<T, bool> precondition, Error preconditionError)
    {
        if (result.Value == null || result.HasError)
            return result.Error!;

        return !precondition(result.Value)
            ? preconditionError :
            result.Value!;
    }

    public static Result<T> Validate<T, TValueObject>(this Result<T> result, Result<TValueObject> value)
    {
        if (result.Value == null || result.HasError)
            return result.Error!;

        return value.HasError ? value.Error! : result.Value;
    }

    public static Result<T> Apply<T>(this Result<T> result, Func<T, Result> precondition)
    {
        return result.Ensure(r =>
        !precondition(result.Value!).HasError,
        precondition(result.Value!).Error!);
    }

    public static Result<T> Apply<T>(this Result<T> result, Func<T, Result<T>> precondition)
    {
        if (result.HasError)
            return result.Error!;

        return result.Ensure(r =>
        !precondition(result.Value!).HasError,
        precondition(result.Value!).Error!);
    }
}
