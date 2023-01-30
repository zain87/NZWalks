using Microsoft.AspNetCore.Mvc;

namespace NZWalksWebApp.Controllers
{
    public class RegionInfoController : Controller
    {
        private readonly IConfiguration configuration;
        private string apiUrl;

        public RegionInfoController(IConfiguration configuration)
        {
            this.configuration = configuration;
            apiUrl = this.configuration.GetValue<string>("WebAPIUrl");
        }

        //[HttpGet]
        //public async Task<IActionResult> GetAll()
        //{
        //    HttpClient client = new HttpClient();
        //    string endPoint = apiUrl + "/Regions";

        //    var regions = await client.GetAsync(endPoint);

        //}
    }
}
