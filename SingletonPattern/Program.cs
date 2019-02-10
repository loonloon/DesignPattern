using System;

namespace SingletonPattern
{
    //http://csharpindepth.com/Articles/General/Singleton.aspx
    class Program
    {
        /*
         * Intent
         * Ensure that only one instance of a class is created.
         * Provide a global point of access to the object.
         */
        static void Main(string[] args)
        {
        }
    }

    /*
     * First version - not thread-safe
     *
     * As hinted at before, the above is not thread-safe.
     * Two different threads could both have evaluated the test if (instance==null) and found it to be true, then both create instances, which violates the singleton pattern.
     *
     * Note that in fact the instance may already have been created before the expression is evaluated,
     * but the memory model doesn't guarantee that the new value of instance will be seen by other threads unless suitable memory barriers have been passed.
     */
    //Bad code! Do not use!
    //public sealed class Singleton
    //{
    //    private static Singleton _instance;
    //    public static Singleton Instance => _instance ?? (_instance = new Singleton());

    //    private Singleton()
    //    {
    //    }
    //}

    /*
     * Second version - simple thread-safety
     *
     * This implementation is thread-safe. The thread takes out a lock on a shared object, and then checks whether or not the instance has been created before creating the instance.
     *
     * This takes care of the memory barrier issue (as locking makes sure that all reads occur logically after the lock acquire, and unlocking makes sure that all writes occur logically before the lock release)
     * and ensures that only one thread will create an instance (as only one thread can be in that part of the code at a time - by the time the second thread enters it,
     * the first thread will have created the instance, so the expression will evaluate to false). Unfortunately, performance suffers as a lock is acquired every time the instance is requested.
     *
     * Note that instead of locking on typeof(Singleton) as some versions of this implementation do, I lock on the value of a static variable which is private to the class.
     * Locking on objects which other classes can access and lock on (such as the type) risks performance issues and even deadlocks.
     *
     * This is a general style preference of mine - wherever possible, only lock on objects specifically created for the purpose of locking,
     * or which document that they are to be locked on for specific purposes (e.g. for waiting/pulsing a queue).
     *
     * Usually such objects should be private to the class they are used in. This helps to make writing thread-safe applications significantly easier.
     */
    //public sealed class Singleton
    //{
    //    private static Singleton _instance;
    //    private static readonly object Locker = new object();
    //    public static Singleton Instance
    //    {
    //        get
    //        {
    //            lock (Locker)
    //            {
    //                return _instance ?? (_instance = new Singleton());
    //            }
    //        }
    //    }

    //    Singleton()
    //    {
    //    }
    //}

    /*
     * Third version - attempted thread-safety using double-check locking
     *
     * This implementation attempts to be thread-safe without the necessity of taking out a lock every time. Unfortunately, there are four downsides to the pattern:
     *
     * It doesn't work in Java. This may seem an odd thing to comment on, but it's worth knowing if you ever need the singleton pattern in Java, and C# programmers may well also be Java programmers.
     * The Java memory model doesn't ensure that the constructor completes before the reference to the new object is assigned to instance.
     * The Java memory model underwent a reworking for version 1.5, but double-check locking is still broken after this without a volatile variable (as in C#).
     *
     * Without any memory barriers, it's broken in the ECMA CLI specification too. It's possible that under the .NET 2.0 memory model (which is stronger than the ECMA spec) it's safe,
     * but I'd rather not rely on those stronger semantics, especially if there's any doubt as to the safety. Making the instance variable volatile can make it work,
     * as would explicit memory barrier calls, although in the latter case even experts can't agree exactly which barriers are required. I tend to try to avoid situations where experts don't agree what's right and what's wrong!
     *
     * It's easy to get wrong. The pattern needs to be pretty much exactly as above - any significant changes are likely to impact either performance or correctness.
     * It still doesn't perform as well as the later implementations.
     */
    // Bad code! Do not use!
    //public sealed class Singleton
    //{
    //    private static Singleton _instance;
    //    private static readonly object Locker = new object();
    //    public static Singleton Instance
    //    {
    //        get
    //        {
    //            if (_instance == null)
    //            {
    //                lock (Locker)
    //                {
    //                    if (_instance == null)
    //                    {
    //                        _instance = new Singleton();
    //                    }
    //                }
    //            }
    //            return _instance;
    //        }
    //    }

