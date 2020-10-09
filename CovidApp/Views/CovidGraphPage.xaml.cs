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
                    ChartEntry entry = new ChartEntry(rozdil);
                    
                    if (rozdil < 100)
                    {
                        entry.Color = SKColor.Parse("#00ff00");
                    }
                    else if (rozdil < 1000)
                    {
                        entry.Color = SKColor.Parse("#FFBE33");
                    }
                    else
                    {
                        entry.Color = SKColor.Parse("#ff3300");
                    }
                  
                    if (cnt % 10 == 0)
                    {
                        entry.Label = DateTime.Parse(nv.date.Substring(0, 10)).ToShortDateString().Trim();
                        entry.ValueLabel = rozdil.ToString();
                    }
                    int total = result.Result.totalPositiveTests.Count();
                    if (cnt == 1 || cnt == total)
                    {
                        entry.Label = DateTime.Parse(nv.date.Substring(0, 10)).ToShortDateString().Trim();
                        entry.ValueLabel = rozdil.ToString();
                    }

                    last = nv;
                    cnt++;
                    entries.Add(entry);
      
                }
                var chart = new LineChart() { Entries = entries };
                chart.ValueLabelOrientation = Orientation.Vertical;
                chart.LineMode = LineMode.Straight;
                //chart.LabelTextSize = 12;
                chart.LineSize = 2;
                Chart.Chart = chart;

            });
        }
    }
}