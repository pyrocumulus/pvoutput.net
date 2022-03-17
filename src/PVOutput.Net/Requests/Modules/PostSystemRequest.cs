using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using PVOutput.Net.Objects;
using PVOutput.Net.Objects.Core;
using PVOutput.Net.Requests.Base;

namespace PVOutput.Net.Requests.Modules
{
    internal sealed class PostSystemRequest : PostRequest
    {
        public int SystemId { get; set; }
        public string SystemName { get; set; }
        public IEnumerable<IExtendedDataDefinition> DataDefinitions { get; set; }

        public override HttpMethod Method => HttpMethod.Post;
        public override string UriTemplate => "postsystem.jsp{?sid,name," +
            "v7l,v7u,v7c,v7a,v7g,v8l,v8u,v8c,v8a,v8g,v9l,v9u,v9c,v9a,v9g," +
            "v10l,v10u,v10c,v10a,v10g,v11l,v11u,v11c,v11a,v11g,v12l,v12u,v12c,v12a,v12g}";

        public override IDictionary<string, object> GetUriPathParameters()
        {
            var parameters = new Dictionary<string, object>
            {
                ["sid"] = SystemId,
                ["name"] = SystemName,
            };

            AddDataDefinitions(parameters);
            return parameters;
        }

        private void AddDataDefinitions(Dictionary<string, object> parameters)
        {
            if (DataDefinitions?.Any() != true)
                return;

            foreach (IExtendedDataDefinition definition in DataDefinitions)
            {
                var index = definition.Index.ToString();
                
                if (!string.IsNullOrEmpty(definition.Label))
                {
                    parameters[$"{index}l"] = definition.Label;
                }

                if (!string.IsNullOrEmpty(definition.Unit))
                {
                    parameters[$"{index}u"] = definition.Unit;
                }

                if (!string.IsNullOrEmpty(definition.Colour))
                {
                    parameters[$"{index}c"] = definition.Colour;
                }

                if (definition.Axis.HasValue)
                {
                    parameters[$"{index}a"] = definition.Axis;
                }

                if (definition.DisplayType.HasValue)
                {
                    parameters[$"{index}g"] = definition.DisplayType.ToString();
                }
            }
        }
    }
}
