using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Ntech.DistributedLocks.ApplicationService;
using Ntech.DistributedLocks.KeyProvider;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ntech.DistributedLocks.RedLock.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IContributionService _contributionService;
        private readonly ICacheService _cacheService;

        public int TimeQuery { get; set; }

        public IndexModel(ILogger<IndexModel> logger, IContributionService contributionService, ICacheService cacheService)
        {
            _logger = logger;
            _contributionService = contributionService;
            _cacheService = cacheService;
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnGetAddContributionsWithDistributedLocks()
        {
            List<Task> addContributionTasks = new List<Task>();
            for (int i = 1; i <= 50; i++)
            {
                addContributionTasks.Add(_contributionService.AddContributionWithDitributedLocks(1));
            }
            await Task.WhenAll(addContributionTasks);
            TimeQuery = await _cacheService.GetAsync<int>(CacheKeyProvider.GetAddContributionKey);
            _logger.LogInformation($"AddContributionsWithDistributedLocks - TimeQuery={TimeQuery}");

            return Page();
        }

        public async Task<IActionResult> OnGetAddContributionsWithoutDistributedLocks()
        {
            List<Task> addContributionTasks = new List<Task>();
            for (int i = 1; i <= 50; i++)
            {
                addContributionTasks.Add(_contributionService.AddContributionWihtoutDitributedLocks(1));
            }
            await Task.WhenAll(addContributionTasks);
            TimeQuery = await _cacheService.GetAsync<int>(CacheKeyProvider.GetAddContributionKey);
            _logger.LogInformation($"AddContributionsWithoutDistributedLocks - TimeQuery={TimeQuery}");

            return Page();
        }
    }
}
