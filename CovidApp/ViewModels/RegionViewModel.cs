using CovidApp.Models;
using CovidApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CovidApp.ViewModels
{
    public class RegionViewModel : BaseViewModel
    {
        ICovidService covidService => DependencyService.Get<ICovidService>() ?? new CovidService();
        public RegionViewModel()
        {
            Title = "Přehled krajů";
        }

        private UnoficialSummary _summary;

        public void LoadData()
        {
            Task<UnoficialSummary> taskSum = covidService.GetUnoficialSummary();
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
