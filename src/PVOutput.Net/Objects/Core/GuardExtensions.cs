using System;
using System.Collections.Generic;
using System.Text;
using Dawn;

namespace PVOutput.Net.Objects.Core
{
    internal static class GuardExtensions
    {
        internal static Guard.ArgumentInfo<DateTime> IsNoFutureDate(this Guard.ArgumentInfo<DateTime> argument)
        {
            return argument.LessThan(DateTime.Today.AddDays(1));
        }

        public static ref readonly Guard.ArgumentInfo<DateTime> NoTimeComponent(in this Guard.ArgumentInfo<DateTime> argument)
        {
            if (argument.Value.TimeOfDay != TimeSpan.Zero)
            {
                throw Guard.Fail(new ArgumentException(
                    $"{argument.Name} has a time component." +
                    "Please only use DateTime.Date instead.",
                    argument.Name));
            }

            return ref argument;
        }
    }
}
