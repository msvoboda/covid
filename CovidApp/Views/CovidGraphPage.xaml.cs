using CovidApp.ChartData;
using CovidApp.Models;
using CovidApp.ViewModels;
using Microcharts;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CovidApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CovidGraphPage : ContentPage
    {
        CovidViewModel viewModel;

        public CovidGraphPage()
        {
            InitializeComponent();
            BindingContext = viewModel = new CovidViewModel();
            Task<CovidSummary> task = viewModel.LoadData();
            task.ContinueWith(result =>
            {
                List<ChartEntry> entries = new List<ChartEntry>();
                TimeSeries timeValues = new TimeSeries();

                int cnt = 1;
                DayValue last = null;
                foreach (DayValue nv in result.Result.totalPositiveTests.ToList())
                {
                    if (last == null)
                    {
                        last = nv;
                    }

                    if (nv.value == 0)
                    {
                        cnt++;
                        continue;
                    }

                    int rozdil = nv.value - last.value;               
                    last = nv;
                    cnt++;
    

                    TimeValue value = new TimeValue()
                    {
                        DateTime = DateTime.Parse(nv.date),
                        Value = (float)rozdil
                    };                    
                    timeValues.Add(value);

                }

                chartSimple.setTimeSeries(timeValues);
            });
        }
    }
}