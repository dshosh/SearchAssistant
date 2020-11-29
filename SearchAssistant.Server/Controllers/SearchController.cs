using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SearchAssistant.Infra;
using SearchAssistant.Infra.Dto;

namespace SearchAssistant.Server.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        private readonly ICanSearch _dispatcher;

        public SearchController(ICanSearch dispatcher)
        {
            _dispatcher = dispatcher;
        }

        [HttpPost]
        public async Task<IEnumerable<string>> Search(SearchRequest request)
        {
            var response = await _dispatcher.SearchAsync(request);
            return response.SpiderResponses.Select(sr => sr.ToString());
        }

        [HttpGet("/search")]
        public async Task<SearchResponse> SearchGet([FromQuery] SearchRequest request) => await _dispatcher.SearchAsync(request);
    }
}
