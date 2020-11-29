using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using SearchAssistant.Infra.Dto;

namespace SearchAssistant.Infra.Spiders
{
    internal abstract class AWebSpider : ASpider
    {
        const int PAGE_SIZE = 10;

        public AWebSpider(ISpiderConfiguration configuration)
            : base(configuration)
        {
        }

        public override async Task<SpiderResponse> SearchAsync(SearchRequest request)
        {
            var hits = new List<int>();
            int offset = 0;
            do
            {
                offset = await SearchPageAsync(request, hits, offset, 0);
            }
            while (offset < Configuration.MaxResults);
            return new SpiderResponse
            {
                SpiderName = Name,
                Hits = hits
            };
        }

        protected override string FormatQueryString(string query, int offset, int pageNum = 0) =>
            string.Format(Configuration.QueryString, WebUtility.UrlEncode(query), offset, PAGE_SIZE);
    }
}
