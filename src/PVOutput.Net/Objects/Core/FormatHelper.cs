using System;
using System.ComponentModel;
using System.Globalization;
using System.Net;
using System.Reflection;
using PVOutput.Net.Enums;

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

        internal static string GetCoordinateAsString(PVCoordinate coordinate)
        {
            var lat = coordinate.Latitude.ToString("##.####", CultureInfo.CreateSpecificCulture("en-US"));
            var lon = coordinate.Longitude.ToString("##.####", CultureInfo.CreateSpecificCulture("en-US"));

            return $"{lat},{lon}";
        }

        internal static string GetLocationAsString(double? latitude, double? longitude)
        {
            if (latitude == null || longitude == null)
            {
                return null;
            }

            var lat = latitude.Value.ToString("##.####", CultureInfo.CreateSpecificCulture("en-US"));
            var lon = longitude.Value.ToString("##.####", CultureInfo.CreateSpecificCulture("en-US"));

            return $"{lat},{lon}";
        }

        internal static string GetTimeAsString(DateTime date)
        {
            return date.ToString("HH:mm", CultureInfo.InvariantCulture.DateTimeFormat);
        }

        internal static string GetValueAsString<TInputType>(TInputType? value) where TInputType : struct
        {
            if (value == null)
            {
                return null;
            }

            return (string)Convert.ChangeType(value, typeof(string), CultureInfo.CreateSpecificCulture("en-US"));
        }

        internal static string UrlEncode(string value)
        {
            if (value == null)
            {
                return null;
            }

            // + is only a valid character for encoding space in application/x-www-form-urlencoded content
            return WebUtility.UrlEncode(value).Replace("+", "%20");
        }

        internal static TResultType? GetValue<TResultType>(string value) where TResultType : struct
        {
            if (string.IsNullOrEmpty(value) || value.Equals("NaN", StringComparison.OrdinalIgnoreCase))
            {
                return null;
            }

            return (TResultType)Convert.ChangeType(value, typeof(TResultType), CultureInfo.CreateSpecificCulture("en-US"));
        }

        internal static TResultType GetValueOrDefault<TResultType>(string value) where TResultType : struct
        {
            TResultType? result = GetValue<TResultType>(value);
            return result ?? (default);
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Globalization", "CA1303:Do not pass literals as localized parameters", Justification = "Exception messages are non translatable for now")]
        internal static string GetEnumerationDescription<TEnumType>(this TEnumType enumerationValue) where TEnumType : struct
        {
            Type type = enumerationValue.GetType();
            if (!type.IsEnum)
            {
                throw new ArgumentException("EnumerationValue must be of Enum type", nameof(enumerationValue));
            }

            string name = Enum.GetName(type, enumerationValue);
            if (name != null)
            {
                FieldInfo field = type.GetField(name);
                if (field != null)
                {
                    if (Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) is DescriptionAttribute attr)
                    {
                        return attr.Description;
                    }
                }
            }
            return null;
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Globalization", "CA1303:Do not pass literals as localized parameters", Justification = "Exception messages are non translatable for now")]
        public static TEnumType DescriptionToEnumValue<TEnumType>(this string enumerationDescription) where TEnumType : struct
        {
            var type = typeof(TEnumType);

            if (!type.IsEnum)
                throw new ArgumentException("Type parameter must be of Enum type");

            foreach (TEnumType val in Enum.GetValues(type))
            {
                if (val.GetEnumerationDescription() == enumerationDescription)
                    return val;
            }

            throw new ArgumentException("Invalid description for enum " + type.Name, nameof(enumerationDescription));
        }
    }
}
