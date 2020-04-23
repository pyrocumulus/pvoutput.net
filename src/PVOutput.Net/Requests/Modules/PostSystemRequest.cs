using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using PVOutput.Net.Objects;
using PVOutput.Net.Requests.Base;

namespace PVOutput.Net.Requests.Modules
{
    internal class PostSystemRequest : PostRequest
    {
        public int SystemId { get; set; }
        public string SystemName { get; set; }
        public IEnumerable<IExtendedDataDefinition> Configurations { get; set; }

        public override HttpMethod Method => HttpMethod.Post;
        public override string UriTemplate => "postsystem.jsp{?sid,name}";

        public override IDictionary<string, object> GetUriPathParameters()
        {
            var parameters = new Dictionary<string, object>
            {
                ["sid"] = SystemId,
                ["name"] = SystemName
            };

            AddConfigurations(parameters);

            return parameters;
        }

        private void AddConfigurations(Dictionary<string, object> parameters)
        {
            if (!Configurations.Any())
                return;

            foreach (IExtendedDataDefinition configuration in Configurations)
            {
                var index = configuration.Index.ToString();
                
                if (!string.IsNullOrEmpty(configuration.Label))
                {
                    parameters[$"{index}l"] = configuration.Label;
                }
                if (!string.IsNullOrEmpty(configuration.Unit))
                {
                    parameters[$"{index}u"] = configuration.Unit;
                }
                if (!string.IsNullOrEmpty(configuration.Colour))
                {
                    parameters[$"{index}c"] = configuration.Colour;
                }
                parameters[$"{index}a"] = configuration.Axis;
                parameters[$"{index}g"] = configuration.DisplayType.ToString();
            }
        }
    }
}
