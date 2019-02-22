namespace SingleResponsibilityPrinciple
{
    class Program
    {
        /*
         * Intent
         * A class should have only one reason to change.
         */
        static void Main(string[] args)
        {
        }
    }

    // single responsibility principle - good example
    //public interface IEmail
    //{
    //    void SetSender(string sender);
    //    void SetReceiver(string receiver);
    //    void SetContent(IContent content);
    //}

    //public interface IContent
    //{
    //    string GetContent();
    //}

    //public class Email : IEmail
    //{
    //    public void SetSender(string sender)
    //    {
    //    }

    //    public void SetReceiver(string receiver)
    //    {
    //    }

    //    public void SetContent(IContent content)
    //    {
    //    }
    //}

    // single responsibility principle - bad example
    //Adding a new content type (like html).
    //public interface IEmail
    //{
    //    void SetSender(string sender);
    //    void SetReceiver(string receiver);
    //    void SetContent(string content);
    //}

    //public class Email : IEmail
    //{
    //    public void SetSender(string sender)
    //    {
    //    }

    //    public void SetReceiver(string receiver)
    //    {
    //    }

    //    public void SetContent(string content)
    //    {
    //    }
    //}
}
