using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Logging;

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
        public const string Parameter_Search_CountryCode = "CountryCode";
        public const string Parameter_Search_Panel = "Panel";
        public const string Parameter_Search_Inverter = "Inverter";
        public const string Parameter_Search_Kilometers = "Kilometers";
        public const string Parameter_Search_TeamName = "Teamname";
        public const string Parameter_Search_Orientation = "Orientation";
        public const string Parameter_Search_Tilt = "Tilt";

        /*
         * RequestHandler base events
         */
        public static readonly EventId Handler_ExecuteRequest = new EventId(10001, "ExecuteRequest");
        public static readonly EventId Handler_ReceivedResponseContent = new EventId(10002, "ReceivedResponseContent");
        public static readonly EventId Handler_RequestStatusSuccesful = new EventId(10003, "RequestStatusSuccesful");
        public static readonly EventId Handler_RequestStatusFailed = new EventId(10004, "RequestStatusFailed");

        /*
         * Module specific event IDs per request
         */
        public static readonly EventId ExtendedService_GetRecentExtendedData = new EventId(20101, "GetRecentExtendedData");
        public static readonly EventId ExtendedService_GetExtendedDataForPeriod = new EventId(20102, "GetExtendedDataForPeriod");
        public static readonly EventId FavouriteService_GetFavourites = new EventId(20201, "GetFavourites");
        public static readonly EventId InsolationService_GetInsolationForOwnSystem = new EventId(20301, "GetInsolationForOwnSystem");
        public static readonly EventId InsolationService_GetInsolationForSystem = new EventId(20302, "GetInsolationForSystem");
        public static readonly EventId InsolationService_GetInsolationForLocation = new EventId(20303, "GetInsolationForLocation");
        public static readonly EventId MissingService_GetMissingDaysInPeriod = new EventId(20401, "GetMissingDaysInPeriod");
        public static readonly EventId OutputService_GetOutputForDate = new EventId(20501, "GetOutputForDate");
        public static readonly EventId OutputService_GetOutputsForPeriod = new EventId(20502, "GetOutputsForPeriod");
        public static readonly EventId OutputService_GetTeamOutputForDate = new EventId(20503, "GetTeamOutputForDate");
        public static readonly EventId OutputService_GetTeamOutputsForPeriod = new EventId(20504, "GetTeamOutputsForPeriod");
        public static readonly EventId OutputService_GetAggregatedOutputs = new EventId(20505, "GetAggregatedOutputs");
        public static readonly EventId OutputService_AddOutput = new EventId(20506, "AddOutput");
        public static readonly EventId OutputService_AddBatchOutput = new EventId(20507, "AddBatchOutput");
        public static readonly EventId SearchService_Search = new EventId(20601, "Search");
        public static readonly EventId SearchService_SearchByName = new EventId(20602, "SearchByName");
        public static readonly EventId SearchService_SearchByPostCodeOrSize = new EventId(20603, "SearchByPostCodeOrSize");
        public static readonly EventId SearchService_SearchByPostCode = new EventId(20604, "SearchByPostCode");
        public static readonly EventId SearchService_SearchBySize = new EventId(20605, "SearchBySize");
        public static readonly EventId SearchService_SearchByPanel = new EventId(20606, "SearchByPanel");
        public static readonly EventId SearchService_SearchByInverter = new EventId(20607, "SearchByInverter");
        public static readonly EventId SearchService_SearchByDistance = new EventId(20608, "SearchByDistance");
        public static readonly EventId SearchService_SearchByTeam = new EventId(20609, "SearchByTeam");
        public static readonly EventId SearchService_SearchByOrientation = new EventId(20610, "SearchByOrientation");
        public static readonly EventId SearchService_SearchByTilt = new EventId(20611, "SearchByTilt");
        public static readonly EventId SearchService_GetFavourites = new EventId(20612, "GetFavourites");
        public static readonly EventId StatisticsService_GetLifetimeStatistics = new EventId(20701, "GetLifetimeStatistics");
        public static readonly EventId StatisticsService_GetStatisticsForPeriod = new EventId(20702, "GetStatisticsForPeriod");
        public static readonly EventId StatusService_GetStatusForDateTime = new EventId(20801, "GetStatusForDateTime");
        public static readonly EventId StatusService_GetHistoryForPeriod = new EventId(20802, "GetHistoryForPeriod");
        public static readonly EventId StatusService_GetDayStatisticsForPeriod = new EventId(20803, "GetDayStatisticsForPeriod");
        public static readonly EventId StatusService_AddStatus = new EventId(20804, "AddStatus");
        public static readonly EventId StatusService_AddBatchStatus = new EventId(20805, "AddBatchStatus");
        public static readonly EventId StatusService_DeleteStatus = new EventId(20806, "DeleteStatus");
        public static readonly EventId SupplyService_GetSupply = new EventId(20901, "GetSupply");
        public static readonly EventId SystemService_GetOwnSystem = new EventId(21001, "GetOwnSystem");
        public static readonly EventId SystemService_GetOtherSystem = new EventId(21002, "GetOtherSystem");
        public static readonly EventId SystemService_PostSystem = new EventId(21003, "PostSystem");
        public static readonly EventId TeamService_GetTeam = new EventId(21101, "GetTeam");
        public static readonly EventId TeamService_JoinTeam = new EventId(21102, "JoinTeam");
        public static readonly EventId TeamService_LeaveTeam = new EventId(21103, "LeaveTeam");
    }
}
