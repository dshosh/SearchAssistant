using System.Net.Http;

namespace SearchAssistant.Infra
{
    internal interface ISpiderConfiguration
    {
        HttpClient HttpClient { get; set; }
        int MaxResults { get; set; }
        string QueryString { get; set; }
        string SearchPattern { get; set; }
    }
}
