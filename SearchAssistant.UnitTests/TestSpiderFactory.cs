using System.Linq;
using Microsoft.Extensions.Configuration;
using SearchAssistant.Infra;
using Xunit;

namespace SearchAssistant.UnitTests
{
    public class TestSpiderFactory
    {
        private IConfiguration Configuration;
        private ISpiderFactory SpiderFactory;

        public TestSpiderFactory()
        {
            Configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            SpiderFactory = new SpiderFactory(Configuration, new Moq.HttpClientFactory(Configuration));
        }

        [Theory(DisplayName = "SpiderFactory: Create Correct Spider")]
        [InlineData("GoogleWebSpider")]
        [InlineData("BingWebSpider")]
        [InlineData("GoogleFileSpider")]
        [InlineData("BingFileSpider")]
        public void SpiderFactoryTests1(string type)
        {
            var spiders = SpiderFactory.CreateMany(new[] { type });
            Assert.Equal(type, spiders.First().GetType().Name);
        }

        [Theory(DisplayName = "SpiderFactory: Create Many Spiders")]
        [InlineData( "GoogleWebSpider", "BingWebSpider" )]
        public void SpiderFactoryTests2(params string[] types)
        {
            var spiders = SpiderFactory.CreateMany( types );
            Assert.Equal(types.Count(), spiders.Count());
        }
    }
}
