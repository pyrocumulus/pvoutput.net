namespace PVOutput.Net.Enums
{
    /// <summary>
    /// Describes a cumulative type, if any
    /// </summary>
    public enum CumulativeStatusType
    { 
        /// <summary>
        /// No cumulation
        /// </summary>
        None = 0,

        /// <summary>
        /// Lifetime cumulative for generation and consumption
        /// </summary>
        LifetimeGenerationAndConsumption = 1,

        /// <summary>
        /// Lifetime cumulative for generation
        /// </summary>
        LifetimeGeneration = 2,

        /// <summary>
        /// Lifetime cumulative for consumption
        /// </summary>
        LifetimeConsumption = 3 
    }
}
