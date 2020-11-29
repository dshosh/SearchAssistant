using System.Threading.Tasks;
using SearchAssistant.Infra.Dto;

namespace SearchAssistant.Infra.Spiders
{
    public interface ISpider
    {
        Task<SpiderResponse> SearchAsync(SearchRequest request);
    }
}
