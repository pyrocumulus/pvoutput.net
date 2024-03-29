﻿using System;
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

        internal static TimeSpan ParseTime(string timeString)
        {
            return TimeSpan.ParseExact(timeString, "h\\:mm", CultureInfo.InvariantCulture, TimeSpanStyles.None);
        }

        internal static string GetDateAsString(DateTime date)
        {
            return date.ToString("yyyyMMdd", CultureInfo.InvariantCulture);
        }

        internal static string GetTimeAsString(DateTime date)
        {
            return date.ToString("HH:mm", CultureInfo.InvariantCulture.DateTimeFormat);
        }

        internal static string GetTimeAsString(TimeSpan time)
        {
            return time.ToString("h\\:mm", CultureInfo.InvariantCulture.DateTimeFormat);
        }

        internal static string GetValueAsString<TInputType>(TInputType? value) where TInputType : struct
        {
            if (value == null)
            {
                return null;
            }

            return (string)Convert.ChangeType(value, typeof(string), CultureInfo.CreateSpecificCulture("en-US"));
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

        internal static string GetEnumerationDescription<TEnumType>(this TEnumType enumerationValue) where TEnumType : struct
        {
            Type type = enumerationValue.GetType();
            if (!type.IsEnum)
            {
                throw new ArgumentException("EnumerationValue must be of Enum type", nameof(enumerationValue));
            }

            var name = Enum.GetName(type, enumerationValue);
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

        public static TEnumType DescriptionToEnumValue<TEnumType>(this string enumerationDescription) where TEnumType : struct
        {
            Type type = typeof(TEnumType);

            if (!type.IsEnum)
            { 
                throw new ArgumentException("Type parameter must be of Enum type"); 
            }

            foreach (TEnumType val in Enum.GetValues(type))
            {
                if (val.GetEnumerationDescription() == enumerationDescription)
                    return val;
            }

            throw new ArgumentException($"Invalid description '{enumerationDescription}' for enum " + type.Name, nameof(enumerationDescription));
        }

        /// <summary>
        /// Returns enum value corresponding to given textual string value. If the string is a null literal, it returns null.
        /// </summary>
        /// <typeparam name="TEnumType">Enum to cast the string to.</typeparam>
        /// <param name="enumerationDescription">String containing the enum description.</param>
        /// <returns>An enum value if the description is found, null if the description equates to a null value.</returns>
        /// <exception cref="ArgumentException">Throws if supplied type is not an Enum-type or if description does not match any enumeration value.</exception>
        public static TEnumType? DescriptionToNullableEnumValue<TEnumType>(this string enumerationDescription) where TEnumType : struct
        {
            Type type = typeof(TEnumType);
            if (!type.IsEnum)
            {
                throw new ArgumentException("Type parameter must be of Enum type");
            }

            if (string.IsNullOrWhiteSpace(enumerationDescription))
            {
                return null;
            }

            if (enumerationDescription.Equals("null", StringComparison.OrdinalIgnoreCase))
            {
                return null;
            }

            foreach (TEnumType val in Enum.GetValues(type))
            {
                if (val.GetEnumerationDescription() == enumerationDescription)
                    return val;
            }

            throw new ArgumentException($"Invalid description '{enumerationDescription}' for enum " + type.Name, nameof(enumerationDescription));
        }
    }
}
