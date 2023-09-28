using Microsoft.Extensions.DependencyInjection;
using LRUCache;
using System;

class Program
{
    static void Main()
    {
        // Dependency Injection for Singleton Use
        var serviceProvider = new ServiceCollection()
            .AddSingleton<ICache<string, int>>(provider => new LRUCache<string, int>(5)) // Singleton with capacity 5
            .BuildServiceProvider();

        var cache = serviceProvider.GetRequiredService<ICache<string, int>>();

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