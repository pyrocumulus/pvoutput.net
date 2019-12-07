using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using PVOutput.Net.Objects.Modules;
using PVOutput.Net.Requests.Handler;
using PVOutput.Net.Requests.Modules;
using PVOutput.Net.Responses;

namespace PVOutput.Net.Modules
{
    public class InsolationService : BaseService
    {
        public InsolationService(PVOutputClient client) : base(client)
        {
        }

        public Task<PVOutputArrayResponse<IInsolation>> GetInsolationForOwnSystem(DateTime? date = null, CancellationToken cancellationToken = default)
        {
            var handler = new RequestHandler(Client);
            var response = handler.ExecuteArrayRequestAsync<IInsolation>(new InsolationRequest { Date = date }, cancellationToken);

            if (date.HasValue)
            {
                return response.ContinueWith(antecedent => AddRequestedDate(antecedent, date.Value), TaskContinuationOptions.OnlyOnRanToCompletion);
            }
            return response;
        }

        public Task<PVOutputArrayResponse<IInsolation>> GetInsolationForSystem(int systemId, DateTime? date = null, CancellationToken cancellationToken = default)
        {
            var handler = new RequestHandler(Client);
            var response = handler.ExecuteArrayRequestAsync<IInsolation>(new InsolationRequest { SystemId = systemId, Date = date }, cancellationToken);

            if (date.HasValue)
            {
                return response.ContinueWith(antecedent => AddRequestedDate(antecedent, date.Value), TaskContinuationOptions.OnlyOnRanToCompletion);
            }
            return response;
        }

        public Task<PVOutputArrayResponse<IInsolation>> GetInsolationForLocation(decimal latitude, decimal longitude, DateTime? date = null, CancellationToken cancellationToken = default)
        {
            var handler = new RequestHandler(Client);
            var response = handler.ExecuteArrayRequestAsync<IInsolation>(new InsolationRequest { Latitude = latitude, Longitude = longitude, Date = date }, cancellationToken);

            if (date.HasValue)
            {
                return response.ContinueWith(antecedent => AddRequestedDate(antecedent, date.Value), TaskContinuationOptions.OnlyOnRanToCompletion);
            }
            return response;
        }

        private PVOutputArrayResponse<IInsolation> AddRequestedDate(Task<PVOutputArrayResponse<IInsolation>> response, DateTime requestedDate)
        {
            foreach (var insolation in response.Result.Values)
            {
                insolation.Time = requestedDate.Add(insolation.Time.TimeOfDay);
            }
            return response.Result;
        }
    }
}
