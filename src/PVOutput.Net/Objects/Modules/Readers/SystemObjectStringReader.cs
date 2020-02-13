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
            _parsers.Add((target, reader) => target.Donations = FormatHelper.GetValueOrDefault<int>(ReadProperty(reader)));
            _parsers.Add(ParseExtendedProperties);
            _parsers.Add(ParseMonthlyEstimates);
        }

        private void ParseBaseProperties(ISystem target, TextReader reader)
        {
            var properties = new Action<ISystem, string>[]
            {
                (t, s) => t.SystemName = s,
                (t, s) => t.SystemSize = FormatHelper.GetValueOrDefault<int>(s),
                (t, s) => t.Postcode = FormatHelper.GetValueOrDefault<int>(s),
                (t, s) => t.NumberOfPanels = FormatHelper.GetValueOrDefault<int>(s),
                (t, s) => t.PanelPower = FormatHelper.GetValueOrDefault<int>(s),
                (t, s) => t.PanelBrand = s,
                (t, s) => t.NumberOfInverters = FormatHelper.GetValueOrDefault<int>(s),
                (t, s) => t.InverterPower = FormatHelper.GetValueOrDefault<int>(s),
                (t, s) => t.InverterBrand = s,
                (t, s) => t.Orientation = s,
                (t, s) => t.ArrayTilt = FormatHelper.GetValueOrDefault<decimal>(s),
                (t, s) => t.Shade = s,
                (t, s) => t.InstallDate = FormatHelper.ParseDate(s),
                (t, s) => t.Latitude = FormatHelper.GetValueOrDefault<double>(s),
                (t, s) => t.Longitude = FormatHelper.GetValueOrDefault<double>(s),
                (t, s) => t.StatusInterval = FormatHelper.GetValueOrDefault<int>(s),
                (t, s) => t.SecondaryNumberOfPanels = FormatHelper.GetValue<int>(s),
                (t, s) => t.SecondaryPanelPower = FormatHelper.GetValue<int>(s),
                (t, s) => t.SecondaryOrientation = s,
                (t, s) => t.SecondaryArrayTilt = FormatHelper.GetValue<decimal>(s)
            };

            ParsePropertyArray(target, reader, properties);
        }

        private void ParseTariffProperties(ISystem target, TextReader reader)
        {
            var properties = new Action<ISystem, string>[]
            {
                    (t, s) => t.ExportTariff = FormatHelper.GetValue<decimal>(s),
                    (t, s) => t.ImportPeakTariff = FormatHelper.GetValue<decimal>(s),
                    (t, s) => t.ImportOffPeakTariff = FormatHelper.GetValue<decimal>(s),
                    (t, s) => t.ImportShoulderTariff = FormatHelper.GetValue<decimal>(s),
                    (t, s) => t.ImportHighShoulderTariff = FormatHelper.GetValue<decimal>(s),
                    (t, s) => t.ImportDailyCharge = FormatHelper.GetValue<decimal>(s),
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
                result.Add(FormatHelper.GetValueOrDefault<int>(teamId));
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
                var estimate = FormatHelper.GetValueOrDefault<int>(estimates[i]);

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
