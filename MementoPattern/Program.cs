namespace MementoPattern
{
    class Program
    {
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
