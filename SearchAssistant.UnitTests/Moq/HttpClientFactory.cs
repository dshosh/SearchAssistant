using System;
using System.Net.Http;
using Microsoft.Extensions.Configuration;

namespace SearchAssistant.UnitTests.Moq
{
    internal class HttpClientFactory : IHttpClientFactory
    {
        private readonly IConfiguration _configuration;

        public HttpClientFactory(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public HttpClient CreateClient(string type)
        {
            var client = new HttpClient
            {
                BaseAddress = new Uri(_configuration.GetValue<string>($"Spiders:{type}:BaseUrl"))
            };
            client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/86.0.4240.198 Safari/537.36");
            return client;
        }
    }
}
