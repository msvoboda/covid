using CovApp.ViewModels;
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
    public partial class InjectionPage : ContentPage
    {
        InjectionViewModel viewModel;

        public InjectionPage()
        {
            InitializeComponent();
            viewModel = new InjectionViewModel();
            BindingContext = viewModel;
            viewModel.LoadData();
                
        }
    }
}