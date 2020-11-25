using System;
using System.Collections.Generic;
using System.Text;

namespace CovidApp.Models
{
    public class DieData
    {
        public String modified { get; set; }
        public String source { get; set; }
        public List<DieDay> data { get; set; }
    }

    public class DieDay
    {
        public String datum { get; set; }
        public Int32 kumulativni_pocet_nakazenych { get; set; }
        public Int32 kumulativni_pocet_vylecenych { get; set; }
        public Int32 kumulativni_pocet_umrti { get; set; }

        public Int32 kumulativni_pocet_testu { get; set; }
    }
}
