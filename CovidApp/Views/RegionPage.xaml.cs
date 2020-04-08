using CovidApp.ViewModels;
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
    public partial class RegionPage : ContentPage
    {
        RegionViewModel viewModel;

        public RegionPage()
        {
            InitializeComponent();
            BindingContext = viewModel = new RegionViewModel();
            viewModel.LoadData();
        }
    }
}