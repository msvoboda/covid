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
    public partial class CovidPage : ContentPage
    {
        CovidViewModel viewModel;

        public CovidPage()
        {
            InitializeComponent();
            BindingContext = viewModel = new CovidViewModel();
            viewModel.LoadData();
        }
    }
}