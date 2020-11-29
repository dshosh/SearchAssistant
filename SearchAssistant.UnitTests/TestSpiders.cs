using System.Linq;
using Microsoft.Extensions.Configuration;
using SearchAssistant.Infra;
using SearchAssistant.Infra.Dto;
using Xunit;

namespace SearchAssistant.UnitTests
{
    public class TestSpiders
    {
        private IConfiguration Configuration;
        private ISpiderFactory SpiderFactory;

        public TestSpiders()
        {
            Configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            SpiderFactory = new SpiderFactory(Configuration, new Moq.HttpClientFactory(Configuration));
        }

        [Theory(DisplayName = "BingFileSpider: Correct Number of Hits", Timeout = 5000)]
        [InlineData("infotrack", 3)]
        [InlineData("iNfoTrAcK", 3)]
        [InlineData("propertyregistry.com.au", 5)]
        [InlineData("Unexpected", 0)]
        public async void BingFileTests1(string term, int expected)
        {
            string type = "BingFileSpider";
            var spiders = SpiderFactory.CreateMany(new[] { type });
            var searchRequest = new SearchRequest
            {
                Query = "",
                Term = term
            };
            var result = await spiders.First().SearchAsync(searchRequest);

            Assert.Equal(type, result.SpiderName);
            Assert.Equal(expected, result.Hits.Count());
        }

        [Theory(DisplayName = "BingFileSpider: Correct Hits", Timeout = 5000)]
        [InlineData("infotrack", new[] { 1, 18, 27 })]
        [InlineData("onproperty.com.au", new[] { 14, 21 })]
        public async void BingFileTests2(string term, int[] expected)
        {
            string type = "BingFileSpider";
            var spiders = SpiderFactory.CreateMany(new[] { type });
            var searchRequest = new SearchRequest
            {
                Query = "",
                Term = term
            };
            var result = await spiders.First().SearchAsync(searchRequest);

            Assert.Equal(expected, result.Hits);
        }
    }
}
