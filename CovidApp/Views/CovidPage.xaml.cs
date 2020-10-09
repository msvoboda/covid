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
    public partial class CovidPage : ContentPage
    {
        CovidViewModel viewModel;

        public CovidPage()
        {
            InitializeComponent();
            BindingContext = viewModel = new CovidViewModel();
            Task<CovidSummary> task = viewModel.LoadData();
            task.ContinueWith(result =>
            {
            List<ChartEntry> entries = new List<ChartEntry>();

             int cnt = 1;
             DayValue last=null;
            foreach (DayValue nv in result.Result.totalPositiveTests.ToList()) {

                    if (nv.value == 0)
                    {
                        cnt++;
                        continue;
                    }

                    ChartEntry entry = new ChartEntry(nv.value)
                    {
                        Color = SKColor.Parse("#ff3300")                 
                    };
                    if (cnt % 10 == 0)
                    {
                        entry.Label = DateTime.Parse(nv.date.Substring(0,10)).ToShortDateString();
                        entry.ValueLabel = nv.value.ToString();
                    }
                    int total = result.Result.totalPositiveTests.Count();
                    if (cnt == 1 || cnt == total)
                    {
                        entry.Label = DateTime.Parse(nv.date.Substring(0, 10)).ToShortDateString();
                        entry.ValueLabel = nv.value.ToString();
                    }

                    last = nv;
                    cnt++;
                    entries.Add(entry);
            }
            var chart = new LineChart() { Entries = entries };
                chart.ValueLabelOrientation = Orientation.Vertical;
                chart.PointMode = PointMode.None;
                chart.LineMode = LineMode.Straight;
                Chart.Chart = chart;

            });

        }
    }
}