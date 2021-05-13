using AioCore.BackgroundTasks.Extensions;
using AioCore.BackgroundTasks.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AioCore.BackgroundTasks
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public virtual void ConfigureServices(IServiceCollection services)
        {
            services
                .Configure<BackgroundTaskSettings>(this.Configuration)
                .AddOptions()
                .AddHostedService<ElasticsearchSnapshotService>()
                .AddEventBus(this.Configuration);
        }

        public void Configure()
        {
        }
    }
}