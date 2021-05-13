using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Nest;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AioCore.BackgroundTasks.Services
{
    public class ElasticsearchSnapshotService : BackgroundService
    {
        private readonly IElasticClient _elasticClient;
        private readonly ILogger<ElasticsearchSnapshotService> _logger;

        public ElasticsearchSnapshotService(
            IElasticClient elasticClient,
            ILogger<ElasticsearchSnapshotService> logger)
        {
            _elasticClient = elasticClient ?? throw new ArgumentNullException(nameof(elasticClient));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                const string repository = "aioc";
                var snapshots = await _elasticClient.Snapshot.GetAsync(repository, $"{repository}-daily", ct: stoppingToken);
                if (!snapshots.IsValid) throw new NotImplementedException();
                var snapshotToDeletes = snapshots.Snapshots.OrderByDescending(t => t.EndTime)
                    .Skip(2).Take(snapshots.Snapshots.Count);

                foreach (var item in snapshotToDeletes)
                {
                    await _elasticClient.Snapshot.DeleteAsync(repository, item.Name, ct: stoppingToken);
                }
                throw new NotImplementedException();
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
            }
        }
    }
}