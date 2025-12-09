using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommunityToolkit.Diagnostics;

namespace PVOutput.Net.Objects.Core
{
    internal static class GuardExtensions
    {
        internal static void IsNoFutureDate(DateTime value, string paramName)
        {
            Guard.IsLessThan(value, DateTime.Today.AddDays(1), paramName);
        }

        internal static void NoTimeComponent(DateTime value, string paramName)
        {
            if (value.TimeOfDay != TimeSpan.Zero)
            {
                throw new ArgumentException(
                    $"{paramName} has a time component. Please only use DateTime.Date instead.",
                    paramName);
            }
        }

        internal static void NotEmpty<T>(IEnumerable<T> value, string paramName)
        {
            Guard.IsNotNull(value, paramName);
            if (!value.Any())
            {
                throw new ArgumentException("Collection must not be empty.", paramName);
            }
        }
    }
}
