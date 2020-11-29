using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SearchAssistant.Infra.Dto;
using SearchAssistant.Infra.Spiders;

namespace SearchAssistant.Infra
{
    public class Dispatcher : ICanSearch
    {
        private readonly ISpiderFactory _spiderFactory;

        public Dispatcher(ISpiderFactory spiderFactory)
        {
            _spiderFactory = spiderFactory;
        }

        public async Task<SearchResponse> SearchAsync(SearchRequest searchRequest)
        {
            // Parallel for async tasks:
            IEnumerable<ISpider> spiders = _spiderFactory.CreateMany(searchRequest.Spiders);
            var tasks = spiders.Select(spider => spider.SearchAsync(searchRequest));
            return new SearchResponse(await Task.WhenAll(tasks));
        }
    }
}
