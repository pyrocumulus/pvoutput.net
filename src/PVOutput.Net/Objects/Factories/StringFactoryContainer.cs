using System;
using System.Collections.Generic;
using PVOutput.Net.Objects.Core;
using PVOutput.Net.Objects.Modules;

namespace PVOutput.Net.Objects.Factories
{
    internal static class StringFactoryContainer
    {
        private static Dictionary<Type, object> ReaderFactories { get; } = new Dictionary<Type, object>();

        static StringFactoryContainer()
        {
            ReaderFactories.Add(typeof(IOutput), new OutputFactory());
            ReaderFactories.Add(typeof(ITeamOutput), new TeamOutputFactory());
            ReaderFactories.Add(typeof(IAggregatedOutput), new AggregatedOutputFactory());
            ReaderFactories.Add(typeof(ISystem), new SystemFactory());
            ReaderFactories.Add(typeof(IStatus), new StatusFactory());
            ReaderFactories.Add(typeof(IStatusHistory), new StatusHistoryFactory());
            ReaderFactories.Add(typeof(IDayStatistics), new DayStatisticsFactory());
            ReaderFactories.Add(typeof(IBatchStatusPostResult), new BatchStatusPostResultFactory());
            ReaderFactories.Add(typeof(IStatistic), new StatisticFactory());
            ReaderFactories.Add(typeof(IMissing), new MissingFactory());
            ReaderFactories.Add(typeof(ITeam), new TeamFactory());
            ReaderFactories.Add(typeof(IExtended), new ExtendedFactory());
            ReaderFactories.Add(typeof(IFavourite), new FavouriteFactory());
            ReaderFactories.Add(typeof(IInsolation), new InsolationFactory());
            ReaderFactories.Add(typeof(ISupply), new SupplyFactory());
            ReaderFactories.Add(typeof(ISystemSearchResult), new SystemSearchResultFactory());
        }

        private static object GetObjectStringFactory<TReturnType>()
        {
            Type type = typeof(TReturnType);
            if (!ReaderFactories.ContainsKey(type))
            {
                throw new InvalidOperationException($"Factory for {type} is not known");
            }

            return ReaderFactories[type];
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
