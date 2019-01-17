using System;
using System.Globalization;

namespace PVOutput.Net.Responses
{
    public class SystemInformation
    {
        public class ArrayInfo
        {
            public int NumberOfPanels { get; set; }
            public int PanelPower { get; set; }
            public string Orientation { get; set; }
            public Decimal ArrayTilt { get; set; }
        }

        public class YearlyEstimates
        {
            public int January { get; set; }
            public int Februray { get; set; }
            public int March { get; set; }
            public int April { get; set; }
            public int May { get; set; }
            public int June { get; set; }
            public int July { get; set; }
            public int August { get; set; }
            public int September { get; set; }
            public int October { get; set; }
            public int November { get; set; }
            public int December { get; set; }
        }

        public string SystemName { get; set; }
        public int SystemSize { get; set; }
        public string Postcode { get; set; }
        public int NumberOfPanels { get; set; }
        public int PanelPower { get; set; }
        public string PanelBrand { get; set; }
        public int NumberOfInverters { get; set; }
        public int InverterPower { get; set; }
        public string InverterBrand { get; set; }
        public string Orientation { get; set; }
        public Decimal ArrayTilt { get; set; }
        public string Shade { get; set; }
        public DateTime InstallDate { get; set; }

        public Decimal Latitude { get; set; }
        public Decimal Longitude { get; set; }
        public int StatusInterval { get; set; }

        public ArrayInfo SecondaryArray { get; set; }

        public YearlyEstimates GenerationEstimates { get; set; }
        public YearlyEstimates ConsumptionEstimates { get; set; }

        public static SystemInformation FromString(string apiResponse)
        {
            var result = new SystemInformation();
            var groups = apiResponse.Split(';');

            var items = groups[0].Split(',');

            result.SystemName = items[0];
            result.SystemSize = Convert.ToInt32(items[1]);
            result.Postcode = items[2];
            result.NumberOfPanels = Convert.ToInt32(items[3]);
            result.PanelPower = Convert.ToInt32(items[4]);
            result.PanelBrand = items[5];
            result.NumberOfInverters = Convert.ToInt32(items[6]);
            result.InverterPower = Convert.ToInt32(items[7]);
            result.InverterBrand = items[8];
            result.Orientation = items[9];
            result.ArrayTilt = Convert.ToDecimal(items[10], CultureInfo.CreateSpecificCulture("en-US"));
            result.Shade = items[11];
            result.InstallDate = DateTime.ParseExact(items[12], "yyyyMMdd", CultureInfo.InvariantCulture.DateTimeFormat);
            result.Latitude = Convert.ToDecimal(items[13], CultureInfo.CreateSpecificCulture("en-US"));
            result.Longitude = Convert.ToDecimal(items[14], CultureInfo.CreateSpecificCulture("en-US"));
            result.StatusInterval = Convert.ToInt32(items[15]);

            items = groups[3].Split(',');

            result.GenerationEstimates = new YearlyEstimates()
            {
                January = Convert.ToInt32(items[0]),
                Februray = Convert.ToInt32(items[1]),
                March = Convert.ToInt32(items[2]),
                April = Convert.ToInt32(items[3]),
                May = Convert.ToInt32(items[4]),
                June = Convert.ToInt32(items[5]),
                July = Convert.ToInt32(items[6]),
                August = Convert.ToInt32(items[7]),
                September = Convert.ToInt32(items[8]),
                October = Convert.ToInt32(items[9]),
                November = Convert.ToInt32(items[10]),
                December = Convert.ToInt32(items[11])
            };

            result.ConsumptionEstimates = new YearlyEstimates()
            {
                January = Convert.ToInt32(items[12]),
                Februray = Convert.ToInt32(items[13]),
                March = Convert.ToInt32(items[14]),
                April = Convert.ToInt32(items[15]),
                May = Convert.ToInt32(items[16]),
                June = Convert.ToInt32(items[17]),
                July = Convert.ToInt32(items[18]),
                August = Convert.ToInt32(items[19]),
                September = Convert.ToInt32(items[20]),
                October = Convert.ToInt32(items[21]),
                November = Convert.ToInt32(items[22]),
                December = Convert.ToInt32(items[23])
            };

            return result;
        }
    }
}
