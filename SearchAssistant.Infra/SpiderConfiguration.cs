using System.Net.Http;

namespace SearchAssistant.Infra
{
    internal class SpiderConfiguration : ISpiderConfiguration
    {
        public int MaxResults { get; set; }
        public string SearchPattern { get; set; }
        public string QueryString { get; set; }
        public HttpClient HttpClient { get; set; }
    }
}
