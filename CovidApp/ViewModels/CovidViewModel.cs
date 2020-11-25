﻿using CovidApp.Models;
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
        private DieData _dies;
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


        public Task<CovidSummary> LoadData()
        {
            Task<IEnumerable<NakazaDen>> task = covidService.GetNakazaList();
            task.ContinueWith(result =>
            {
                _nakaza = result.Result.ToList();
                NakazaDen last = _nakaza.Last();
                Infected = last.pocetCelkem;
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
