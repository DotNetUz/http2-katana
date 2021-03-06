﻿// Copyright © Microsoft Open Technologies, Inc.
// All Rights Reserved       
// Licensed under the Apache License, Version 2.0 (the "License"); you may not use this file except in compliance with the License. You may obtain a copy of the License at
// http://www.apache.org/licenses/LICENSE-2.0

// THIS CODE IS PROVIDED ON AN *AS IS* BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING WITHOUT LIMITATION ANY IMPLIED WARRANTIES OR CONDITIONS OF TITLE, FITNESS FOR A PARTICULAR PURPOSE, MERCHANTABLITY OR NON-INFRINGEMENT.

// See the Apache 2 License for the specific language governing permissions and limitations under the License.
using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.Http2.Protocol.IO
{
    internal class PriorityQueue : IQueue
    {
        private List<IPriorityItem> _storage;
        private readonly object _lock = new object();

        public int Count { get { return _storage.Count; } }

        public bool IsDataAvailable 
        { 
            get { return Count != 0; }
        }

        public PriorityQueue()
        {
            _storage = new List<IPriorityItem>();
        }

        public PriorityQueue(IEnumerable<IPriorityItem> initialCollection)
            : this()
        {
            EnqueueRange(initialCollection);
        }

        public void Enqueue(IQueueItem item)
        {
            lock (_lock)
            {
                if (!(item is IPriorityItem))
                {
                    throw new ArgumentException("Cant enqueue item into priority queue. Argument should be IPriorityItem");
                }
                var priItem = item as IPriorityItem;
                _storage.Add(priItem);
                _storage = _storage.OrderByDescending(priorityItem => priorityItem.Priority).ToList();
            }
        }

        public void EnqueueRange(IEnumerable<IPriorityItem> items)
        {
            foreach (var item in items)
            {
                Enqueue(item);
            }
        }

        public IQueueItem Dequeue()
        {
            lock (_lock)
            {
                if (_storage.Count == 0)
                {
                    return null;
                }
                var result = _storage[0];
                _storage.RemoveAt(0);
                _storage = _storage.OrderByDescending(priorityItem => priorityItem.Priority).ToList();

                return result;
            }
        }

        public IQueueItem Peek()
        {
            return _storage.FirstOrDefault();
        }

        public IQueueItem First()
        {
            return _storage.FirstOrDefault();
        }

        public IQueueItem Last()
        {
            return _storage.LastOrDefault();
        }
    }
}
