using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PVOutput.Net.Tests.Modules.System
{
    public static class SystemModuleTestsData
    {
        public const string GETSYSTEM_URL = "getsystem.jsp*";

        public const string SYSTEM_RESPONSE_SIMPLE = "Test System,4125,1234,15,275,JA Solar mono,1,5500,Fronius Primo 3.6-1,E,53.0,No,20161001,51.0,0.0,5;;0";
		public const string SYSTEM_RESPONSE_EXTENDED = "Test System,4125,1234,15,275,JA Solar mono,1,5500,Fronius Primo 3.6-1,E,53.0,No,20160822,51.0,6.1,5,0,0,null,NaN;19.37,19.37,19.37,NaN,NaN,10.65;97,406,426,493;1;,,,,,,,,,,,;61,106,252,380,430,425,417,354,253,159,70,48,0,0,0,0,0,0,0,0,0,0,0,0";

	}
}
