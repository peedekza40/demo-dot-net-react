using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PracticeReactApp.Infrastructures.Repositories.Interfaces;
using PracticeReactApp.Server.Filters;
using PracticeReactApp.Server.ViewModels.TestAware;
using System.ComponentModel.DataAnnotations;
using System.Net.Http;

namespace PracticeReactApp.Server.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [AuthorizeForbidden(Path = "TestAware")]
    public class TestAwareController : ControllerBase
    {
        [HttpGet]
        [Route("Test1")]
        public IActionResult Test1(string? menuCode)
        {
            var menus = menuRepository.GetByCode(menuCode);
            string json = JsonConvert.SerializeObject(menus);
            return Ok(json);
        }

        [HttpPost]
        [Route("Test2")]
        public IActionResult Test2([FromBody] Test2ViewModel model)
        {
            var chars = model.P1?.Split(',').ToList() ?? new List<string>();

            var charsDub = chars.GroupBy(x => x.ToString())
                                .Where(x => x.Count() >= 2)
                                .Select(x => new { Rank = x.Key });

            var alphabetResults = charsDub
                            .Where(x => !int.TryParse(x.Rank, out _))
                            .OrderBy(x => x.Rank);

            var numberResults = charsDub
                            .Where(x => int.TryParse(x.Rank, out _))
                            .OrderBy(x => int.Parse(x.Rank));

            var results = alphabetResults.Concat(numberResults).ToList();
            return Ok(results);
        }

        [HttpGet]
        [Route("Test3")]
        public async Task<IActionResult> Test3(string? id)
        {
            if (id == null || id.ToLower() == "null")
            {
                return BadRequest("Id is required.");
            }

            var apiUrl = $"{configuration.GetValue<string>("Test3APIUrl")}{id}";

            var response = await httpClient.GetAsync(apiUrl);

            if (response.IsSuccessStatusCode)
            {
                string apiResponse = await response.Content.ReadAsStringAsync();

                return Ok(new { 
                    url = apiUrl,
                    method = "GET",
                    response = JsonConvert.DeserializeObject<Dictionary<string, object>>(apiResponse)
                });
            }
            else
            {
                return StatusCode((int)response.StatusCode, "Failed to call the API.");
            }
        }

        private readonly HttpClient httpClient;
        private readonly IConfiguration configuration;
        private readonly IMenuRepository menuRepository;

        public TestAwareController(
            HttpClient httpClient,
            IConfiguration configuration,
            IMenuRepository menuRepository)
        {
            this.httpClient = httpClient;
            this.configuration = configuration;
            this.menuRepository = menuRepository;
        }
    }
}