    //    Singleton()
    //    {
    //    }
    //}

    /*
     * Fourth version - not quite as lazy, but thread-safe without using locks
     *
     * As you can see, this is really is extremely simple - but why is it thread-safe and how lazy is it?
     *
     * Well, static constructors in C# are specified to execute only when an instance of the class is created or a static member is referenced,
     * and to execute only once per AppDomain. Given that this check for the type being newly constructed needs to be executed whatever else happens,
     * it will be faster than adding extra checking as in the previous examples. There are a couple of wrinkles, however:
     *
     * It's not as lazy as the other implementations. In particular, if you have static members other than Instance,
     * the first reference to those members will involve creating the instance. This is corrected in the next implementation.
     *
     * There are complications if one static constructor invokes another which invokes the first again.
     * Look in the .NET specifications (currently section 9.5.3 of partition II) for more details about the exact nature of type initializers - they're unlikely to bite you,
     * but it's worth being aware of the consequences of static constructors which refer to each other in a cycle.
     *
     * The laziness of type initializers is only guaranteed by .NET when the type isn't marked with a special flag called beforefieldinit.
     * Unfortunately, the C# compiler (as provided in the .NET 1.1 runtime, at least) marks all types which don't have a static constructor (i.e. a block which looks like a constructor but is marked static) as beforefieldinit.
     * I now have an article with more details about this issue. Also note that it affects performance, as discussed near the bottom of the page.
     *
     * One shortcut you can take with this implementation (and only this one) is to just make instance a public static readonly variable,
     * and get rid of the property entirely. This makes the basic skeleton code absolutely tiny! Many people, however, prefer to have a property in case further action is needed in future,
     * and JIT inlining is likely to make the performance identical. (Note that the static constructor itself is still required if you require laziness.)
     */
    //public sealed class Singleton
    //{
    //    public static Singleton Instance { get; } = new Singleton();

    //    // Explicit static constructor to tell C# compiler
    //    // not to mark type as beforefieldinit
    //    static Singleton()
    //    {
    //    }

    //    private Singleton()
    //    {
    //    }
    //}

    /*
     * Fifth version - fully lazy instantiation
     *
     * Here, instantiation is triggered by the first reference to the static member of the nested class, which only occurs in Instance.
     * This means the implementation is fully lazy, but has all the performance benefits of the previous ones.
     * Note that although nested classes have access to the enclosing class's private members, the reverse is not true, hence the need for instance to be internal here.
     * That doesn't raise any other problems, though, as the class itself is private. The code is a bit more complicated in order to make the instantiation lazy, however.
     */
    //public sealed class Singleton
    //{
    //    public static Singleton Instance => Nested.instance;

    //    private Singleton()
    //    {
    //    }

    //    private class Nested
    //    {
    //        internal static readonly Singleton instance = new Singleton();

    //        // Explicit static constructor to tell C# compiler
    //        // not to mark type as beforefieldinit
    //        static Nested()
    //        {
    //        }
    //    }
    //}

    /*
     * Sixth version - using .NET 4's Lazy<T> type
     *
     * If you're using .NET 4 (or higher), you can use the System.Lazy<T> type to make the laziness really simple.
     * All you need to do is pass a delegate to the constructor which calls the Singleton constructor - which is done most easily with a lambda expression.
     *
     * It's simple and performs well. It also allows you to check whether or not the instance has been created yet with the IsValueCreated property, if you need that.
     * The code above implicitly uses LazyThreadSafetyMode.ExecutionAndPublication as the thread safety mode for the Lazy<Singleton>. Depending on your requirements, you may wish to experiment with other modes.
     */
    public sealed class Singleton
    {
        private static readonly Lazy<Singleton> Lazy = new Lazy<Singleton>(() => new Singleton());
        public static Singleton Instance => Lazy.Value;

        private Singleton()
        {
        }
    }
}
