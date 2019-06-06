using MedicalRecordWpfApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft;
using Newtonsoft.Json;
using System.Net;
using System.Windows;

namespace MedicalRecordWpfApp.Data
{
    class WebApiService
    {
        private const string URL = "http://localhost:58986/api/PacientsWpf";
        public HttpClient GetClient()
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("accept", "application/json");
            return client;
        }

        public IEnumerable<WebModelPacient> GetPacients()
        {
            
                HttpClient client = GetClient();
                var response = client.GetStringAsync(URL).Result;
                return JsonConvert.DeserializeObject<IEnumerable<WebModelPacient>>(response).OrderBy(u => u.Name);
         
        }
        public async Task<WebModelPacient> AddUser(WebModelPacient user)
        {
            HttpClient client = GetClient();
            var response = await client.PostAsync(URL, new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json"));

            if (response.StatusCode != HttpStatusCode.OK) return null;

            return JsonConvert.DeserializeObject<WebModelPacient>(await response.Content.ReadAsStringAsync());

        }
    }
}
