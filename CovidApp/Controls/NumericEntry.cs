using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CovApp.Controls
{
    public class NumericEntry: Entry
    {
        public NumericEntry()
        {
            this.Keyboard = Keyboard.Numeric;
        }
    }
}
