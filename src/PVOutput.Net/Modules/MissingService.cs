using PVOutput.Net.Objects.Missing;
using PVOutput.Net.Requests.Handler;
using PVOutput.Net.Requests.Missing;
using PVOutput.Net.Responses;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PVOutput.Net.Modules
{
	public class MissingService : BaseService
	{
		public MissingService(PVOutputClient client) : base(client)
		{

		}

		public Task<PVOutputResponse<IMissing>> GetMissingDaysInPeriod(DateTime fromDate, DateTime toDate, CancellationToken cancellationToken = default)
		{
			var handler = new RequestHandler(Client);
			return handler.ExecuteSingleItemRequestAsync<IMissing>(new MissingRequest { FromDate = fromDate, ToDate = toDate }, cancellationToken);
		}
	}
}
