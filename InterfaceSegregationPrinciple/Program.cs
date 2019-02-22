namespace InterfaceSegregationPrinciple
{
    class Program
    {
        /*
         * Intent
         * Clients should not be forced to depend upon interfaces that they don't use.
         */
        static void Main(string[] args)
        {
        }
    }

    // interface segregation principle - good example
    public interface IWorker
    {
        void Work();
    }

    public interface IFeedable
    {
        void Eat();
    }

    public class Worker : IWorker, IFeedable
    {
        public void Work()
        {
            //working...
        }

        public void Eat()
        {
            //eating in lunch break...
        }
    }

    public class SuperWorker : IWorker, IFeedable
    {
        public void Work()
        {
            //working much more...
        }

        public void Eat()
        {
            //eating in lunch break...
        }
    }

    public class Robot : IWorker
    {
        public void Work()
        {
            //working...
        }
    }

    public class Manager
    {
        public IWorker Worker { get; }

        public Manager(IWorker worker)
        {
            Worker = worker;
        }

        public void Manage()
        {
            Worker.Work();
        }
    }

    // interface segregation principle - bad example
    //public interface IWorker
    //{
    //    void Work();
    //    void Eat();
    //}

    //public class Worker : IWorker
    //{
    //    public void Work()
    //    {
    //        //working...
    //    }

    //    public void Eat()
    //    {
    //        //eating in lunch break...
    //    }
    //}

    //public class SuperWorker : IWorker
    //{
    //    public void Work()
    //    {
    //        //working much more...
    //    }

    //    public void Eat()
    //    {
    //        //eating in lunch break...
    //    }
    //}

    //public class Manager
    //{
    //    public IWorker Worker { get; }

    //    public Manager(IWorker worker)
    //    {
    //        Worker = worker;
    //    }

    //    public void Manage()
    //    {
    //        Worker.Work();
    //    }
    //}
}
