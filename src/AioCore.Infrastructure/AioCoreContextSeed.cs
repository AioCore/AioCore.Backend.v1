using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Polly;
using Polly.Retry;
using System;
using System.Threading.Tasks;

namespace AioCore.Infrastructure
{
    public class AioCoreContextSeed
    {
        public async Task SeedAsync(AioCoreContext context, ILogger<AioCoreContextSeed> logger)
        {
            var policy = CreatePolicy(logger, nameof(AioCoreContextSeed));

            await policy.ExecuteAsync(async () =>
            {
                await using (context)
                {
                    await context.Database.MigrateAsync();

                    // migrate here

                    await context.SaveChangesAsync();
                }
            });
        }

        private static AsyncRetryPolicy CreatePolicy(ILogger logger, string prefix, int retries = 3)
        {
            return Policy.Handle<SqlException>().
                WaitAndRetryAsync(
                    retries,
                    retry => TimeSpan.FromSeconds(5),
                    (exception, timeSpan, retry, ctx) =>
                    {
                        logger.LogWarning(exception, "[{prefix}] Exception {ExceptionType} with message {Message} detected on attempt {retry} of {retries}", prefix, exception.GetType().Name, exception.Message, retry, retries);
                    }
                );
        }
    }
}