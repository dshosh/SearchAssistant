using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace SearchAssistant.Infra.Spiders
{
    internal class GoogleFileSpider : AFileSpider
    {
        public GoogleFileSpider(ISpiderConfiguration configuration)
            : base(configuration)
        {
        }

        protected override IEnumerable<string> GetSearchResults(string page, string searchPattern) =>
            Regex.Matches(page, searchPattern).Select(m => m.Value.ToLower()).Distinct();
    }
}
