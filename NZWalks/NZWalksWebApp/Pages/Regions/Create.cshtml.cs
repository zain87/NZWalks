using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using NZWalksWebApp.Model;
using System.Security.Cryptography;
using System.Text;

namespace NZWalksWebApp.Pages.Regions
{
    [BindProperties]
    public class CreateModel : PageModel
    {
        private readonly IConfiguration _configuration;
        private string apiUrl;

        public Region Region { get; set; }

        public CreateModel(IConfiguration configuration)
        {
            this._configuration = configuration;
            apiUrl = this._configuration.GetValue<string>("WebAPIUrl");
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                var jsonString = JsonConvert.SerializeObject(Region);

                using (HttpClient client = new HttpClient())
                {
                    string endPoint = apiUrl + "/Regions";
                    StringContent content = new StringContent(jsonString, Encoding.UTF8, "application/json");

                    var response = await client.PostAsync(endPoint, content);
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        string result = await response.Content.ReadAsStringAsync();
                        Region = JsonConvert.DeserializeObject<Region>(result);
                        return RedirectToPage("Index");
                    }
                }
            }

            return Page();
        }
    }
}
