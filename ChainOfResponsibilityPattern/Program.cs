using System;

namespace ChainOfResponsibilityPattern
{
    class Program
    {
        /*
         * Intent:
         * It avoids attaching the sender of a request to its receiver, giving this way other objects the possibility of handling the request too.
         * The objects become parts of a chain and the request is sent from one object to another across the chain until one of the objects will handle it.
         */
        static void Main(string[] args)
        {
            // Build the chain of responsibility
            var logger = new ConsoleLogger(LogLevel.All);
            var logger1 = logger.SetNext(new EmailLogger(LogLevel.FunctionalMessage | LogLevel.FunctionalError));
            var logger2 = logger1.SetNext(new FileLogger(LogLevel.Warning | LogLevel.Error));

            // Handled by ConsoleLogger since the console has a loglevel of all
            logger.Message("Entering function ProcessOrder().", LogLevel.Debug);
            logger.Message("Order record retrieved.", LogLevel.Info);

            // Handled by ConsoleLogger and FileLogger since filelogger implements Warning & Error
            logger.Message("Customer Address details missing in Branch DataBase.", LogLevel.Warning);
            logger.Message("Customer Address details missing in Organization DataBase.", LogLevel.Error);

            // Handled by ConsoleLogger and EmailLogger as it implements functional error
            logger.Message("Unable to Process Order ORD1 Dated D1 For Customer C1.", LogLevel.FunctionalError);

            // Handled by ConsoleLogger and EmailLogger
            logger.Message("Order Dispatched.", LogLevel.FunctionalMessage);
        }
    }

    [Flags]
    public enum LogLevel
    {
        None = 0,                 //        0
        Info = 1,                 //        1
        Debug = 2,                //       10
        Warning = 4,              //      100
        Error = 8,                //     1000
        FunctionalMessage = 16,   //    10000
        FunctionalError = 32,     //   100000
        All = 63                  //   111111
    }

    /// <summary>
    /// Abstract Handler in chain of responsibility pattern.
    /// </summary>
    public abstract class Logger
    {
        protected LogLevel LogMask;
        // The next Handler in the chain
        protected Logger Next;

        protected Logger(LogLevel mask)
        {
            LogMask = mask;
        }

        /// <summary>
        /// Sets the Next logger to make a list/chain of Handlers.
        /// </summary>
        public Logger SetNext(Logger nextLogger)
        {
            Next = nextLogger;
            return nextLogger;
        }

        public void Message(string msg, LogLevel severity)
        {
            //True only if all logMask bits are set in severity
            if ((severity & LogMask) != 0)
            {
                WriteMessage(msg);
            }

            Next?.Message(msg, severity);
        }

        protected abstract void WriteMessage(string msg);
    }

    public class ConsoleLogger : Logger
    {
        public ConsoleLogger(LogLevel mask)
            : base(mask)
        {
        }

        protected override void WriteMessage(string msg)
        {
            Console.WriteLine("Writing to console: " + msg);
        }
    }

    public class EmailLogger : Logger
    {
        public EmailLogger(LogLevel mask)
            : base(mask)
        {
        }

        protected override void WriteMessage(string msg)
        {
            // Placeholder for mail send logic, usually the email configurations are saved in config file.
            Console.WriteLine($"Sending via email: {msg}");
        }
    }

    class FileLogger : Logger
    {
        public FileLogger(LogLevel mask)
            : base(mask)
        {
        }

        protected override void WriteMessage(string msg)
        {
            // Placeholder for File writing logic
            Console.WriteLine($"Writing to Log File: {msg}");
        }
    }
}
