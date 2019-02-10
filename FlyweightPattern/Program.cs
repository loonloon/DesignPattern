using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;

namespace FlyweightPattern
{
    class Program
    {
        /*
         * Intent
         * The intent of this pattern is to use sharing to support a large number of objects
         * that have part of their internal state in common where the other part of state can vary.
         */
        static void Main(string[] args)
        {
        }
    }

    /*
     * To enable safe sharing, between clients and threads, Flyweight objects must be immutable.Flyweight objects are by definition value objects.
     * The identity of the object instance is of no consequence therefore two Flyweight instances of the same value are considered equal
     */
    public class CoffeeFlavour
    {
        public string Flavour { get; }

        public CoffeeFlavour(string flavour)
        {
            Flavour = flavour;
        }

        public override bool Equals(object obj)
        {
            return !ReferenceEquals(null, obj) && (obj is CoffeeFlavour a && Equals(a));
        }

        public bool Equals(CoffeeFlavour other)
        {
            return string.Equals(Flavour, other.Flavour);
        }

        public override int GetHashCode()
        {
            return Flavour.GetHashCode();
        }

        public static bool operator ==(CoffeeFlavour a, CoffeeFlavour b)
        {
            return Equals(a, b);
        }

        public static bool operator !=(CoffeeFlavour a, CoffeeFlavour b)
        {
            return !Equals(a, b);
        }
    }

    public interface ICoffeeFlavourFactory
    {
        CoffeeFlavour GetFlavour(string flavour);
    }

    public class ReducedMemoryFootprint : ICoffeeFlavourFactory
    {
        private readonly object _cacheLock = new object();
        private readonly IDictionary<string, CoffeeFlavour> _cache = new Dictionary<string, CoffeeFlavour>();

        public CoffeeFlavour GetFlavour(string flavour)
        {
            if (_cache.ContainsKey(flavour))
            {
                return _cache[flavour];
            }

            var coffeeFlavour = new CoffeeFlavour(flavour);
            ThreadPool.QueueUserWorkItem(AddFlavourToCache, coffeeFlavour);
            return coffeeFlavour;
        }

        private void AddFlavourToCache(object state)
        {
            var coffeeFlavour = (CoffeeFlavour)state;

            if (!_cache.ContainsKey(coffeeFlavour.Flavour))
            {
                lock (_cacheLock)
                {
                    if (!_cache.ContainsKey(coffeeFlavour.Flavour))
                    {
                        _cache.Add(coffeeFlavour.Flavour, coffeeFlavour);
                    }
                }
            }
        }

        public class MinimumMemoryFootprint : ICoffeeFlavourFactory
        {
            private readonly ConcurrentDictionary<string, CoffeeFlavour> _cache =
                new ConcurrentDictionary<string, CoffeeFlavour>();

            public CoffeeFlavour GetFlavour(string flavour)
            {
                return _cache.GetOrAdd(flavour, flv => new CoffeeFlavour(flv));
            }
        }
    }
}
