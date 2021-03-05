using System;
using System.Collections.Generic;
using System.Text;

namespace CovApp.Models
{
    public class HospitalizaceData
    {
        public String datum { get; set; }
        public int pacient_prvni_zaznam { get; set; }

        public int kum_pacient_prvni_zaznam { get; set; }
        public int pocet_hosp { get; set; }

        public int stav_bez_priznaku { get; set; }
        public int stav_lehky { get; set; }
        public int stav_stredni { get; set; }
        public int stav_tezky { get; set; }
        public int jip { get; set; }
        public int kyslik { get; set; }
        public int hfno { get; set; }
        public int upv { get; set; }

        public int ecmo { get; set; }

        public int tezky_upv_ecmo { get; set; }
        public int umrti { get; set; }

        public int kum_umrti { get; set; }
    }
}
