using System;

namespace Ntech.DistributedLocks
{
    public class LockProcessResult
    {
        public LockProcessResult()
        {
        }

        public void SetException(Exception ex)
        {
            this.Exception = ex;
        }

        public bool IsSuccessfullyProcessed => Exception == null;

        public Exception Exception { get; set; }
    }

    public class LockProcessResult<TInput> : LockProcessResult
    {
    }
}
