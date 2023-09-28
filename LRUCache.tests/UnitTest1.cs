namespace LRUCache.tests
{
    public class LRUTests
    {
        [SetUp]
        public void Setup() // we don't really need any set up since we are not using the same conditions for all the tests
        {
        }

        [Test]
        public void Should_Throw_Exception_For_Non_Positive_Capacity()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new LRUCache<string, int>(0));
        }

        [Test]
        public void Should_Initialize_Correctly_For_Positive_Capacity()
        {
            Assert.DoesNotThrow(() => new LRUCache<string, int>(5));
        }
        [Test]
        public void Should_Add_And_Retrieve_Items()
        {
            var cache = new LRUCache<string, int>(5);
            cache.Add("one", 1);
            var value = cache.Get("one");
            Assert.AreEqual(1, value);
        }
        [Test]
        public void Should_Evict_Least_Recently_Used_Item_When_Cache_Is_Full()
        {
            var cache = new LRUCache<string, int>(2);
            cache.Add("one", 1);
            cache.Add("two", 2);
            cache.Add("three", 3); // This should evict "one" as it is the least recently used
            Assert.Throws<KeyNotFoundException>(() => cache.Get("one"));
        }
        [Test]
        public void Should_Clear_All_Items()
        {
            var cache = new LRUCache<string, int>(5);
            cache.Add("one", 1);
            cache.Add("two", 2);
            cache.Clear();
            Assert.Throws<KeyNotFoundException>(() => cache.Get("one"));
            Assert.Throws<KeyNotFoundException>(() => cache.Get("two"));
        }
        [Test]
        public void Should_Remove_Item()
        {
            var cache = new LRUCache<string, int>(5);
            cache.Add("one", 1);
            cache.Remove("one");
            Assert.Throws<KeyNotFoundException>(() => cache.Get("one"));
        }

        [Test]
        public void Should_Throw_Exception_When_Removing_Non_Existent_Item()
        {
            var cache = new LRUCache<string, int>(5);
            Assert.Throws<KeyNotFoundException>(() => cache.Remove("one"));
        }
        [Test]
        public void Should_Return_True_And_Set_Value_When_Key_Exists()
        {
            var cache = new LRUCache<string, int>(5);
            cache.Add("one", 1);
            Assert.IsTrue(cache.TryGetValue("one", out var value));
            Assert.AreEqual(1, value);
        }

        [Test]
        public void TryGetValue_ShouldReturnFalseAndSetValueToDefault_WhenKeyDoesNotExist()
        {
            var cache = new LRUCache<string, int>(5);

            var result = cache.TryGetValue("one", out var value);

            Assert.IsFalse(result);
            Assert.AreEqual(default(int), value);
        }

        [Test]
        public void ItemEvictedEvent_ShouldBeTriggered_WhenItemIsEvicted()
        {

            var cache = new LRUCache<string, int>(2); 
            bool isEventTriggered = false;
            cache.ItemEvicted += (key, value) => isEventTriggered = true; // Subscribe to the event

            cache.Add("one", 1);
            cache.Add("two", 2);
            cache.Add("three", 3); // Adding this should evict "one" and trigger the event

            Assert.IsTrue(isEventTriggered);
        }

        [Test]
        public void ItemEvictedEvent_ShouldHaveCorrectEvictedItem_WhenItemIsEvicted()
        {
            var cache = new LRUCache<string, int>(2);
            string evictedKey = null;
            int evictedValue = 0;
            cache.ItemEvicted += (key, value) =>
            {
                evictedKey = key;
                evictedValue = value;
            }; // Subscribe to the event

            cache.Add("one", 1);
            cache.Add("two", 2);
            cache.Add("three", 3); // should evict "one" and trigger the event

            Assert.AreEqual("one", evictedKey);
            Assert.AreEqual(1, evictedValue);
        }


    }
}