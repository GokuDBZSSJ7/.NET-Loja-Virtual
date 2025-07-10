namespace Shared.Extensions;

public static class ValidationExtensions
{
    public static void ThrowIfNull<T>(this T? obj, string message)
    {
        if (obj == null)
            throw new ArgumentNullException(message);
    }

    public static void ThrowIf(bool condition, string message)
    {
        if (condition)
            throw new ArgumentException(message);
    }
}