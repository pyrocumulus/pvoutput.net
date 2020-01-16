using System;
using System.Globalization;

namespace PVOutput.Net.Objects.Core
{
    internal static class FormatHelper
    {
        internal static DateTime ParseDate(string dateString)
        {
            return DateTime.ParseExact(dateString, new string[] { "yyyyMMdd", "yyyyMM", "yyyy" }, CultureInfo.InvariantCulture.DateTimeFormat, DateTimeStyles.AssumeLocal);
        }

        internal static DateTimeOffset ParseTimeStamp(string timestamp)
        {
            return DateTimeOffset.Parse(timestamp, null, DateTimeStyles.RoundtripKind);
        }

        internal static DateTime? ParseOptionalDate(string dateString)
        {
            if (string.IsNullOrEmpty(dateString))
                return null;

            return ParseDate(dateString);
        }

        internal static DateTime ParseTime(string timeString)
        {
            return DateTime.ParseExact(timeString, "HH:mm", CultureInfo.InvariantCulture.DateTimeFormat);
        }

        internal static string GetDateAsString(DateTime date)
        {
            return date.ToString("yyyyMMdd", CultureInfo.InvariantCulture);
        }

        internal static string GetLocationAsString(decimal? latitude, decimal? longitude)
        {
            if (latitude == null || longitude == null)
            {
                return null;
            }

            string lat = latitude.Value.ToString("##.####", CultureInfo.CreateSpecificCulture("en-US"));
            string lon = longitude.Value.ToString("##.####", CultureInfo.CreateSpecificCulture("en-US"));

            return $"{lat},{lon}";
        }

        internal static string GetTimeAsString(DateTime date)
        {
            return date.ToString("HH:mm", CultureInfo.InvariantCulture.DateTimeFormat);
        }

        internal static TResultType? ParseValue<TResultType>(string value) where TResultType : struct
        {
            if (string.IsNullOrEmpty(value) || value.Equals("NaN", StringComparison.OrdinalIgnoreCase))
            {
                return null;
            }

            return (TResultType)Convert.ChangeType(value, typeof(TResultType), CultureInfo.CreateSpecificCulture("en-US"));
        }

        internal static string GetValueAsString<TInputType>(TInputType? value) where TInputType : struct
        {
            if (value == null)
            {
                return null;
            }

            return (string)Convert.ChangeType(value, typeof(string), CultureInfo.CreateSpecificCulture("en-US"));
        }

        internal static TResultType ParseValueWithDefault<TResultType>(string value) where TResultType : struct
        {
            TResultType? result = ParseValue<TResultType>(value);
            return result.HasValue ? result.Value : (default);
        }
    }
}
