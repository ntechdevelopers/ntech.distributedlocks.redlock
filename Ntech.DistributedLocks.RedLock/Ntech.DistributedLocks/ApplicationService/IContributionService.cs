using System.Threading.Tasks;

namespace Ntech.DistributedLocks.ApplicationService
{
    public interface IContributionService
    {
        Task AddContributionWihtoutDitributedLocks(int value);
        Task AddContributionWithDitributedLocks(int value);
    }
}
