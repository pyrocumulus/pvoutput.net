﻿using System;
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

        public static IStringFactory<TReturnType> GetStringFactory<TReturnType>()
        {
            var type = typeof(TReturnType);

            if (!_readerFactories.ContainsKey(type))
            {
                throw new NotSupportedException($"Factory for {type} is not known");
            }

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
