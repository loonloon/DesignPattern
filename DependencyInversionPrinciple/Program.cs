namespace DependencyInversionPrinciple
{
    class Program
    {
        /*
         * Intent
         * High-level modules should not depend on low-level modules. Both should depend on abstractions.
         * Abstractions should not depend on details. Details should depend on abstractions.
         * https://stackoverflow.com/questions/62539/what-is-the-dependency-inversion-principle-and-why-is-it-important
         */
        static void Main(string[] args)
        {
        }
    }

    // Dependency Inversion Principle - Bad example
    //public class Worker
    //{
    //    public void Work()
    //    {
    //        //working...
    //    }
    //}

    //public class SuperWorker
    //{
    //    public void Work()
    //    {
    //        //working...
    //    }
    //}

    //public class Manager
    //{
    //    public Worker Worker { private get; set; }

    //    public void Manage()
    //    {
    //        Worker.Work();
    //    }
    //}

    // Dependency Inversion Principle - Good example
    public interface IWorker
    {
        void Work();
    }

    public class Worker : IWorker
    {
        public void Work()
        {
            // ....working
        }
    }

    public class SuperWorker : IWorker
    {
        public void Work()
        {
            //.... working much more
        }
    }

    public class Manager
    {
        public IWorker Worker { private get; set; }

        public void Manage()
        {
            Worker.Work();
        }
    }
}
