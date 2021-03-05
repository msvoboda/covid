using CovApp.Models;
using CovApp.Services;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CovApp.ViewModels
{
    public class CovidViewModel : BaseViewModel
    {

        ICovidService covidService => DependencyService.Get<ICovidService>() ?? new CovidService();
        public CovidViewModel()
        {
            Title = "Denní přehled";
        }

        private List<NakazaDen> _nakaza;
        private TestData<TestDen> _testy;
        private DieData _dies;
        private NemocSummary _summary;

        private DateTime _datum;
        public DateTime Datum
        {
            get
            {
                return _datum;
            }
            set
            {
                _datum = value;
                OnPropertyChanged(nameof(Datum));
            }
        }

        long _totalNakaza = 0;
        public long Infected
        {
            get
            {
                return _totalNakaza;
            }
            set
            {
                _totalNakaza = value;
                OnPropertyChanged(nameof(Infected));
            }
        }

        long _totalTesty = 0;
        public long TotalTesty
        {
            get
            {
                return _totalTesty;
            }
            set
            {
                _totalTesty = value;
                OnPropertyChanged(nameof(TotalTesty));
            }
        }

        long _recovered = 0;
        public long Recovered
        {
            get
            {
                return _recovered;
            }
            set
            {
                _recovered = value;
                OnPropertyChanged(nameof(Recovered));
            }
        }

        long _deceased = 0;
        public long Deceased
        {
            get
            {
                return _deceased;
            }
            set
            {
                _deceased = value;
                OnPropertyChanged(nameof(Deceased));
            }
        }

        long _daydeceased;
        public long DayDeceased
        {
            get
            {
                return _daydeceased;
            }
            set
            {
                _daydeceased = value;
                OnPropertyChanged(nameof(DayDeceased));
            }
        }


        private long _dayIncrease=0;
        public long DayIncrease
        {
            get
            {
                return _dayIncrease;
            }
            set
            {
                SetProperty(ref _dayIncrease, value);
            }
        }

        private long _hospitalizace;
        public long Hospitalizace
        {
            get
            {
                return _hospitalizace;
            }
            set
            {
                SetProperty(ref _hospitalizace, value);
            }
        }

        private long _hospitalizaceHard;
        public long HospitalizaceHard
        {
            get
            {
                return _hospitalizaceHard;
            }
            set
            {
                SetProperty(ref _hospitalizaceHard, value);
            }
        }


        public Task<NemocSummary> LoadData()
        {
            Task<NemocSummary> taskSum = covidService.GetCovidSummary();
            taskSum.ContinueWith(result =>
            {
                _summary = result.Result;
                Recovered = _summary.recovered;
                Deceased = _summary.deceased;
                Infected = _summary.infected;
                TotalTesty = _summary.totalTested;
                Datum = _summary.lastUpdatedAtSource;
                try
                {
                    DayIncrease = _summary.infectedDaily.Last().value;
                }
                catch(Exception e)
                {
                    Console.WriteLine(e);
                }
            });

            Task<DieData> taskDie = covidService.GetDieList();
            taskDie.ContinueWith(result =>
            {
                _dies = result.Result;
                DieDay today = _dies.data.Last();
                DieDay yesterday = _dies.data[_dies.data.Count - 2];
                Console.WriteLine($"{today.datum}; {yesterday.datum}");
                DayDeceased = today.kumulativni_pocet_umrti- yesterday.kumulativni_pocet_umrti;
               
            });

            Task<TestData<HospitalizaceData>> taskHospitalizace = covidService.GetHospitalizaceList();
            taskHospitalizace.ContinueWith(result =>
            {
                TestData<HospitalizaceData> hospitalizace = result.Result;
                if (hospitalizace != null)
                {
                     HospitalizaceData last = hospitalizace.data.Last<HospitalizaceData>();
                    Hospitalizace = last.pocet_hosp;
                    HospitalizaceHard = last.stav_tezky;
                }

            });


            Task<TestData<TestDen>> taskTest = covidService.GetTestyList();
            taskTest.ContinueWith(result =>
            {
                _testy = result.Result;
                TestDen last = _testy.data.Last();
            });

            return taskSum;
        }
    }
}
