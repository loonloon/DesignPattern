using System;

namespace ProxyPattern
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Proxy Pattern\n");

            ISubject proxy = new Proxy();
            Console.WriteLine(proxy.Request());
            Console.WriteLine(proxy.Request());

            var protectionProxy = new ProtectionProxy();
            Console.WriteLine(protectionProxy.Request());

            Console.WriteLine((proxy as ProtectionProxy).Authenticate("Secret"));
            Console.WriteLine((proxy as ProtectionProxy).Authenticate("Abracadabra"));
            Console.WriteLine(proxy.Request());
        }
    }

    public interface ISubject
    {
        string Request();
    }

    public class Subject
    {
        public string Request()
        {
            return "Subject Request Choose left door\n";
        }
    }

    public class Proxy : ISubject
    {
        private Subject _subject;

        public string Request()
        {
            if (_subject == null)
            {
                Console.WriteLine("Subject inactive");
                _subject = new Subject();
            }

            Console.WriteLine("Subject active");
            return $"Proxy: Call to {_subject.Request()}";
        }
    }

    public class ProtectionProxy : ISubject
    {
        private Subject _subject;
        private readonly string password = "Abracadabra";

        public string Authenticate(string supplied)
        {
            if (supplied != password)
            {
                return "Protection Proxy: No access";
            }

            _subject = new Subject();
            return "Protection Proxy: Authenticated";
        }

        public string Request()
        {
            if (_subject == null)
            {
                return "Protection Proxy: Authenticate first";
            }

            return "Protection Proxy: Call to " + _subject.Request();
        }
    }
}
