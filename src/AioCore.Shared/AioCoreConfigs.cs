using Microsoft.Extensions.Configuration;
using System.IO;

namespace AioCore.Shared
{
    public static class AioCoreConfigs
    {
        public static IConfiguration Configuration(string[] args)
        {
            return new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", false, true)
                .AddEnvironmentVariables()
                .AddCommandLine(args)
                .Build();
        }
    }
}