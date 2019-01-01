using System;

namespace NullPattern
{
    class Program
    {
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
