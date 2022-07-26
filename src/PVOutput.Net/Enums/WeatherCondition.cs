using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace PVOutput.Net.Enums
{
    /// <summary>
    /// Describes weather conditions for a daily output.
    /// <para>See the official<see href="https://pvoutput.org/help/general_features.html#weather-conditions"> API information</see>.</para>
    /// </summary>
    public enum WeatherCondition
    {
        /// <summary>
        /// Unsure weather conditions.
        /// </summary>
        [Description("Not Sure")]
        NotSure,

        /// <summary>
        /// Fine weather conditions.
        /// Greather than 5.0 kWh/kW or more than 90% of daily estimate.
        /// </summary>
        [Description("Fine")]
        Fine,

        /// <summary>
        /// Partly cloudy weather conditions.
        /// Between 4.0 and 5.0 kWh/kW or between 60% to 90% of daily estimate.
        /// </summary>
        [Description("Partly Cloudy")]
        PartlyCloudy,

        /// <summary>
        /// Mostly cloudy weather conditions.
        /// Between 2.5 and 4.0 kWh/kW or between 40% to 60% of daily estimate.
        /// </summary>
        [Description("Mostly Cloudy")]
        MostlyCloudy,

        /// <summary>
        /// Cloudy weather conditions.
        /// Between 1.0 and 2.5 kWh/kW or between 20% to 40% of daily estimate.
        /// </summary>
        [Description("Cloudy")]
        Cloudy,

        /// <summary>
        /// Rainy weather conditions.
        /// Less than 1.0 kWh/kW or less than 20% of daily estimate.
        /// </summary>
        [Description("Showers")]
        Showers,

        /// <summary>
        /// Snowy weather conditions.
        /// </summary>
        [Description("Snow")]
        Snow
    }
}
