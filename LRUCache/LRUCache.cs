namespace LRUCache
{
    using System;
    using System.Collections.Generic;

    public class LRUCache<TKey, TValue> : ICache<TKey, TValue>
    {
        private readonly int _capacity;
        private readonly Dictionary<TKey, LinkedListNode<CacheItem>> _cacheMap = new();
        private readonly LinkedList<CacheItem> _lruList = new();

        public LRUCache(int capacity)
        {
            if (capacity <= 0) throw new ArgumentOutOfRangeException(nameof(capacity));
            _capacity = capacity;
        }

        public event Action<TKey, TValue> ItemEvicted;
        public void Add(TKey key, TValue value)
        {
            lock (_cacheMap)
            {
                if (_cacheMap.Count >= _capacity)
                {
                    var lastNode = _lruList.Last;
                    _cacheMap.Remove(lastNode.Value.Key);
                    _lruList.RemoveLast();

                    // Trigger Event when evicted
                    ItemEvicted?.Invoke(lastNode.Value.Key, lastNode.Value.Value);
                }

                var cacheItem = new CacheItem(key, value);
                var node = _lruList.AddFirst(cacheItem);
                _cacheMap.Add(key, node);
            }
        }


        public TValue Get(TKey key)
        {
            lock (_cacheMap)
            {
                if (_cacheMap.TryGetValue(key, out var node))
                {
                    _lruList.Remove(node);
                    _lruList.AddFirst(node);
                    return node.Value.Value;
                }
                else
                {
                    throw new KeyNotFoundException($"Key {key} not found in cache.");
                }
            }
        }


        public bool TryGetValue(TKey key, out TValue value)
        {
            lock (_cacheMap)
            {
                if (_cacheMap.TryGetValue(key, out var node))
                {
                    _lruList.Remove(node);
                    _lruList.AddFirst(node);
                    value = node.Value.Value;
                    return true;
                }
                else
                {
                    value = default;
                    return false;
                }
            }
        }


        public void Remove(TKey key)
        {
            lock (_cacheMap)
            {
                if (_cacheMap.TryGetValue(key, out var node))
                {
                    _lruList.Remove(node);
                    _cacheMap.Remove(key);
                }
                else
                {
                    throw new KeyNotFoundException($"Key {key} not found in cache.");
                }
            }
        }


        public void Clear()
        {
            lock (_cacheMap)
            {
                _lruList.Clear();
                _cacheMap.Clear();
            }
        }


        private class CacheItem
        {
            public TKey Key { get; }
            public TValue Value { get; }

            public CacheItem(TKey key, TValue value)
            {
                Key = key;
                Value = value;
            }
        }
    }
}
