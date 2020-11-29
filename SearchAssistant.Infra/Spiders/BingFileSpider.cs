using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace SearchAssistant.Infra.Spiders
{
    internal class BingFileSpider : AFileSpider
    {
        public BingFileSpider(ISpiderConfiguration configuration)
            : base(configuration)
        {
        }

        protected override IEnumerable<string> GetSearchResults(string page, string searchPattern) =>
            Regex.Matches(page, searchPattern)
            .Select(m => m.Value.ToLower())
            .Where(v => !v.Contains("bing"));
    }
}
