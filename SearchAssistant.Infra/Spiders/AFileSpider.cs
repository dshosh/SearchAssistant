using System.Collections.Generic;
using System.Threading.Tasks;
using SearchAssistant.Infra.Dto;

namespace SearchAssistant.Infra.Spiders
{
    internal abstract class AFileSpider : ASpider
    {
        const int MAX_PAGE = 10;

        public AFileSpider(ISpiderConfiguration configuration) 
            : base(configuration)
        {
        }

        public override async Task<SpiderResponse> SearchAsync(SearchRequest request)
        {
            var hits = new List<int>();
            int offset = 0;
            int pageNum = 0;
            do
            {
                offset = await SearchPageAsync(request, hits, offset, pageNum);
                pageNum++;
            }
            while (offset < Configuration.MaxResults && pageNum < MAX_PAGE);
            return new SpiderResponse
            {
                SpiderName = Name,
                Hits = hits
            };
        }

        protected override string FormatQueryString(string query, int offset, int pageNum) =>
            string.Format(Configuration.QueryString, pageNum + 1, offset);
    }
}
