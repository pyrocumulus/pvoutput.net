namespace PVOutput.Net.Tests.Modules.System
{
    public partial class SystemServiceTests
    {
        public const string GETSYSTEM_URL = "getsystem.jsp";
        
        public const string POSTSYSTEM_URL = "postsystem.jsp";

        public const string SYSTEM_RESPONSE_SIMPLE = "Test System,4125,1234,15,275,JA Solar mono,1,5500,Fronius Primo 3.6-1,E,,No,,51.0,6.1,5;;";
        
        public const string OWNSYSTEM_RESPONSE_WITHOUT_SECONDARYPANEL = "Test System,4125,1234,15,275,JA Solar mono,1,5500,Fronius Primo 3.6-1,E,53.0,No,,51.0,6.1,5,,,null,NaN;";

        public const string SYSTEM_RESPONSE_EXTENDED = "Test System,4125,1234,15,275,JA Solar mono,1,5500,Fronius Primo 3.6-1,E,53.1,No,20160822,51.0,6.1,5,10,190,W,33.5;17.37,20.46,20.2,25.4,22.65,40.0;12,232,480,512;1;DC-1 Voltage,V,DC-2 Voltage,V,DC-DC Booster Temp,°C,DC-1 Power (2x13x290Wp),W;61,106,252,380,430,425,417,354,253,159,70,48,400,350,300,250,200,150,100,175,275,375,475,525";

        public const string SYSTEM_RESPONSE_WITHOUT_TEAMS = "Test System,4125,1234,15,275,JA Solar mono,1,5500,Fronius Primo 3.6-1,E,53.1,No,20160822,51.0,6.1,5,10,190,W,33.5;17.37,20.46,20.2,25.4,22.65,40.0;;1;DC-1 Voltage,V,DC-2 Voltage,V,DC-DC Booster Temp,°C,DC-1 Power (2x13x290Wp),W;61,106,252,380,430,425,417,354,253,159,70,48,400,350,300,250,200,150,100,175,275,375,475,525";

        public const string SYSTEM_RESPONSE_WITHOUT_EXTENDEDDATA = "Test System,4125,1234,15,275,JA Solar mono,1,5500,Fronius Primo 3.6-1,E,53.1,No,20160822,51.0,6.1,5,10,190,W,33.5;17.37,20.46,20.2,25.4,22.65,40.0;12,232,480,512;1;;61,106,252,380,430,425,417,354,253,159,70,48,400,350,300,250,200,150,100,175,275,375,475,525";
        
        public const string SYSTEM_RESPONSE_WITHOUT_ESTIMATES = "Test System,4125,1234,15,275,JA Solar mono,1,5500,Fronius Primo 3.6-1,E,53.1,No,20160822,51.0,6.1,5,10,190,W,33.5;17.37,20.46,20.2,25.4,22.65,40.0;12,232,480,512;1;DC-1 Voltage,V,DC-2 Voltage,V,DC-DC Booster Temp,°C,DC-1 Power (2x13x290Wp),W;;";

        public const string SYSTEM_RESPONSE_WITH_MINIMALEXTENDEDDATA = "Test System,4125,1234,15,275,JA Solar mono,1,5500,Fronius Primo 3.6-1,E,53.1,No,20160822,51.0,6.1,5,10,190,W,33.5;17.37,20.46,20.2,25.4,22.65,40.0;12,232,480,512;1;DC-1 Voltage,V,DC-2 Voltage,,,°C,DC-1 Power (2x13x290Wp),W;61,106,252,380,430,425,417,354,253,159,70,48,400,350,300,250,200,150,100,175,275,375,475,525";
    }
}
