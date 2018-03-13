using System;

namespace BridgePattern
{
    class Program
    {
        static void Main(string[] args)
        {
        }
    }

    public interface IBridge
    {
        void Function1();
        void Function2();
    }

    public interface IAbstractBridge
    {
        void CallMethod1();
        void CallMethod2();
    }

    public class AbstractBridge : IAbstractBridge
    {
        private readonly IBridge _bridge;

        public AbstractBridge(IBridge bridge)
        {
            _bridge = bridge;
        }

        public void CallMethod1()
        {
            _bridge.Function1();
        }

        public void CallMethod2()
        {
            _bridge.Function2();
        }
    }

    public class Bridge1 : IBridge
    {
        public void Function1()
        {
            Console.WriteLine("Bridge1.Function1");
        }

        public void Function2()
        {
            Console.WriteLine("Bridge1.Function2");
        }
    }

    public class Bridge2 : IBridge
    {
        public void Function1()
        {
            Console.WriteLine("Bridge2.Function1");
        }

        public void Function2()
        {
            Console.WriteLine("Bridge2.Function2");
        }
    }
}
