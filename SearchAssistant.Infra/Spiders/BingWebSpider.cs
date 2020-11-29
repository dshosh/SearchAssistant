using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace SearchAssistant.Infra.Spiders
{
    internal class BingWebSpider : AWebSpider
    {
        public BingWebSpider(ISpiderConfiguration configuration)
            : base(configuration)
        {
        }

        protected override IEnumerable<string> GetSearchResults(string page, string searchPattern) =>
            Regex.Matches(page, searchPattern)
            .Select(m => m.Value.ToLower())
            .Where(v => !v.Contains("bing"));
    }
}
