using CovApp.ChartData;
using CovApp.Models;
using CovApp.ViewModels;
using Microcharts;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CovApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CovidPage : ContentPage
    {
        CovidViewModel viewModel;

        public CovidPage()
        {
            InitializeComponent();
            BindingContext = viewModel = new CovidViewModel();
            Task<NemocSummary> task = viewModel.LoadData();
            task.ContinueWith(result =>
            {
                TimeSeries timeValues = new TimeSeries();

                foreach (DayValue nv in result.Result.totalPositiveTests.ToList())
                {

                    TimeValue value = new TimeValue()
                    {
                        DateTime = DateTime.Parse(nv.date),
                        Value = (float)nv.value
                    };
                    timeValues.Add(value);

                }

                chartSimple.setTimeSeries(timeValues);
            });

        }
    }
}