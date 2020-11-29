using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using Microsoft.Extensions.Configuration;
using SearchAssistant.Infra.Spiders;

namespace SearchAssistant.Infra
{
    public class SpiderFactory : ISpiderFactory
    {
        private readonly IConfiguration _configuration;
        private readonly IHttpClientFactory _httpFactory;

        public SpiderFactory(IConfiguration configuration, IHttpClientFactory httpFactory)
        {
            _configuration = configuration;
            _httpFactory = httpFactory;
        }

        public IEnumerable<ISpider> CreateMany(string[] types) => types.Select(type => Create(type));

        private ISpider Create(string type)
        {
            var spiderConfiguration = CreateSpiderConfiguration(type);
            return type switch
            {
                "GoogleWebSpider" => new GoogleWebSpider(spiderConfiguration),
                "BingWebSpider" => new BingWebSpider(spiderConfiguration),
                "GoogleFileSpider" => new GoogleFileSpider(spiderConfiguration),
                "BingFileSpider" => new BingFileSpider(spiderConfiguration),
                _ => throw new NotImplementedException(),
            };
        }

        public ISpider Create<TSpider>() where TSpider : ISpider
        {
            var type = typeof(TSpider);
            var spiderConfiguration = CreateSpiderConfiguration(type.Name);
            if (type == typeof(GoogleWebSpider))
            {
                return new GoogleWebSpider(spiderConfiguration);
            }
            else if (type == typeof(BingWebSpider))
            {
                return new BingWebSpider(spiderConfiguration);
            }
            else if (type == typeof(GoogleFileSpider))
            {
                return new GoogleFileSpider(spiderConfiguration);
            }
            else if (type == typeof(BingFileSpider))
            {
                return new BingFileSpider(spiderConfiguration);
            }
            throw new NotImplementedException();
        }

        private SpiderConfiguration CreateSpiderConfiguration(string type)
        {
            return new SpiderConfiguration
            {
                HttpClient = _httpFactory.CreateClient(type),
                MaxResults = _configuration.GetValue<int>("Spiders:MaxResults"),
                SearchPattern = _configuration.GetValue<string>($"Spiders:{type}:SearchPattern"),
                QueryString = _configuration.GetValue<string>($"Spiders:{type}:QueryString")
            };
        }
    }
}
