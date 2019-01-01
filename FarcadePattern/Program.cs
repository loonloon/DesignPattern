using System;

namespace FarcadePattern
{
    class Program
    {
        static void Main(string[] args)
        {
        }
    }

    public class Facade
    {
        private readonly SubsystemA _subsystemA = new SubsystemA();
        private readonly SubsystemB _subsystemB = new SubsystemB();
        private readonly SubsystemC _subsystemC = new SubsystemC();

        public void Operation1()
        {
            Console.WriteLine($"Operation 1\n{_subsystemA.OperationA1()}{_subsystemB.OperationB1()}{_subsystemC.OperationC1()}");
        }

        public void Operation2()
        {
            Console.WriteLine($"Operation 2\n{_subsystemA.OperationA2()}{_subsystemB.OperationB2()}{_subsystemC.OperationC2()}");
        }
    }

    public class SubsystemA
    {
        public string OperationA1()
        {
            return "Subsystem A, Function A1\n";
        }

        public string OperationA2()
        {
            return "Subsystem A, Function A2\n";
        }
    }

    public class SubsystemB
    {
        public string OperationB1()
        {
            return "Subsystem B, Function B1\n";
        }

        public string OperationB2()
        {
            return "Subsystem B, Function B2\n";
        }
    }

    public class SubsystemC
    {
        public string OperationC1()
        {
            return "Subsystem C, Function C1\n";
        }

        public string OperationC2()
        {
            return "Subsystem C, Function C2\n";
        }
    }
}
