using PVOutput.Net.Objects.Outputs;
using PVOutput.Net.Objects.Core;
using PVOutput.Net.Objects.Systems;
using System;
using System.Collections.Generic;
using System.Text;
using PVOutput.Net.Objects.Status;

namespace PVOutput.Net.Objects.Factories
{
    internal static class StringFactoryContainer
    {
        private static readonly Dictionary<Type, object> _readerFactories = new Dictionary<Type, object>();

        static StringFactoryContainer()
        {
            _readerFactories.Add(typeof(IOutput), new OutputFactory());
            _readerFactories.Add(typeof(ITeamOutput), new TeamOutputFactory());
            _readerFactories.Add(typeof(IAggregatedOutput), new AggregatedOutputFactory());
            _readerFactories.Add(typeof(ISystem), new SystemFactory());
			_readerFactories.Add(typeof(IStatus), new StatusFactory());
			_readerFactories.Add(typeof(IStatusHistory), new StatusHistoryFactory());
		}

        public static IStringFactory<TReturnType> GetStringFactory<TReturnType>()
        {
            var type = typeof(TReturnType);

            if (!_readerFactories.ContainsKey(type))
                throw new NotSupportedException($"Factory for {type} is not known");

            return (IStringFactory<TReturnType>)_readerFactories[type];
        }

        public static IObjectStringReader<TReturnType> CreateObjectReader<TReturnType>()
        {
            var factory = GetStringFactory<TReturnType>();
            return factory.CreateObjectReader();
        }

        public static IArrayStringReader<TReturnType> CreateArrayReader<TReturnType>()
        {
            var factory = GetStringFactory<TReturnType>();
            return factory.CreateArrayReader();
        }
    }
}
