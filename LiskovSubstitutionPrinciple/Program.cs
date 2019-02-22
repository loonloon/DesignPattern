using System;

namespace LiskovSubstitutionPrinciple
{
    class Program
    {
        /*
         * Intent
         * if a program module is using a Base class,
         * then the reference to the Base class can be replaced with a Derived class without affecting the functionality of the program module.
         *
         * If an override method does nothing or just throws an exception, then you’re probably violating the LSP.
         */
        static void Main(string[] args)
        {
        }
    }

    //good
    public class Bird
    {
    }

    public class FlyingBird : Bird
    {
        public virtual void Fly()
        {
            Console.WriteLine("Fly");
        }
    }

    public class Duck : FlyingBird
    {
    }

    public class Penguin : Bird
    {
    }

    //bad
    //public class Bird
    //{
    //    public virtual void Fly()
    //    {
    //        Console.WriteLine("Fly");
    //    }
    //}

    //public class Duck : Bird
    //{
    //}

    ////Penguin is a bird, But it can't fly,
    ////Penguin class is a subtype of class Bird, But it can't use the fly method, that means that we are breaking LSP principle.
    //public class Penguin : Bird
    //{
    //    public override void Fly()
    //    {
    //        Console.WriteLine("Cannot Fly");
    //    }
    //}
}
