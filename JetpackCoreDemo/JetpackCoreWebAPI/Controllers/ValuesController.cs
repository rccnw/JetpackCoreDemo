using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;


namespace JetpackCoreWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly IHttpClientFactory _clientFactory;

        public ValuesController(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }


        // GET api/values
        [HttpGet]
        public async Task<ActionResult<IEnumerable<string>>> Get()
        {
            var result = await SendMessageToAzureFunction();


            return new string[] { "value1", "value2" };
        }


        private async Task<string> SendMessageToAzureFunction()
        {
            var client = _clientFactory.CreateClient();
            var result = await client.GetStringAsync("https://jetpackfunctionapp1.azurewebsites.net?name=Rob/");
            return result;
        }


        private async Task<string> SendMessageToAzureFunctionOLD()
        {
            var request = new HttpRequestMessage(
                    HttpMethod.Get,
                    "https://jetpackfunctionapp1.azurewebsites.net?name=Rob/");

                    //  "http://localhost:7071/api/Function1?name=Rob");
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


        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
