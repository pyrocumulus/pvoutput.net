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
    internal class OutputObjectStringReader : BaseObjectStringReader<IOutput>
    {
        public override IOutput CreateObjectInstance() => new Output();

        protected override Action<IOutput, string>[] ObjectProperties
        {
            get
            {
                return new Action<IOutput, string>[]
                {
                    (target, propertyString) => target.Date = FormatHelper.ParseDate(propertyString),
                    (target, propertyString) => target.EnergyGenerated = Convert.ToInt32(propertyString),
                    (target, propertyString) => target.Efficiency = FormatHelper.ParseNumeric(propertyString),
                    (target, propertyString) => target.EnergyExported = Convert.ToInt32(propertyString),
                    (target, propertyString) => target.EnergyUsed = Convert.ToInt32(propertyString),
                    (target, propertyString) => target.PeakPower = FormatHelper.ParseValue<int>(propertyString),
                    (target, propertyString) => target.PeakTime = propertyString.Equals("NaN", StringComparison.OrdinalIgnoreCase) ? (DateTime?)null : target.Date.Add(FormatHelper.ParseTime(propertyString).TimeOfDay),
                    (target, propertyString) => target.Condition = propertyString,
                    (target, propertyString) => target.MinimumTemperature = FormatHelper.ParseValue<int>(propertyString),
                    (target, propertyString) => target.MaximumTemperature = FormatHelper.ParseValue<int>(propertyString),
                    (target, propertyString) => target.PeakEnergyImport = FormatHelper.ParseValue<int>(propertyString),
                    (target, propertyString) => target.OffPeakEnergyImport = FormatHelper.ParseValue<int>(propertyString),
                    (target, propertyString) => target.ShoulderEnergyImport = FormatHelper.ParseValue<int>(propertyString),
                    (target, propertyString) => target.HighShoulderEnergyImport = FormatHelper.ParseValue<int>(propertyString),
                    (target, propertyString) => target.Insolation = FormatHelper.ParseValue<int>(propertyString)
                };
            }
        }
    }
}
