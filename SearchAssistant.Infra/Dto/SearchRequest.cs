namespace SearchAssistant.Infra.Dto
{
    public class SearchRequest
    {
        public string Query { get; set; }

        public string Term { get; set; }

        public string[] Spiders { get; set; }
    }
}