using PVOutput.Net.Enums;

namespace PVOutput.Net.Objects
{
    /// <summary>
    /// A single status used for posting single statuses.
    /// </summary>
    /// <inheritdoc />
    public interface IStatusPost : IBatchStatusPost
    {
        /// <summary>
        /// Indicates what kind of cumulative value the status has, if any.
        /// </summary>
        CumulativeStatusType Cumulative { get; set; }

        /// <summary>
        /// Indicates that the power values passed are net export/import rather than gross generation/consumption.
        /// </summary>
        bool Net { get; set; }

        /// <summary>
        /// A text message to record with the status.
        /// </summary>
        string? TextMessage { get; set; }

        /// <summary>
        /// Battery power in watts. Positive when charging, negative when discharging.
        /// <para>Donation only. Required for any of the other battery values to be recorded.</para>
        /// </summary>
        decimal? BatteryPower { get; set; }

        /// <summary>
        /// Battery state of charge in percent.
        /// <para>Donation only.</para>
        /// </summary>
        decimal? BatteryStateOfCharge { get; set; }

        /// <summary>
        /// Battery size or capacity in watt hours.
        /// <para>Donation only.</para>
        /// </summary>
        decimal? BatteryCapacity { get; set; }

        /// <summary>
        /// Lifetime energy charged into the battery in watt hours.
        /// <para>Donation only.</para>
        /// </summary>
        decimal? BatteryLifetimeEnergyCharged { get; set; }

        /// <summary>
        /// Lifetime energy discharged from the battery in watt hours.
        /// <para>Donation only.</para>
        /// </summary>
        decimal? BatteryLifetimeEnergyDischarged { get; set; }

        /// <summary>
        /// Status of the battery.
        /// <para>Donation only.</para>
        /// </summary>
        BatteryStatus? BatteryStatus { get; set; }
    }
}
