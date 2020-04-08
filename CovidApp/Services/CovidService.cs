using CovidApp.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CovidApp.Services
{

    public interface ICovidService
    {
        Task<IEnumerable<NakazaDen>> GetNakazaList();
        Task<TestData> GetTestyList();
        Task<UnoficialSummary> GetUnoficialSummary();
    }

    public class CovidService : ICovidService
    {
        Uri urlNakaza = new Uri("https://onemocneni-aktualne.mzcr.cz/api/v1/covid-19/nakaza.min.json");
        Uri urlTesty = new Uri("https://onemocneni-aktualne.mzcr.cz/api/v1/covid-19/testy.min.json");
        Uri urlUnoficial = new Uri("https://api.apify.com/v2/key-value-stores/K373S4uCFR9W1K8ei/records/LATEST?disableRedirect=true");

        HttpClientService client = new HttpClientService();

        public async Task<IEnumerable<NakazaDen>> GetNakazaList()
        {
            var result = await client.Get<IEnumerable<NakazaDen>>(urlNakaza, null, async response =>
            {
                var content = await response.Content.ReadAsStringAsync();
                IEnumerable<NakazaDen> items = JsonConvert.DeserializeObject<IEnumerable<NakazaDen>>(content);

                return items;
            });
            return result;
        }

        public async Task<TestData> GetTestyList()
        {
            var result = await client.Get<TestData>(urlTesty, null, async response =>
            {
                var content = await response.Content.ReadAsStringAsync();
                TestData items = JsonConvert.DeserializeObject<TestData>(content);
                return items;
            });
            return result;
        }

        public async Task<UnoficialSummary> GetUnoficialSummary()
        {
            var result = await client.Get<UnoficialSummary>(urlUnoficial, null, async response =>
            {
                var content = await response.Content.ReadAsStringAsync();
                try
                {
                    UnoficialSummary summary = JsonConvert.DeserializeObject<UnoficialSummary>(content);
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
    }
}
