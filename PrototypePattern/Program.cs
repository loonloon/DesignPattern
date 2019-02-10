namespace PrototypePattern
{
    class Program
    {
        static void Main(string[] args)
        {
            /*
             * This pattern is used to:
             * avoid subclasses of an object creator in the client application, like the factory method pattern does.
             * avoid the inherent cost of creating a new object in the standard way (e.g., using the 'new' keyword) when it is prohibitively expensive for a given application.
             */
        }
    }

    public abstract class Prototype
    {
        // normal implementation
        public abstract Prototype Clone();
    }

    public class ConcretePrototype1 : Prototype
    {
        public override Prototype Clone()
        {
            // Clones the concrete class.
            return (Prototype)MemberwiseClone();
        }
    }

    public class ConcretePrototype2 : Prototype
    {
        public override Prototype Clone()
        {
            // Clones the concrete class.
            return (Prototype)MemberwiseClone();
        }
    }
}
