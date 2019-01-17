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
    internal class TeamOutputObjectStringReader : BaseObjectStringReader<ITeamOutput>
    {
        public override ITeamOutput CreateObjectInstance() => new TeamOutput();

        protected override Action<ITeamOutput, string>[] ObjectProperties
        {
            get
            {
                return new Action<ITeamOutput, string>[]
                {
                    (target, propertyString) => target.Date = FormatHelper.ParseDate(propertyString),
                    (target, propertyString) => target.Outputs = Convert.ToInt32(propertyString),
                    (target, propertyString) => target.Efficiency = Convert.ToDecimal(propertyString, CultureInfo.CreateSpecificCulture("en-US")),
                    (target, propertyString) => target.TotalGeneration = Convert.ToInt32(propertyString),
                    (target, propertyString) => target.AverageGeneration  = Convert.ToInt32(propertyString),
                    (target, propertyString) => target.TotalExported = Convert.ToInt32(propertyString),
                    (target, propertyString) => target.TotalConsumption = Convert.ToInt32(propertyString),
                    (target, propertyString) => target.AverageConsumption = Convert.ToInt32(propertyString),
                    (target, propertyString) => target.TotalImported = Convert.ToInt32(propertyString)
                };
            }
        }
    }
}
