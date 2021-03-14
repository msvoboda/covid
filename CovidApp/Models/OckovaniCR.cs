using System;
using System.Collections.Generic;
using System.Text;

namespace CovApp.Models
{
    public class OckovaniKraj : List<OckovaniData>
    {
        public new void Add(OckovaniData vacine)
        {
            if (String.IsNullOrEmpty(Name))
            {
                Name = vacine.kraj_nazev;
            }
            Total += vacine.celkem_davek;
            base.Add(vacine);
        }

        public string Name
        {
            get; set;
        }

        public long Total
        {
            get;
            set;
        }
    }

    public class OckovaniVacine : List<OckovaniData>
    {
        public OckovaniVacine(OckovaniData d)
        {
            Name = d.vakcina;
        }        
        public new void Add(OckovaniData vacine)
        {
            if (String.IsNullOrEmpty(Name))
            {
                Name = vacine.vakcina;
            }
            Total += vacine.celkem_davek;
            base.Add(vacine);
        }

        public string Name
        {
            get;set;
        }

        public long Total
        {
            get;
            set;
        }
    }

    public class OckovaniCR
    {
        TestData<OckovaniData> _data;
        Dictionary<string, OckovaniKraj> _kraje=new Dictionary<string, OckovaniKraj>();
        //Dictionary<string, OckovaniVacine> _vacine = new Dictionary<string, OckovaniVacine>();

        public OckovaniCR(TestData<OckovaniData> data)
        {
            _data = data;
            ProcessData(data);

        }

        private void ProcessData(TestData<OckovaniData> data)
        {
            foreach (OckovaniData d in data.data)
            {
                OckovaniKraj kraj = null;
                if (!_kraje.ContainsKey(d.kraj_nuts_kod))
                {
                    kraj = new OckovaniKraj();
                    _kraje.Add(d.kraj_nuts_kod, kraj);
                }
                else
                {
                    kraj = _kraje[d.kraj_nuts_kod];
                }
                kraj.Add(d);

                /*
                OckovaniVacine vacine = null; 
                if (!_vacine.ContainsKey(d.vakcina))
                {
                    vacine = new OckovaniVacine(d);
                    _vacine.Add(d.vakcina, vacine);
                }
                else {
                    vacine = _vacine[d.vakcina];
                }
                vacine.Add(d);*/
                if ("Comirnaty".Equals(d.vakcina))
                {
                    _comirnaty+= d.celkem_davek; 
                }
                else if ("COVID-19 Vaccine Moderna".Equals(d.vakcina))
                {
                    _moderna+= d.celkem_davek; 
                }
                else if ("COVID-19 Vaccine AstraZeneca".Equals(d.vakcina))
                {
                    _astrazeneca+= d.celkem_davek; 
                }
                

                _totalInjection += d.celkem_davek;
                _secondInjection += d.druhych_davek;
                _firstInjection += d.prvnich_davek;
            }
        }
        long _comirnaty = 0;
        long _moderna = 0;
        long _astrazeneca = 0;

        public long Comirnaty
        {
            get { return _comirnaty; }
        }

        public long Moderna
        {
            get { return _moderna; }
        }
        public long AstraZeneca
        {
            get { return _astrazeneca; }
        }

        long _totalInjection=0;
        public long OckovaniCelkem
        {
            get { return _totalInjection; }
        }

        long _firstInjection = 0;
        public long OckovaniPvni
        {
            get { return _firstInjection; }
        }

        long _secondInjection = 0;
        public long OckovaniDruha
        {
            get { return _secondInjection; }
        }
    }
}
