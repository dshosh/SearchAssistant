using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SearchAssistant.Infra.Dto;

namespace SearchAssistant.Infra.Spiders
{
    internal abstract class ASpider : ISpider
    {
        protected ISpiderConfiguration Configuration { get; private set; }

        public ASpider(ISpiderConfiguration configuration)
        {
            Configuration = configuration;
        }

        protected string Name => GetType().Name;

        public abstract Task<SpiderResponse> SearchAsync(SearchRequest request);

        protected async Task<int> SearchPageAsync(SearchRequest request, List<int> hits, int offset, int pageNum)
        {
            string queryString = FormatQueryString(request.Query, offset, pageNum);
            var page = await Configuration.HttpClient.GetStringAsync(queryString);
            var searchResults = GetSearchResults(page, Configuration.SearchPattern);
            hits.AddRange(Find(searchResults, request.Term.ToLower(), offset));
            return offset += searchResults.Count();
        }

        protected abstract string FormatQueryString(string query, int offset, int pageNum);

        protected abstract IEnumerable<string> GetSearchResults(string page, string searchPattern);

        private static IEnumerable<int> Find(IEnumerable<string> searchResults, string term, int offset) =>
            searchResults.Select((st, i) => new
            {
                Value = st,
                Index = offset + i + 1
            }).Aggregate(new List<int>(), (hits, r) =>
            {
                if (r.Value.Contains(term))
                {
                    hits.Add(r.Index);
                }
                return hits;
            });
    }
}
