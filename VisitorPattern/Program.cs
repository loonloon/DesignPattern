using System;
using System.Collections.Generic;

namespace VisitorPattern
{
    class Program
    {
        static void Main(string[] args)
        {
            /*
             * What problems can the Visitor design pattern solve?
             * It should be possible to define a new operation for (some) classes of an object structure without changing the classes.
             *
             * When new operations are needed frequently and the object structure consists of many unrelated classes,
             * it's inflexible to add new subclasses each time a new operation is required because
             * "[..] distributing all these operations across the various node classes leads to a system that's hard to understand, maintain, and change."
             */

            //example 1
            var files = new List<File> { new ExtractedFile(), new SplitFile(), new ExtractedFile() };
            var dispatcher = new Dispatcher(); ;

            foreach (var file in files)
            {
                file.Accept(dispatcher);
            }

            //example 2
            /*
             * It shows how the contents of a tree of nodes (in this case describing the components of a car) can be printed.
             *
             * Instead of creating print methods for each node subclass (Wheel, Engine, Body, and Car),
             * one visitor class (CarElementPrintVisitor) performs the required printing action.
             *
             * Because different node subclasses require slightly different actions to print properly,
             * CarElementPrintVisitor dispatches actions based on the class of the argument passed to its visit method. CarElementDoVisitor,
             * which is analogous to a save operation for a different file format, does likewise.
             */
            var car = new Car();
            car.Accept(new CarElementPrintVisitor());
            car.Accept(new CarElementDoVisitor());
        }
    }

    public abstract class File
    {   // Parent class for the elements (ArchivedFile, SplitFile and ExtractedFile)

        // This function accepts an object of any class derived from AbstractDispatcher and must be implemented in all derived classes
        public abstract void Accept(AbstractDispatcher dispatcher);
    }

    public abstract class AbstractDispatcher
    {
        // Declare overloads for each kind of a file to dispatch
        public abstract void Dispatch(ArchivedFile file);
        public abstract void Dispatch(SplitFile file);
        public abstract void Dispatch(ExtractedFile file);
    }

    // Implements dispatching of all kind of elements (files)
    public class Dispatcher : AbstractDispatcher
    {
        public override void Dispatch(ArchivedFile file)
        {
            Console.WriteLine("dispatching ArchivedFile");
        }

        public override void Dispatch(SplitFile file)
        {
            Console.WriteLine("dispatching SplitFile");
        }

        public override void Dispatch(ExtractedFile file)
        {
            Console.WriteLine("dispatching ExtractedFile");
        }
    }

    public class ArchivedFile : File
    {
        public override void Accept(AbstractDispatcher dispatcher)
        {
            dispatcher.Dispatch(this);
        }
    }

    public class SplitFile : File
    {
        public override void Accept(AbstractDispatcher dispatcher)
        {
            dispatcher.Dispatch(this);
        }
    }

    public class ExtractedFile : File
    {
        public override void Accept(AbstractDispatcher dispatcher)
        {
            dispatcher.Dispatch(this);
        }
    }

    public interface ICarElement
    {
        void Accept(ICarElementVisitor visitor);
    }

    public interface ICarElementVisitor
    {
        void Visit(Body body);
        void Visit(Car car);
        void Visit(Engine engine);
        void Visit(Wheel wheel);
    }

    public class Wheel : ICarElement
    {
        public string Name { get; }

        public Wheel(string name)
        {
            Name = name;
        }

        public void Accept(ICarElementVisitor visitor)
        {
            /*
             * accept(CarElementVisitor) in Wheel implements
             * accept(CarElementVisitor) in CarElement, so the call
             * to accept is bound at run time. This can be considered
             * the *first* dispatch. However, the decision to call
             * visit(Wheel) (as opposed to visit(Engine) etc.) can be
             * made during compile time since 'this' is known at compile
             * time to be a Wheel. Moreover, each implementation of
             * CarElementVisitor implements the visit(Wheel), which is
             * another decision that is made at run time. This can be
             * considered the *second* dispatch.
             */
            visitor.Visit(this);
        }
    }

    public class Body : ICarElement
    {
        public void Accept(ICarElementVisitor visitor)
        {
            visitor.Visit(this);
        }
    }

    public class Engine : ICarElement
    {
        public void Accept(ICarElementVisitor visitor)
        {
            visitor.Visit(this);
        }
    }

    public class Car : ICarElement
    {
        private readonly List<ICarElement> _elements;

        public Car()
        {
            _elements = new List<ICarElement>
            {
                new Wheel("front left"), new Wheel("front right"),
                new Wheel("back left"), new Wheel("back right"),
                new Body(), new Engine()
            };
        }

        public void Accept(ICarElementVisitor visitor)
        {
            foreach (var element in _elements)
            {
                element.Accept(visitor);
            }

            visitor.Visit(this);
        }
    }

    public class CarElementDoVisitor : ICarElementVisitor
    {
        public void Visit(Body body)
        {
            Console.WriteLine("Moving my body");
        }

        public void Visit(Car car)
        {
            Console.WriteLine("Starting my car");
        }

        public void Visit(Wheel wheel)
        {
            Console.WriteLine($"Kicking my {wheel.Name} wheel");
        }

        public void Visit(Engine engine)
        {
            Console.WriteLine("Starting my engine");
        }
    }

    public class CarElementPrintVisitor : ICarElementVisitor
    {
        public void Visit(Body body)
        {
            Console.WriteLine("Visiting body");
        }

        public void Visit(Car car)
        {
            Console.WriteLine("Visiting car");
        }

        public void Visit(Engine engine)
        {
            Console.WriteLine("Visiting engine");
        }

        public void Visit(Wheel wheel)
        {
            Console.WriteLine($"Visiting {wheel.Name} wheel");
        }
    }
}
