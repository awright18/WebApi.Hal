using System;

namespace WebApi.Hal.Tests
{
    public static class RemoveWhiteSpaceStringExtensions
    {
        public static string RemoveWhitespace(this string value)
        {
            if (value == null)
                throw new ArgumentException("value");

            var newValue = value
                .Replace("  ", " ")
                .Replace(" ", "")
                .Replace("\r", "")
                .Replace("\n", "")
                .Replace("\t", "");

            return newValue;
        }
    }
}
