using System;
using System.Collections.Generic;
using System.Text;

namespace CovApp.Models
{
    public class OckovaniData
    {
        public String datum { get; set; }
        public string vakcina { get; set; }
        public string kraj_nuts_kod { get; set; }
        
        public string kraj_nazev { get; set; }
        public string vekova_skupina { get; set; }
        
        public int prvnich_davek { get; set; }
        
        public int druhych_davek { get; set; }
        
        public int celkem_davek { get; set; }
    }
}
