using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Ntech.RedLock;
using RedLockNet;
using StackExchange.Redis;

namespace Ntech.DistributedLocks.RedLock.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void ConfigureOptions(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<RedisConfiguration>(opt => configuration.GetSection(nameof(RedisConfiguration)).Bind(opt));
        }

        public static void ConfigureDistributedLocks(this IServiceCollection services, IConfiguration configuration)
        {
            var redisOptions = configuration.GetSection(nameof(RedisConfiguration)).Get<RedisConfiguration>();
            var redis = ConnectionMultiplexer.Connect(redisOptions.UrlPort); // Using redisOptions.Connection when included password
            services.AddSingleton(s => redis.GetDatabase());

            // Initialize and register distributed lock factory
            var loggerFactory = LoggerFactory.Create(b => b.SetMinimumLevel(LogLevel.Trace).AddConsole());

            RedLockProvider.SetRedLockFactory(redisOptions, loggerFactory);
            services.AddSingleton(typeof(IDistributedLockFactory), RedLockProvider.RedLockFactoryObject);
        }
    }
}
