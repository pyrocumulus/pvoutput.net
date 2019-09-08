using PVOutput.Net.Objects.Statistics;
using PVOutput.Net.Requests.Handler;
using PVOutput.Net.Requests.Statistics;
using PVOutput.Net.Responses;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PVOutput.Net.Modules
{
	public class StatisticsService : BaseService
	{
		public StatisticsService(PVOutputClient client) : base(client)
		{
		}

		public Task<PVOutputResponse<IStatistic>> GetLifetimeStatisticsAsync(bool includeConsumptionAndImport = false, bool includeCreditDebit = false, int? systemId = null, CancellationToken cancellationToken = default)
		{
			var handler = new RequestHandler(Client);
			return handler.ExecuteSingleItemRequestAsync<IStatistic>(new StatisticRequest { SystemId = systemId, IncludeConsumptionImport = includeConsumptionAndImport, IncludeCreditDebit = includeConsumptionAndImport }, cancellationToken);
		}

		public Task<PVOutputResponse<IStatistic>> GetStatisticsForPeriodAsync(DateTime fromDate, DateTime toDate, bool includeConsumptionAndImport = false, bool includeCreditDebit = false, int? systemId = null, CancellationToken cancellationToken = default)
		{
			var handler = new RequestHandler(Client);
			return handler.ExecuteSingleItemRequestAsync<IStatistic>(new StatisticPeriodRequest { FromDate = fromDate, ToDate = toDate, SystemId = systemId, IncludeConsumptionImport = includeConsumptionAndImport, IncludeCreditDebit = includeConsumptionAndImport }, cancellationToken);
		}
	}
}
