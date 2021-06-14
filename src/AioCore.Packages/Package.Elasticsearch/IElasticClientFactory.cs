using Nest;

namespace Package.Elasticsearch
{
    public interface IElasticClientFactory
    {
        IElasticClient CreateElasticClient();
    }
}
