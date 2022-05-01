using Ntech.DistributedLocks.KeyProvider;
using System.Threading.Tasks;

namespace Ntech.DistributedLocks.ApplicationService
{
    public class ContributionService : IContributionService
    {
        private readonly ICacheService cacheService;

        public ContributionService(ICacheService cacheService)
        {
            this.cacheService = cacheService;
        }

        public async Task AddContributionWihtoutDitributedLocks(int value)
        {
            await AddContributionToCache(value);
        }

        public async Task AddContributionWithDitributedLocks(int value)
        {
            var result = await cacheService.DoActionWithLockAsync<int>(LockKeyProvider.ContributionLockKey, value, async (arg) => await AddContributionToCache(value));

            if (!result.IsSuccessfullyProcessed)
            {
                var exception = result.Exception;
            }
        }

        private async Task AddContributionToCache(int value)
        {
            var cacheKey = CacheKeyProvider.GetAddContributionKey;

            var currentValue = await cacheService.GetAsync<int>(cacheKey);

            var newValue = currentValue + value;
            await cacheService.SetAsync(cacheKey, newValue);

        }
    }
}
