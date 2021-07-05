using Nest;
using System.Collections.Generic;

namespace Package.Elasticsearch.Common
{
    internal class QueryBaseContainer
    {
        public List<QueryContainer> Must { get; set; } = new();

        public List<QueryContainer> Should { get; set; } = new();

        public List<QueryContainer> MustNot { get; set; } = new();
    }
}