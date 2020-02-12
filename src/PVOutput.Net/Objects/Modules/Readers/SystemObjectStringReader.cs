using System;
using System.Collections.Generic;
using System.IO;
using PVOutput.Net.Enums;
using PVOutput.Net.Objects.Core;
using PVOutput.Net.Objects.Modules.Implementations;

namespace PVOutput.Net.Objects.Modules.Readers
{
    internal class SystemObjectStringReader : BaseObjectStringReader<ISystem>
    {
        public override ISystem CreateObjectInstance() => new Implementations.System();

        public SystemObjectStringReader()
        {
            _parsers.Add(ParseBaseProperties);
            _parsers.Add(ParseTariffProperties);
            _parsers.Add(ParseTeamProperties);
            _parsers.Add((target, reader) => target.Donations = Convert.ToInt32(ReadProperty(reader)));
            _parsers.Add(ParseExtendedProperties);
            _parsers.Add(ParseMonthlyEstimates);
        }

        private void ParseBaseProperties(ISystem target, TextReader reader)
        {
            var properties = new Action<ISystem, string>[]
            {
                (t, s) => t.SystemName = s,
                (t, s) => t.SystemSize = Convert.ToInt32(s),
                (t, s) => t.Postcode = Convert.ToInt32(s),
                (t, s) => t.NumberOfPanels = Convert.ToInt32(s),
                (t, s) => t.PanelPower = Convert.ToInt32(s),
                (t, s) => t.PanelBrand = s,
                (t, s) => t.NumberOfInverters = Convert.ToInt32(s),
                (t, s) => t.InverterPower = Convert.ToInt32(s),
                (t, s) => t.InverterBrand = s,
                (t, s) => t.Orientation = s,
                (t, s) => t.ArrayTilt = FormatHelper.ParseValueWithDefault<decimal>(s),
                (t, s) => t.Shade = s,
                (t, s) => t.InstallDate = FormatHelper.ParseDate(s),
                (t, s) => t.Latitude = FormatHelper.ParseValueWithDefault<double>(s),
                (t, s) => t.Longitude = FormatHelper.ParseValueWithDefault<double>(s),
                (t, s) => t.StatusInterval = Convert.ToInt32(s),
                (t, s) => t.SecondaryNumberOfPanels = FormatHelper.ParseValue<int>(s),
                (t, s) => t.SecondaryPanelPower = FormatHelper.ParseValue<int>(s),
                (t, s) => t.SecondaryOrientation = s,
                (t, s) => t.SecondaryArrayTilt = FormatHelper.ParseValue<decimal>(s)
            };

            ParsePropertyArray(target, reader, properties);
        }

        private void ParseTariffProperties(ISystem target, TextReader reader)
        {
            var properties = new Action<ISystem, string>[]
            {
                    (t, s) => t.ExportTariff = FormatHelper.ParseValue<decimal>(s),
                    (t, s) => t.ImportPeakTariff = FormatHelper.ParseValue<decimal>(s),
                    (t, s) => t.ImportOffPeakTariff = FormatHelper.ParseValue<decimal>(s),
                    (t, s) => t.ImportShoulderTariff = FormatHelper.ParseValue<decimal>(s),
                    (t, s) => t.ImportHighShoulderTariff = FormatHelper.ParseValue<decimal>(s),
                    (t, s) => t.ImportDailyCharge = FormatHelper.ParseValue<decimal>(s),
            };

            ParsePropertyArray(target, reader, properties);
        }

        private void ParseTeamProperties(ISystem target, TextReader reader)
        {
            var teamIds = ReadPropertiesForGroup(reader);

            if (teamIds.Count == 0)
            {
                target.Teams = new List<int>();
                return;
            }

            var result = new List<int>();
            foreach (string teamId in teamIds)
            {
                result.Add(Convert.ToInt32(teamId));
            }
            target.Teams = result;
        }

        private void ParseExtendedProperties(ISystem target, TextReader reader)
        {
            var extendedData = ReadPropertiesForGroup(reader);

            if (extendedData.Count == 0)
            {
                target.ExtendedDataConfig = new List<ExtendedDataElement>();
                return;
            }

            var result = new List<ExtendedDataElement>(5);
            var enumerator = extendedData.GetEnumerator();
            while (enumerator.MoveNext())
            {
                var label = enumerator.Current;
                enumerator.MoveNext();
                var unit = enumerator.Current;
                result.Add(new ExtendedDataElement(label, unit));
            }

            target.ExtendedDataConfig = result;
        }

        private void ParseMonthlyEstimates(ISystem target, TextReader reader)
        {
            var estimates = ReadPropertiesForGroup(reader);

            if (estimates.Count == 0)
            {
                target.MonthlyGenerationEstimates = new Dictionary<PVMonth, int>();
                target.MonthlyConsumptionEstimates = new Dictionary<PVMonth, int>();
                return;
            }

            var consumptionEstimates = new Dictionary<PVMonth, int>();
            var generationEstimates = new Dictionary<PVMonth, int>();

            for (int i = 0; i < estimates.Count; i++)
            {
                var month = (PVMonth)(i % 12 + 1);
                var estimate = Convert.ToInt32(estimates[i]);

                if (i < 12)
                {
                    generationEstimates.Add(month, estimate);
                }
                else
                {
                    consumptionEstimates.Add(month, estimate);
                }
            }

            target.MonthlyGenerationEstimates = generationEstimates;
            target.MonthlyConsumptionEstimates = consumptionEstimates;
        }
    }
}
