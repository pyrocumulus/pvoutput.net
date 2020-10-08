using System;
using System.Collections.Generic;
using PVOutput.Net.Objects.Core;
using PVOutput.Net.Objects.Modules;

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
            _readerFactories.Add(typeof(IDayStatistics), new DayStatisticsFactory());
            _readerFactories.Add(typeof(IBatchStatusPostResult), new BatchStatusPostResultFactory());
            _readerFactories.Add(typeof(IStatistic), new StatisticFactory());
            _readerFactories.Add(typeof(IMissing), new MissingFactory());
            _readerFactories.Add(typeof(ITeam), new TeamFactory());
            _readerFactories.Add(typeof(IExtended), new ExtendedFactory());
            _readerFactories.Add(typeof(IFavourite), new FavouriteFactory());
            _readerFactories.Add(typeof(IInsolation), new InsolationFactory());
            _readerFactories.Add(typeof(ISupply), new SupplyFactory());
            _readerFactories.Add(typeof(ISystemSearchResult), new SystemSearchResultFactory());
        }

        private static object GetObjectStringFactory<TReturnType>()
        {
            Type type = typeof(TReturnType);
            if (!_readerFactories.ContainsKey(type))
            {
                throw new InvalidOperationException($"Factory for {type} is not known");
            }

            return _readerFactories[type];
        }

        public static IObjectStringReader<TReturnType> CreateObjectReader<TReturnType>()
        {
            // Currently every factory is an ObjectStringFactory at minimum
            var factory = GetObjectStringFactory<TReturnType>() as IObjectStringFactory<TReturnType>;
            return factory.CreateObjectReader();
        }

        public static IArrayStringReader<TReturnType> CreateArrayReader<TReturnType>()
        {
            var factory = GetObjectStringFactory<TReturnType>();
            if (factory is IArrayStringFactory<TReturnType> arrayFactory)
            {
                return arrayFactory.CreateArrayReader();
            }
            throw new InvalidOperationException($"Factory for {typeof(TReturnType)} is not an array factory");
        }
    }
}
