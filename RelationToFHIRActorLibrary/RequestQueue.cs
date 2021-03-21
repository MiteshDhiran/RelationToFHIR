using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace RelationToFHIRActorLibrary
{
    /// <summary>
    /// 
    /// </summary>
    internal class RequestQueue
    {

        private readonly ConcurrentQueue<RelationalToBlobSyncRequest> _queue =
            new ConcurrentQueue<RelationalToBlobSyncRequest>();

        private readonly ConcurrentDictionary<string,RelationalToBlobSyncRequest> _allItems =
            new ConcurrentDictionary<string,RelationalToBlobSyncRequest>();

        private readonly ConcurrentDictionary<string,RelationalToBlobSyncRequest> _inProgressQueue =
            new ConcurrentDictionary<string,RelationalToBlobSyncRequest>();

        private int _maxConcurrentItemsToProcess;
        public RequestQueue(int maxConcurrentItemsToProcess)
        {
            _maxConcurrentItemsToProcess = maxConcurrentItemsToProcess;
        }

        internal void AddRequestToQueue(RelationalToBlobSyncRequest request)
        {
            if (_allItems.ContainsKey(request.RequestKey) == false)
            {
                _allItems.TryAdd(request.RequestKey, request);
                _queue.Enqueue(request);
            }
        }

        internal RelationalToBlobSyncRequest GetRequestToProcess()
        {
            if (_queue.IsEmpty == false)
            {
                var retVal = _queue.TryDequeue(out var res) ? res : null;
                if (retVal != null)
                {
                    _inProgressQueue.TryAdd(retVal.RequestKey, retVal);
                }

                return retVal;
            }
            else
            {
                return null;
            }
        }

        internal bool MarkRequestCompleted(RelationalToBlobSyncRequest request)
        {
            return _inProgressQueue.TryRemove(request.RequestKey, out var _);
        }

    }
}
