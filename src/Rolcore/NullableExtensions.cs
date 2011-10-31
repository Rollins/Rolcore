using System;

namespace Rolcore
{
    public static class NullableExtensions
    {
        public static string NullableToString<T>(this Nullable<T> value, string format, string nullResult) where T : struct
        {
            return (value.HasValue) ? string.Format(format, value.Value) : nullResult;
        }
    }
}
