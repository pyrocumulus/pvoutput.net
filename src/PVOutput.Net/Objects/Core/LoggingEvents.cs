using System;
using System.Collections.Generic;
using System.Text;

namespace PVOutput.Net.Objects.Core
{
    internal class LoggingEvents
    {
        /*
         * Logging scope (parameter) keywords
         */
        public const string RequestId = "RequestId";
        public const string Parameter_Date = "Date";
        public const string Parameter_FromDate = "FromDate";
        public const string Parameter_ToDate = "ToDate";
        public const string Parameter_Moment = "Moment";
        public const string Parameter_Limit = "Limit";
        public const string Parameter_SystemId = "SystemId";
        public const string Parameter_TeamId = "TeamId";
        public const string Parameter_Coordinate = "Coordinate";
        public const string Parameter_SearchQueryText = "SearchQueryText";
        public const string Parameter_AggregationPeriod = "AggregationPeriod";
        public const string Parameter_ExtendedData = "ExtendedData";
        public const string Parameter_Ascending = "Ascending";
        public const string Parameter_IncludeConsumptionAndImport = "IncludeConsumptionAndImport";
        public const string Parameter_IncludeCreditDebit = "IncludeCreditDebit";
        public const string Parameter_GetInsolation = "GetInsolation";
        public const string Parameter_TimeZone = "TimeZone";
        public const string Parameter_RegionKey = "RegionKey";        
        public const string Parameter_Search_Name = "Name";
        public const string Parameter_Search_UseStartsWith = "UseStartsWith";
        public const string Parameter_Search_Value = "SearchValue";
        public const string Parameter_Search_Postcode = "Postcode";
        public const string Parameter_Search_Panel = "Panel";
        public const string Parameter_Search_Inverter = "Inverter";
        public const string Parameter_Search_Kilometers = "Kilometers";
        public const string Parameter_Search_TeamName = "Teamname";
        public const string Parameter_Search_Orientation = "Orientation";
        public const string Parameter_Search_Tilt = "Tilt";

        /*
         * RequestHandler base events
         */
        public const int Handler_ExecuteRequest = 10001;
        public const int Handler_ReceivedResponseContent = 10002;
        public const int Handler_RequestStatusSuccesful = 10003;
        public const int Handler_RequestStatusFailed = 10004;

        /*
         * Module specific event IDs per request
         */
        public const int ExtendedService_GetRecentExtendedData = 20101;
        public const int ExtendedService_GetExtendedDataForPeriod = 20102;

        public const int FavouriteService_GetFavourites = 20201;

        public const int InsolationService_GetInsolationForOwnSystem = 20301;
        public const int InsolationService_GetInsolationForSystem = 20302;
        public const int InsolationService_GetInsolationForLocation = 20303;

        public const int MissingService_GetMissingDaysInPeriod = 20401;

        public const int OutputService_GetOutputForDate = 20501;
        public const int OutputService_GetOutputsForPeriod = 20502;
        public const int OutputService_GetTeamOutputForDate = 20503;
        public const int OutputService_GetTeamOutputsForPeriod = 20504;
        public const int OutputService_GetAggregatedOutputs = 20505;
        public const int OutputService_AddOutput = 20506;
        public const int OutputService_AddBatchOutput = 20507;

        public const int SearchService_Search = 20601;
        public const int SearchService_SearchByName = 20602;
        public const int SearchService_SearchByPostCodeOrSize = 20603;
        public const int SearchService_SearchByPostCode = 20604;
        public const int SearchService_SearchBySize = 20605;
        public const int SearchService_SearchByPanel = 20606;
        public const int SearchService_SearchByInverter = 20607;
        public const int SearchService_SearchByDistance = 20608;
        public const int SearchService_SearchByTeam = 20609;
        public const int SearchService_SearchByOrientation = 20610;
        public const int SearchService_SearchByTilt = 20611;
        public const int SearchService_GetFavourites = 20612;

        public const int StatisticsService_GetLifetimeStatistics = 20701;
        public const int StatisticsService_GetStatisticsForPeriod = 20702;

        public const int StatusService_GetStatusForDateTime = 20801;
        public const int StatusService_GetHistoryForPeriod = 20802;
        public const int StatusService_GetDayStatisticsForPeriod = 20803;
        public const int StatusService_AddStatus = 20804;
        public const int StatusService_AddBatchStatus = 20805;
        public const int StatusService_DeleteStatus = 20806;

        public const int SupplyService_GetSupply = 20901;

        public const int SystemService_GetOwnSystem = 21001;
        public const int SystemService_GetOtherSystem = 21002;

        public const int TeamService_GetTeam = 21101;
        public const int TeamService_JoinTeam = 21102;
        public const int TeamService_LeaveTeam = 21103;
    }
}
