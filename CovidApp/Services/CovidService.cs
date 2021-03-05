using CovApp.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CovApp.Services
{

    public interface ICovidService
    {
        //Task<IEnumerable<NakazaDen>> GetNakazaList();
        Task<TestData<TestDen>> GetTestyList();
        Task<NemocSummary> GetCovidSummary();
        Task<DieData> GetDieList();

        Task<TestData<HospitalizaceData>> GetHospitalizaceList();
    }

    public class CovidService : ICovidService
    {
        const String urlid = "id-19";
        
        Uri urlTesty = new Uri("https://onemocneni-aktualne.mzcr.cz/api/v1/cov"+urlid+"/testy.min.json");
        Uri urlCovidData = new Uri("https://api.apify.com/v2/key-value-stores/K373S4uCFR9W1K8ei/records/LATEST?disableRedirect=true");
        Uri urlDieData = new Uri("https://onemocneni-aktualne.mzcr.cz/api/v2/cov"+urlid+"/nakazeni-vyleceni-umrti-testy.min.json");
        Uri urlHospitalizace = new Uri("https://onemocneni-aktualne.mzcr.cz/api/v2/covid-19/hospitalizace.json");

        HttpClientService client = new HttpClientService();

    
        public async Task<TestData<TestDen>> GetTestyList()
        {
            var result = await client.Get<TestData<TestDen>>(urlTesty, null, async response =>
            {
                var content = await response.Content.ReadAsStringAsync();
                TestData<TestDen> items = JsonConvert.DeserializeObject<TestData<TestDen>>(content);
                return items;
            });
            return result;
        }

        public async Task<NemocSummary> GetCovidSummary()
        {
            var result = await client.Get<NemocSummary>(urlCovidData, null, async response =>
            {
                var content = await response.Content.ReadAsStringAsync();
                try
                {
                    NemocSummary summary = JsonConvert.DeserializeObject<NemocSummary>(content);
                    return summary;
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex);               
                }
                return null;
            });
            return result;
        }

        public async Task<DieData> GetDieList()
        {
            var result = await client.Get<DieData>(urlDieData, null, async response =>
            {
                var content = await response.Content.ReadAsStringAsync();
                DieData items = JsonConvert.DeserializeObject<DieData>(content);
                return items;
            });
            return result;
        }

        public async Task<TestData<HospitalizaceData>> GetHospitalizaceList()
        {
            var result = await client.Get<TestData<HospitalizaceData>>(urlHospitalizace, null, async response =>
            {
                var content = await response.Content.ReadAsStringAsync();
                TestData<HospitalizaceData> items = JsonConvert.DeserializeObject<TestData<HospitalizaceData>>(content);
                return items;
            });
            return result;
        }
    }
}
