using PVOutput.Net.Objects.Outputs.Implementations;
using PVOutput.Net.Objects.String;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PVOutput.Net.Objects.Outputs.String.Readers
{
    internal class AggregatedOutputObjectStringReader : BaseObjectStringReader<IAggregatedOutput>
    {
        public override IAggregatedOutput CreateObjectInstance() => new AggregatedOutput();

        protected override Action<IAggregatedOutput, string>[] ObjectProperties
        {
            get
            {
                return new Action<IAggregatedOutput, string>[]
                {
                    (target, propertyString) => target.Date = FormatHelper.ParseDate(propertyString),
                    (target, propertyString) => target.Outputs = Convert.ToInt32(propertyString),
                    (target, propertyString) => target.EnergyGenerated = Convert.ToInt32(propertyString),
                    (target, propertyString) => target.Efficiency = Convert.ToDecimal(propertyString, CultureInfo.CreateSpecificCulture("en-US")),
                    (target, propertyString) => target.EnergyExported = Convert.ToInt32(propertyString),
                    (target, propertyString) => target.EnergyUsed  = Convert.ToInt32(propertyString),
                    (target, propertyString) => target.PeakEnergyImport = FormatHelper.ParseValue<int>(propertyString),
                    (target, propertyString) => target.OffPeakEnergyImport = FormatHelper.ParseValue<int>(propertyString),
                    (target, propertyString) => target.ShoulderEnergyImport = FormatHelper.ParseValue<int>(propertyString),
                    (target, propertyString) => target.HighShoulderEnergyImport = FormatHelper.ParseValue<int>(propertyString)
                };
            }
        }
    }
}
