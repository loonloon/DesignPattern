namespace MementoPattern
{
    class Program
    {
        /*
         * Intent
         * The intent of this pattern is to capture the internal state of an object without violating encapsulation and
         * thus providing a mean for restoring the object into initial state when needed.
         */
        static void Main(string[] args)
        {
        }
    }

    public class OriginalObject
    {
        private readonly Memento _myMemento;
        public string Str1 { get; set; }
        public string Str2 { get; set; }

        public OriginalObject(string str1, string str2)
        {
            Str1 = str1;
            Str2 = str2;
            _myMemento = new Memento(str1, str2);
        }

        public void Revert()
        {
            Str1 = _myMemento.Str1;
            Str2 = _myMemento.Str2;
        }
    }

    public class Memento
    {
        public readonly string Str1;
        public readonly string Str2;

        public Memento(string str1, string str2)
        {
            Str1 = str1;
            Str2 = str2;
        }
    }
}
