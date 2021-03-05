using System;
using System.Collections.Generic;
using System.Text;

namespace CovApp.Models
{
    public class TestData<T>
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

        public IEnumerable<T> data
        {
            get;
            set;
        }
    }
}
