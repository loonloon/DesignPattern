using System;

namespace DecoratorPattern
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Decorator Pattern\n");

            var component = new Component();
            Display("1. Basic component: ", component);
            Display("2. A-decorated : ", new DecoratorA(component));
            Display("3. B-decorated : ", new DecoratorB(component));
            Display("4. B-A-decorated : ", new DecoratorB(new DecoratorA(component)));

            // Explicit DecoratorB
            var b = new DecoratorB(new Component());
            Display("5. A-B-decorated : ", new DecoratorA(b));

            // Invoking its added state and added behavior
            Console.WriteLine($"\t\t\t{b.AddedState}{b.AddedBehavior()}");
        }

        public static void Display(string s, IComponent component)
        {
            Console.WriteLine("{0}{1}", s, component.Operation());
        }
    }

    public interface IComponent
    {
        string Operation();
    }

    public class Component : IComponent
    {
        public string Operation() => "I am walking ";
    }

    public class DecoratorA : IComponent
    {
        private readonly IComponent _component;

        public DecoratorA(IComponent component)
        {
            _component = component;
        }

        public string Operation()
        {
            var s = _component.Operation();
            s += "and listening to Classic FM ";
            return s;
        }
    }

    public class DecoratorB : IComponent
    {
        private readonly IComponent _component;
        public string AddedState = "past the Coffee Shop ";
        public DecoratorB(IComponent component)
        {
            _component = component;
        }

        public string Operation()
        {
            var s = _component.Operation();
            s += "to school ";
            return s;
        }

        public string AddedBehavior()
        {
            return "and I bought a cappuccino";
        }
    }
}
