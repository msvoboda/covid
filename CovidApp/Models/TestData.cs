using System;
using System.Collections.Generic;
using System.Text;

namespace CovidApp.Models
{
    public class TestData
    {
        public String modified
        {
            get;
            set;

        }

        public String source
        {
            get;
            set;
        }

        public IEnumerable<TestDen> data
        {
            get;
            set;
        }
    }
}
