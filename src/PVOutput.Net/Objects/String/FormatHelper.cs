using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace PVOutput.Net.Objects.String
{
    internal static class FormatHelper
    {
        internal static DateTime ParseDate(string dateString)
        {
            return DateTime.ParseExact(dateString, new string[] { "yyyyMMdd", "yyyyMM", "yyyy" }, CultureInfo.InvariantCulture.DateTimeFormat, DateTimeStyles.AssumeLocal);
        }

        internal static DateTime ParseTime(string timeString)
        {
            return DateTime.ParseExact(timeString, "HH:mm", CultureInfo.InvariantCulture.DateTimeFormat);
        }

        internal static decimal ParseNumeric(string numericString)
        {
            return Convert.ToDecimal(numericString, CultureInfo.CreateSpecificCulture("en-US"));
        }

        internal static string GetDateAsString(DateTime date)
        {
            return date.ToString("yyyyMMdd", CultureInfo.InvariantCulture);
        }

        internal static TResultType? ParseValue<TResultType>(string value) where TResultType : struct
        {
            if (string.IsNullOrEmpty(value) || value.Equals("NaN", StringComparison.OrdinalIgnoreCase))
            {
                return null;
            }

            return (TResultType)Convert.ChangeType(value, typeof(TResultType));
        }

        internal static TResultType ParseValueDefault<TResultType>(string value) where TResultType : struct
        {
            if (value.Equals("NaN", StringComparison.OrdinalIgnoreCase))
            {
                return default;
            }

            return (TResultType)Convert.ChangeType(value, typeof(TResultType));
        }
    }
}
