using System;

namespace MediatorPattern
{
    class Program
    {
        static void Main(string[] args)
        {
        }
    }

    public delegate void MessageReceivedEventHandler(string meesage, string from);

    public class Mediator
    {
        public event MessageReceivedEventHandler MessageReceived;

        public void Send(string message, string from)
        {
            if (MessageReceived != null)
            {
                Console.WriteLine($"Sending '{message}' from {from}");
                MessageReceived(message, from);
            }
        }
    }

    public class Person
    {
        private readonly Mediator _mediator;
        public string Name { get; set; }

        public Person(Mediator mediator, string name)
        {
            _mediator = mediator;
            _mediator.MessageReceived += Receive;
            Name = name;
        }

        private void Receive(string message, string from)
        {
            if (!string.IsNullOrEmpty(from) && !from.Equals(Name, StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine($"{Name} received '{message}' from {from}");
            }
        }

        public void Send(string message)
        {
            _mediator.Send(message, Name);
        }
    }
}
