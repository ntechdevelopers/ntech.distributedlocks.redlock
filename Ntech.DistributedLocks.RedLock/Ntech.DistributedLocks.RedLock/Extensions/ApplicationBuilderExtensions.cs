using Microsoft.Extensions.Hosting;
using Ntech.RedLock;

namespace Ntech.DistributedLocks.RedLock.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static void DisposeLockFactory(this IHostApplicationLifetime lifeTime)
        {
            lifeTime.ApplicationStopping.Register(() => 
            { 
                RedLockProvider.RedLockFactoryObject.Dispose();
            });
        }
    }
}
