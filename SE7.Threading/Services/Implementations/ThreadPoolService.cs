using SE7.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SE7.Threading.Services.Implementations
{
    internal sealed class ThreadPoolService : IDisposable
    {
        private readonly Dictionary<Guid, ThreadPool> ThreadPools = [];
        private readonly EventsService EventsService;

        public ThreadPoolService(EventsService eventsService)
        {
            EventsService = eventsService;
        }

        public ThreadPool CreateThreadPool()
        {
            var threadPool = new ThreadPool(EventsService);

            ThreadPools.Add(threadPool.Id, threadPool);

            return threadPool;
        }

        public bool TryGetThreadPool(Guid threadPoolId, out ThreadPool? threadPool) =>
            ThreadPools.TryGetValue(threadPoolId, out threadPool);

        public bool TryMergeThreadPools(out Guid? newThreadPoolId, params Guid[] threadPoolIds)
        {
            return TryMergeThreadPools(out newThreadPoolId, threadPoolIds.AsEnumerable());
        }

        public bool TryMergeThreadPools(out Guid? newThreadPoolId, IEnumerable<Guid> threadPoolIds)
        {
            newThreadPoolId = null;

            int GetThreadPoolSize(Guid threadPoolId) =>
                ThreadPools.TryGetValue(threadPoolId, out var threadPool) ? threadPool.Size :
                throw new Exception($"Thread Pool with Id {threadPoolId} does not exist in this Thread Pool Service.");

            if (threadPoolIds.Sum(GetThreadPoolSize) > ThreadPool.MaxThreads)
            {
                return false;
            }

            var threadPoolsToMerge = new List<ThreadPool>();

            foreach (var threadPoolId in threadPoolIds)
            {
                if (!ThreadPools.TryGetValue(threadPoolId, out var threadPool))
                {
                    return false;
                }

                threadPoolsToMerge.Add(threadPool);
            }

            foreach (var threadPoolToMerge in threadPoolsToMerge)
            {
                ThreadPools.Remove(threadPoolToMerge.Id);

                threadPoolToMerge.Dispose();
            }

            newThreadPoolId = Guid.NewGuid();

            ThreadPools.Add(
                newThreadPoolId.Value,
                new ThreadPool(EventsService, threadPoolsToMerge.SelectMany(tp => tp.Threads).ToDictionary())
            );

            return threadPoolsToMerge.Count != 0;
        }

        public void Dispose()
        {
            foreach (var threadPool in ThreadPools)
            {
                threadPool.Value.Dispose();
            }
        }
    }
}
