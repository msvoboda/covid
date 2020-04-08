using System;
using System.Collections.Generic;
using System.Text;

namespace CovidApp.Models
{
    public class UnoficialSummary
    {
        public int totalTested
        {
            get;
            set;
        }

        public int infected
        {
            get;
            set;
        }

        public int recovered
        {
            get;
            set;
        }

        public int deceased
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
