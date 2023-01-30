using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Newtonsoft.Json;
using NZWalksWebApp.Model;
using System.Collections;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace NZWalksWebApp.Pages.Regions
{
    public class IndexModel : PageModel
    {
        private readonly IConfiguration _configuration;
        private string apiUrl;

        public IEnumerable<Region> Regions { get; set; }

        public IndexModel(IConfiguration configuration)
        {
            this._configuration = configuration;
            apiUrl = this._configuration.GetValue<string>("WebAPIUrl");
        }

        public async void OnGet()
        {
            using (HttpClient client = new HttpClient())
            {
                string endPoint = apiUrl + "/Regions";
                //HttpRequestMessage request = new HttpRequestMessage();
                //request.Method = HttpMethod.Get;
                //request.RequestUri = new Uri(endPoint);

                var rsp = await client.GetAsync(endPoint);
                if (rsp.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    string result = await rsp.Content.ReadAsStringAsync();
                    Regions = JsonConvert.DeserializeObject<IEnumerable<Region>>(result);
                }

                //var query = new Dictionary<string, string>();
                //var response = await client.GetAsync(QueryHelpers.AddQueryString(endPoint, query));

                //if (response.StatusCode == System.Net.HttpStatusCode.OK)
                //{
                //    var result = await response.Content.ReadAsStringAsync();
                //    Regions = Newtonsoft.Json.JsonConvert.DeserializeObject<IEnumerable<Region>>(result);
                //}

                //if (response.StatusCode == System.Net.HttpStatusCode.OK)
                //{
                //    string result = await response.Content.ReadAsStringAsync();
                //    Regions = Newtonsoft.Json.JsonConvert.DeserializeObject<IEnumerable<Region>>(result);
                //}

            }
        }
    }
}
