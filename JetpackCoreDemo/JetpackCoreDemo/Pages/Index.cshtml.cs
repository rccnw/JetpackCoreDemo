using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http;


namespace JetpackCoreDemo.Pages
{
    public class IndexModel : PageModel
    {

        private readonly IHttpClientFactory _clientFactory;

        public IndexModel(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public async void OnGet()
        {
            //var result = await SendMsgToWebApi();
            await Foo();
        }



        private async Task<string> Foo()
        {
            //string apiUrl = "http://localhost:44385/api/values";

            string apiUrl = "http://jetpackcorewebapi.azurewebsites.net/api/values";



            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync(apiUrl);
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    var table = Newtonsoft.Json.JsonConvert.DeserializeObject<System.Data.DataTable>(data);
                    return data;
                }
            }
            return "error";
        }

        private async Task<string> SendMsgToWebApi()
        {
            var request = new HttpRequestMessage(
                    HttpMethod.Get, "http://localhost:44385/api/values");

                  //  "http://localhost:7071/api/Function1?name=Rob");


            // https://localhost:44385/api/values

            //"https://jetpack-devpoc-1-fn1.azurewebsites.net?name=Rob/");

            request.Headers.Add("Accept", "application/vnd.github.v3+json");
            request.Headers.Add("User-Agent", "HttpClientFactory-Sample");

            var client = _clientFactory.CreateClient();

            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                return response.Content.ToString();
            }
            else
            {
                return "error";
            }
        }


    }
}
