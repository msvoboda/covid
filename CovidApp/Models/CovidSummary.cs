using System;
using System.Collections.Generic;
using System.Text;

namespace CovidApp.Models
{
    public class CovidSummary
    {
        /// <summary>
        /// Tototal tested people
        /// </summary>
        public long totalTested
        {
            get;
            set;
        }

        /// <summary>
        ///Count people, which were positive tested
        /// </summary>
        public long infected
        {
            get;
            set;
        }

        /// <summary>
        ///Count people, which were recovered
        /// </summary>
        public long recovered
        {
            get;
            set;
        }

        /// <summary>
        ///Count people, which have been died.
        /// </summary>
        public long deceased
        {
            get;
            set;
        }

        public IEnumerable<DayValue> totalPositiveTests
        {
            get;
            set;
        }

        public IEnumerable<DayValue> numberOfTestedGraph
        {
            get;
            set;

        }

        public IEnumerable<NameValue> infectedByRegion
        {
            get;
            set;
        }

        public IEnumerable<DayValue> infectedDaily
        {
            get;
            set;
        }

        public IEnumerable<CountryNameValue> countryOfInfection
        {
            get;
            set;
        }
    }
}
