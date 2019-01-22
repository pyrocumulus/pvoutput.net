using PVOutput.Net.Objects.String;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;

namespace PVOutput.Net.Objects.Systems.String.Readers
{
    internal class SystemObjectStringReader : ComplexObjectStringReader<ISystem>
    {
        public override ISystem CreateObjectInstance() => new Implementations.System();

		public SystemObjectStringReader()
		{
			_parsers = new List<Action<ISystem, TextReader>>();
			_parsers.Add(ParseBaseProperties);
			_parsers.Add(ParseSecondaryProperties);
			_parsers.Add(ParseTariffProperties);
			_parsers.Add(ParseTeamProperties);
		}

		/*protected Action<ISystem, string>[] ObjectProperties
        {
            get
            {
                return new Action<ISystem, string>[]
                {
                    //";"

                    (target, propertyString) => target.Donations = FormatHelper.ParseValueDefault<int>(propertyString),

                    //";"

                    (target, propertyString) => target.ExtendedDataConfig = propertyString,

                    //";"

                    (target, propertyString) => target.MonthlyEstimations = propertyString
                };
            }
        }*/

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
				(t, s) => t.ArrayTilt = FormatHelper.ParseNumeric(s),
				(t, s) => t.Shade = s,
				(t, s) => t.InstallDate = FormatHelper.ParseDate(s),
				(t, s) => t.Latitude = FormatHelper.ParseNumeric(s),
				(t, s) => t.Longitude = FormatHelper.ParseNumeric(s),
				(t, s) => t.StatusInterval = Convert.ToInt32(s)
			};

			ParsePropertyArray(target, reader, properties);
		}

		private void ParseSecondaryProperties(ISystem target, TextReader reader)
		{
			var properties = new Action<ISystem, string>[]
			{
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

			if (teamIds.Count() == 0)
			{
				target.Teams = Enumerable.Empty<int>();
				return;
			}

			var result = new List<int>();
			foreach (string teamId in teamIds)
			{
				result.Add(Convert.ToInt32(teamId));
			}
			target.Teams = result;
		}

		private string GetMonthlyEstimates(string v)
		{
			throw new NotImplementedException();
		}

		private string GetExtendedDataConfig(string v)
		{
			throw new NotImplementedException();
		}
    }
}
