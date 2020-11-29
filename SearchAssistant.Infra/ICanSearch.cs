using System.Threading.Tasks;
using SearchAssistant.Infra.Dto;

namespace SearchAssistant.Infra
{
    public interface ICanSearch
    {
        Task<SearchResponse> SearchAsync(SearchRequest request);
    }
}
