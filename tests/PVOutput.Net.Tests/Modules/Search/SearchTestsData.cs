using System;
using System.Collections.Generic;
using System.Text;

namespace PVOutput.Net.Tests.Modules.Search
{
    public partial class SearchServiceTests
    {
        public const string SEARCH_URL = "search.jsp";

        public const string SEARCH_RESPONSE_SINGLE = "Solar 4 US,9360,Australia 4280,NW,81,2 days ago,249,Solarfun,Aurora,NaN,-27.831402,153.028469";

        public const string SEARCH_RESPONSE_ARRAY = 
@"Solar 4 US,9360,Australia 4280,NW,81,2 days ago,249,Solarfun,Aurora,NaN,-27.831402,153.028469
Solar Chaos,1480,Australia 4870,NW,14,5 weeks ago,694,ET Solar ET-M572185,PCM Solar King 1500,NaN,-16.883938,145.746732
Solar Frontier 2.97KW 2768,2952,Australia 2768,W,72,Yesterday,387,Solar Frontier,Xantrex 2.8 AU,NaN,-33.737863,150.922732
solar powered muso,3600,Australia 5074,NW,146,5 days ago,151,Sunpower,Fronius,NaN,-34.878302,138.663553";
    }
}
