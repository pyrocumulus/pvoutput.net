namespace PVOutput.Net.Enums
{
    /// <summary>
    /// Contains the possible statuses of a battery.
    /// </summary>
    public enum BatteryStatus
    {
        /// <summary>
        /// Invalid
        /// </summary>
        Invalid = 0,

        /// <summary>
        /// Standby
        /// </summary>
        Standby = 1,

        /// <summary>
        /// Thermal management
        /// </summary>
        ThermalManagement = 2,

        /// <summary>
        /// Enabled
        /// </summary>
        Enabled = 3,

        /// <summary>
        /// Fault
        /// </summary>
        Fault = 4
    }
}
