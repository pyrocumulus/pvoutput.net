using PVOutput.Net.Objects.Modules;
using PVOutput.Net.Requests.Handler;
using PVOutput.Net.Requests.Modules;
using PVOutput.Net.Responses;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PVOutput.Net.Modules
{
	public class StatusService : BaseService
	{
		public StatusService(PVOutputClient client) : base(client)
		{
		}

		public Task<PVOutputResponse<IStatus>> GetStatusForDateTimeAsync(DateTime dateTime, int? systemId = null, CancellationToken cancellationToken = default)
		{
			var handler = new RequestHandler(Client);

			return handler.ExecuteSingleItemRequestAsync<IStatus>(new StatusRequest { Date = dateTime, SystemId = systemId }, cancellationToken);
		}

		public Task<PVOutputArrayResponse<IStatusHistory>> GetHistoryForPeriodAsync(DateTime fromDateTime, DateTime toDateTime, bool ascending = true, bool extendedData = false, int? limit = null, CancellationToken cancellationToken = default)
		{
			var handler = new RequestHandler(Client);

			return handler.ExecuteArrayRequestAsync<IStatusHistory>(
				new StatusRequest
				{
					Date = fromDateTime.Date,
					From = fromDateTime,
					To = toDateTime,
					Ascending = ascending,
					Extended = extendedData,
					Limit = limit
				}, cancellationToken);
		}
	}
}
