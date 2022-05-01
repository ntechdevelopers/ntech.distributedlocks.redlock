using System;
using System.Threading.Tasks;

namespace Ntech.DistributedLocks.ApplicationService
{
    public interface ICacheService
    {
        Task<LockProcessResult> DoActionWithLockAsync(string lockKey, Func<Task> processor);
        Task<LockProcessResult<TInput>> DoActionWithLockAsync<TInput>(string lockKey, TInput parameter, Func<TInput, Task> processor);
        Task<TEntity> GetAsync<TEntity>(string key);
        Task SetAsync<TEntity>(string key, TEntity value);
    }
}
