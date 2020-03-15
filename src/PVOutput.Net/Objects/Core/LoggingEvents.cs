using System;
using System.Collections.Generic;
using System.Text;

namespace PVOutput.Net.Objects.Core
{
    internal class LoggingEvents
    {
        /*
         * RequestHandler base events
         */ 
        public const int ExecuteRequest             = 10001;
        public const int ReceivedResponseContent    = 10002;
        public const int RequestStatusSuccesful     = 10003;
        public const int RequestStatusFailed        = 10004;


        /*
         * Module specific event IDs
         */
        public const int ExtendedService_           = 20101;
        public const int FavouriteService_          = 20201;
        public const int InsolationService_         = 20301;
        public const int MissingService_            = 20401;
        public const int OutputService_             = 20501;
        public const int SearchService_             = 20601;
        public const int StatisticsService_         = 20701;
        public const int StatusService_             = 20801;
        public const int SupplyService_             = 20901;
        public const int SystemService_             = 21001;
        public const int TeamService_               = 21101;

    }
}
