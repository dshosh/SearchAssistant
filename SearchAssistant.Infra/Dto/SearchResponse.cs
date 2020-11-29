using System.Collections.Generic;

namespace SearchAssistant.Infra.Dto
{
    public class SearchResponse
    {
        public SearchResponse(IEnumerable<SpiderResponse> responses)
        {
            SpiderResponses = responses;
        }

        public IEnumerable<SpiderResponse> SpiderResponses { get; private set; }
    }
}
