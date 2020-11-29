using System.Collections.Generic;
using SearchAssistant.Infra.Spiders;

namespace SearchAssistant.Infra
{
    public interface ISpiderFactory
    {
        ISpider Create<T>() where T : ISpider;

        IEnumerable<ISpider> CreateMany(string[] types);
    }
}
