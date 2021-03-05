using CovApp.Models;
using CovApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CovApp.ViewModels
{
    public class RegionViewModel : BaseViewModel
    {
        ICovidService covidService => DependencyService.Get<ICovidService>() ?? new CovidService();
        public RegionViewModel()
        {
            Title = "Přehled krajů";
        }

        private NemocSummary _summary;

        public void LoadData()
        {
            Task<NemocSummary> taskSum = covidService.GetCovidSummary();
            taskSum.ContinueWith(result =>
            {
                _summary = result.Result;
                Regions = _summary.infectedByRegion.ToList();
            });
        }

        List<NameValue> _regions;
        public List<NameValue> Regions
        {
            get
            {
                return _regions;
            }
            set
            {
                _regions = value;
                OnPropertyChanged(nameof(Regions));
            }
        }
    }
}
