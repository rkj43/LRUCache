 # LRUCache Library Solution

## Overview
This solution provides a generic, in-memory cache component, designed to be used by developers in their applications. It is capable of storing arbitrary types of objects, which are added and retrieved using a unique key, similar to a dictionary. The cache has a configurable threshold for the maximum number of items it can hold at any one time.

## Projects
1. **LRUCache**: This is the main project where the LRUCache is implemented. It contains the logic for adding, retrieving, and removing items based on the LRU policy.
2. **LRUCache.Tests**: This project contains all the NUnit test cases for the LRUCache.
3. **LRUCache.Demo**: A simple project demonstrating the usage of the LRUCache with Dependency Injection.

## LRU Cache (Least Recently Used)
The cache follows the Least Recently Used (LRU) policy, discarding the least recently used items first. This algorithm requires keeping track of what was used when, which is expensive if one wants to make sure the algorithm always discards the least recently used item. For more details, refer to the [Wikipedia Definition](https://en.wikipedia.org/wiki/Cache_replacement_policies#Least_recently_used_(LRU)).

This approach is similar to solving the LRU cache problem on [LeetCode](https://leetcode.com/problems/lru-cache/description/), and it has been modified to make it a generic and reusable library. For a better understanding of the LRU Cache, refer to [Neetcode's Video](https://youtu.be/7ABFKPK2hD4?si=Ro1AhtNsjZOtDMG5).

## Features
- **Configurable Capacity**: The cache's capacity is configurable at runtime by the consumer.
- **Thread Safety**: All methods are thread-safe.
- **Item Eviction Event**: An event is triggered when an item is evicted, allowing the consumer to know when items get evicted.
- **Dependency Injection**: Demonstrated in the LRUCache.Demo project, showing how to use the cache as a singleton using dependency injection.

### Dependency Injection
Dependency injection can be done in the consumer class, and it is demonstrated in the LRUCache.Demo project.
### Event for Item Eviction
To subscribe to the item eviction event, refer to the LRUCache project and the corresponding test cases in the LRUCache.Tests project.

## Instructions for Using LRUCache Library

### Step 1: Reference the Library
To use the LRUCache library, first, add it as a dependency to your project.

### Step 2: Initialize the Cache
Initialize the LRUCache with the desired capacity.:
```csharp
ICache<string, int> cache = new LRUCache<string, int>(5); // Initializes a cache with a capacity of 5.
```
### Step 3: Dependency Injection(optional)
Register the LRUCache as a singleton in your service collection
```csharp
services.AddSingleton<ICache<string, int>, LRUCache<string, int>>(_ => new LRUCache<string, int>(5));
```
### Step 4: Use the cache
- Adding Items to Cache
```csharp
cache.Add("one", 1);
```
- Getting Items from Cache
```csharp
int value = cache.Get("one"); //  1
```
- Removing Items from Cache
```csharp
cache.Remove("one");
```
### Step 5: Subscribing to Event
```csharp
cache.ItemEvicted += (key, value) => Console.WriteLine($"Item Evicted - Key: {key}, Value: {value}");

```

## Testing
The LRUCache.Tests project contains NUnit tests covering basic use cases and scenarios, including adding, retrieving, removing items, and item eviction events.

## Conclusion
This LRUCache Library  is a versatile and reusable component, adhering to SOLID principles and allowing developers to efficiently manage in-memory storage with the flexibility of configuration and the assurance of thread safety.
## Time Spent
I spent around 2-3 hours working on this library. A good chunk of that time went into looking up the best ways to do things to make sure I was on the right track and that the library would be solid and efficient.

Besides the coding part, I also spent some time brushing up on and getting to grips with some key concepts like Dependency Injection, Unit Testing, and how to handle Events. Getting a deeper understanding of these things was really important to make the library strong and easy to maintain, and to make sure everything was up to scratch in terms of quality and maintainability.

## Resources I found useful while working on this
- https://www.albahari.com/threading/
- https://stackoverflow.com/questions/1735071/is-it-ok-to-lock-on-system-collections-generic-listt
- [neetcode.io](https://neetcode.io/)
- [Writing NUnit Tests](https://www.youtube.com/watch?v=HYrXogLj7vg&t=2064s)
- https://www.webtrainingroom.com/csharp/unit-testing
- [Dependency Injection](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/dependency-injection?view=aspnetcore-7.0)
- [Using DI container for singleton](https://stackoverflow.com/questions/53825155/how-can-i-use-microsoft-extensions-dependencyinjection-in-an-net-core-console-a)
- [Really Good video for Dependency Injection](https://www.youtube.com/watch?v=Hhpq7oYcpGE&t=138s)
