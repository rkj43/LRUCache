namespace LRUCache;

public interface ICache<TKey, TValue>
{
    void Add(TKey key, TValue value);
    TValue Get(TKey key);
    bool TryGetValue(TKey key, out TValue value);
    void Remove(TKey key);
    void Clear();
    event Action<TKey, TValue> ItemEvicted;
}