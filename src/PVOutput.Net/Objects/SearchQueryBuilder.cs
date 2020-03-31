using System;
using System.Collections.Generic;
using System.Text;
using PVOutput.Net.Enums;
using PVOutput.Net.Objects.Modules.Implementations;

namespace PVOutput.Net.Objects
{
    /// <summary>
    /// Builder that creates structured queries to search systems with on PVOutput.
    /// </summary>
    public class SearchQueryBuilder
    {
        internal SearchQuery _searchQuery;

        internal SearchQueryBuilder()
        {
            _searchQuery = new SearchQuery();
        }

        /// <summary>
        /// Find systems where the name starts with.
        /// </summary>
        /// <param name="text">Text the system name should start with.</param>
        /// <returns>The builder.</returns>
        public SearchQueryBuilder WithNameStartsWith(string text)
        {
            _searchQuery.NameStartsWith = text;
            return this;
        }

        /// <summary>
        /// Find systems where the name contains.
        /// </summary>
        /// <param name="text">Text the system name should contain.</param>
        /// <returns>The builder.</returns>
        public SearchQueryBuilder WithNameContains(string text)
        {
            _searchQuery.NameContains = text;
            return this;
        }

        /// <summary>
        /// Find systems where the postcode or total size begins with a value.
        /// </summary>
        /// <param name="value">Value the postcode or total size should begin with.</param>
        /// <returns>The builder.</returns>
        public SearchQueryBuilder WithPostcodeOrPower(int value)
        {
            _searchQuery.PostcodeOrPower = value;
            return this;
        }

        /// <summary>
        /// Find systems where the postcode begins with a value.
        /// </summary>
        /// <param name="postcode">Text the postcode should begin.</param>
        /// <returns>The builder.</returns>
        public SearchQueryBuilder WithPostcode(string postcode)
        {
            _searchQuery.Postcode = postcode;
            return this;
        }

        /// <summary>
        /// Find systems where the total size begins with a value.
        /// </summary>
        /// <param name="size">Number the size should begin with.</param>
        /// <returns>The builder.</returns>
        public SearchQueryBuilder WithSize(int size)
        {
            _searchQuery.Size = size;
            return this;
        }

        /// <summary>
        /// Find systems where the panel name contains a value.
        /// </summary>
        /// <param name="panel">Text the panel name should contain.</param>
        /// <returns>The builder.</returns>
        public SearchQueryBuilder WithPanel(string panel)
        {
            _searchQuery.Panel = panel;
            return this;
        }

        /// <summary>
        /// Find systems where the inverter name contains a value.
        /// </summary>
        /// <param name="inverter">Text the inverter name should contain.</param>
        /// <returns>The builder.</returns>
        public SearchQueryBuilder WithInverter(string inverter)
        {
            _searchQuery.Inverter = inverter;
            return this;
        }

        /// <summary>
        /// Find systems within a distance.
        /// </summary>
        /// <param name="kilometers">Kilometers the system should be within.</param>
        /// <param name="postcode">Postcode that is searched from. <c>Own system if empty</c>.</param>
        /// <returns>The builder.</returns>
        public SearchQueryBuilder WithinDistance(int kilometers, int? postcode = null)
        {
            _searchQuery.DistanceKilometers = kilometers;
            _searchQuery.DistancePostcode = postcode;
            return this;
        }

        /// <summary>
        /// Find systems where the team name contains a value.
        /// </summary>
        /// <param name="teamName">Text the team name should contain.</param>
        /// <returns>The builder.</returns>
        public SearchQueryBuilder WithTeamName(string teamName)
        {
            _searchQuery.TeamName = teamName;
            return this;
        }

        /// <summary>
        /// Find systems with a certain orientation.
        /// </summary>
        /// <param name="orientation">Orientation the systems should have.</param>
        /// <returns>The builder.</returns>
        public SearchQueryBuilder WithOrientation(Orientation orientation)
        {
            _searchQuery.Orientation = orientation;
            return this;
        }

        /// <summary>
        /// compare search with outputs in a certain period.
        /// </summary>
        /// <param name="retrospect">Period the systems should have outputs in.</param>
        /// <returns>The builder.</returns>
        public SearchQueryBuilder WithRetrospect(TimeSpan retrospect)
        {
            _searchQuery.PreviousPeriod = retrospect;
            return this;
        }

        /// <summary>
        /// compare search with outputs on a certain date.
        /// </summary>
        /// <param name="date">Date the systems should have outputs on.</param>
        /// <returns>The builder.</returns>
        public SearchQueryBuilder WithRetrospect(DateTime date)
        {
            _searchQuery.SpecificDate = date;
            return this;
        }

        /// <summary>
        /// Find systems with a certain tilt.
        /// </summary>
        /// <param name="tilt">Tilt the systems should have.</param>
        /// <returns>The builder.</returns>
        public SearchQueryBuilder WithTilt(int tilt)
        {
            _searchQuery.Tilt = tilt;
            return this;
        }

        /// <summary>
        /// Resets the builder to it's default state. Ready to build a new search query.
        /// </summary>
        public void Reset()
        {
            _searchQuery = new SearchQuery();
        }

        /// <summary>
        /// Uses information within the builder to return the built status.
        /// </summary>
        /// <returns>The search query.</returns>
        public ISearchQuery Build()
        {
            return _searchQuery;
        }

        /// <summary>
        /// Uses information within the builder to return the built output.
        /// Resets the builder to it's default state after building.
        /// </summary>
        /// <returns>The SearchQuery.</returns>
        public ISearchQuery BuildAndReset()
        {
            ISearchQuery result = Build();
            Reset();
            return result;
        }
    }
}
