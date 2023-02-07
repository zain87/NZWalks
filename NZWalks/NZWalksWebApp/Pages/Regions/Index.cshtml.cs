using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using NZWalksWebApp.Model;

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

        public async Task<IActionResult> OnGet()
        {
            using (HttpClient client = new HttpClient())
            {
                string endPoint = apiUrl + "/Regions";
                var rsp = await client.GetAsync(endPoint);
                if (rsp.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    string result = await rsp.Content.ReadAsStringAsync();
                    Regions = JsonConvert.DeserializeObject<IEnumerable<Region>>(result);
                }

                return Page();
            }
        }
    }
}
