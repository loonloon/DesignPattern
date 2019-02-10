using System;

namespace NullPattern
{
    class Program
    {
        /*
         * Intent
         * Provide an object as a surrogate for the lack of an object of a given type.
         * The Null Object Pattern provides intelligent do nothing behavior, hiding the details from its collaborators.
         */
        static void Main(string[] args)
        {
            var dog = new Dog();
            dog.MakeSound();

            var unknown = Animal.Null;
            unknown.MakeSound();
        }
    }

    public interface IAnimal
    {
        void MakeSound();
    }

    public abstract class Animal : IAnimal
    {
        public static readonly IAnimal Null = new NullAnimal();
        public abstract void MakeSound();

        private class NullAnimal : Animal
        {
            public override void MakeSound()
            {
                Console.WriteLine("Nothing");
            }
        }
    }

    public class Dog : Animal
    {
        public override void MakeSound()
        {
            Console.WriteLine("Woof!");
        }
    }
}
