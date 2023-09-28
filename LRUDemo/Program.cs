using Microsoft.Extensions.DependencyInjection;
using LRUCache;
using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        var services = new ServiceCollection();
        services.AddSingleton<ICache<string, int>, LRUCache<string, int>>(_ => new LRUCache<string, int>(5));
        var provider = services.BuildServiceProvider();

        var cache = provider.GetService<ICache<string, int>>();


        // Subscribe to the ItemEvicted event
        cache.ItemEvicted += (key, value) => Console.WriteLine($"Item Evicted - Key: {key}, Value: {value}");

        // Demo
        cache.Add("one", 1);
        cache.Add("two", 2);
        cache.Add("three", 3);
        cache.Add("four", 4);
        cache.Add("five", 5);

        Console.WriteLine(cache.Get("one")); // Should print 1

        cache.Add("six", 6); // This should evict "two" because it is least recently used. ( We just used "one" above)

        try
        {
            Console.WriteLine(cache.Get("two")); // Should throw KeyNotFoundException
        }
        catch (KeyNotFoundException)
        {
            Console.WriteLine("Key 'two' not found in cache.");
        }
    }
}