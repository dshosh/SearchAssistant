using System.Collections.Generic;

namespace SearchAssistant.Infra.Dto
{
    public class SpiderResponse
    {
        public string SpiderName { get; set; }

        public IEnumerable<int> Hits { get; set; }

        public override string ToString() => $"{SpiderName}: {string.Join(",", Hits)}".TrimEnd(',');
    }
}
