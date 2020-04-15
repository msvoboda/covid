using CovidApp.Models;
using CovidApp.Services;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CovidApp.ViewModels
{
    public class CovidViewModel : BaseViewModel
    {

        ICovidService covidService => DependencyService.Get<ICovidService>() ?? new CovidService();
        public CovidViewModel()
        {
            Title = "Denní přehled";
        }

        private List<NakazaDen> _nakaza;
        private TestData _testy;
        private CovidSummary _summary;

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
        public long TotalNakazeni
        {
            get
            {
                return _totalNakaza;
            }
            set
            {
                _totalNakaza = value;
                OnPropertyChanged(nameof(TotalNakazeni));
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


        public Task<CovidSummary> LoadData()
        {
            Task<IEnumerable<NakazaDen>> task = covidService.GetNakazaList();
            task.ContinueWith(result =>
            {
                _nakaza = result.Result.ToList();
                NakazaDen last = _nakaza.Last();
                TotalNakazeni = last.pocetCelkem;
                DateTime dt;
                DateTime.TryParse(last.datum, out dt);
                Datum =  dt;
            });

            Task<CovidSummary> taskSum = covidService.GetCovidSummary();
            taskSum.ContinueWith(result =>
            {
                _summary = result.Result;
                Recovered = _summary.recovered;
                Deceased = _summary.deceased;
                TotalNakazeni = _summary.infected;
                TotalTesty = _summary.totalTested;
                try
                {
                    DayIncrease = _summary.infectedDaily.Last().value;
                }
                catch(Exception e)
                {
                    Console.WriteLine(e);
                }
            });

            Task<TestData> taskTest = covidService.GetTestyList();
            taskTest.ContinueWith(result =>
            {
                _testy = result.Result;
                TestDen last = _testy.data.Last();

            });

            return taskSum;
        }
    }
}
