using CovApp.Models;
using CovApp.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CovApp.ViewModels
{
    public class InjectionViewModel : BaseViewModel
    {
        ICovidService covidService => DependencyService.Get<ICovidService>() ?? new CovidService();

        public InjectionViewModel()
        {
            Title = "Očkování";
        }

        public Task<TestData<OckovaniData>> LoadData()
        {
            Task<TestData<OckovaniData>> taskO = covidService.GetOckovaniList();
            taskO.ContinueWith(result =>
            {
                //
                OckovaniCR ockovani = new OckovaniCR(result.Result);
                OckovaniCelkem = ockovani.OckovaniCelkem;
                OckovaniDruha = ockovani.OckovaniDruha;
                OckovaniPrvni = ockovani.OckovaniPvni;
                Comirnaty = ockovani.Comirnaty;
                Moderna = ockovani.Moderna;
                AstraZeneca = ockovani.AstraZeneca;

            });
            return taskO;
        }

        long _ockovaniCelkem;
        public long OckovaniCelkem
        {
            get { return _ockovaniCelkem; }
            set { SetProperty(ref _ockovaniCelkem, value); }
        }

        long _ockovaniPrvni;
        public long OckovaniPrvni
        {
            get { return _ockovaniPrvni; }
            set { SetProperty(ref _ockovaniPrvni, value); }
        }

        long _ockovaniDruha;
        public long OckovaniDruha
        {
            get { return _ockovaniDruha; }
            set { SetProperty(ref _ockovaniDruha, value); }
        }

        long _comirnaty;
        public long Comirnaty
        {
            get { return _comirnaty; }
            set { SetProperty(ref _comirnaty, value); }
        }

        long _moderna;
        public long Moderna
        {
            get { return _moderna; }
            set { SetProperty(ref _moderna, value); }
        }

        long _astrazeneca;
        public long AstraZeneca
        {
            get { return _astrazeneca; }
            set { SetProperty(ref _astrazeneca, value); }
        }
    }
}
