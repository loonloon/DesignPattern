using System;
using System.Collections.Generic;

namespace BuilderPattern
{
    class Program
    {
        static void Main(string[] args)
        {
            var director = new Director();
            var builderA = new BuilderA();
            var builderB = new BuilderB();

            director.Construct(builderA);
            var productFromBuilderA = builderA.GetResult();
            productFromBuilderA.Display();

            director.Construct(builderB);
            var productFromBuilderB = builderB.GetResult();
            productFromBuilderB.Display();
        }
    }

    public interface IBuilder
    {
        void BuildPartA();
        void BuildPartB();
        Product GetResult();
    }

    public class Product
    {
        private readonly  IList<string> _parts = new List<string>();

        public void Add(string part)
        {
            _parts.Add(part);
        }

        public void Display()
        {
            Console.WriteLine("\nProduct Parts-------");

            foreach (var part in _parts)
            {
                Console.WriteLine(part);
            }

            Console.WriteLine();
        }
    }

    public class BuilderA : IBuilder
    {
        private readonly Product _product = new Product();

        public void BuildPartA()
        {
            _product.Add("BuilderA PartA");
        }

        public void BuildPartB()
        {
            _product.Add("BuilderA PartB");
        }

        public Product GetResult()
        {
            return _product;
        }
    }

    public class BuilderB : IBuilder
    {
        private readonly Product _product = new Product();

        public void BuildPartA()
        {
            _product.Add("BuilderB PartA");
        }

        public void BuildPartB()
        {
            _product.Add("BuilderB PartB");
        }

        public Product GetResult()
        {
            return _product;
        }
    }

    public class Director
    {
        public void Construct(IBuilder builder)
        {
            builder.BuildPartA();
            builder.BuildPartB();
        }
    }
}
