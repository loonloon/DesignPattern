using System;

namespace CommandPattern
{
    class Program
    {
        /*
         * Intent
         * encapsulate a request in an object
         * allows the parameterization of clients with different requests
         * allows saving the requests in a queue
         */
        static void Main(string[] args)
        {
            var argument = args.Length > 0 ? args[0].ToUpper() : null;
            ISwitchable lamp = new Light();

            ICommand switchClose = new CloseSwitchCommand(lamp);
            ICommand switchOpen = new OpenSwitchCommand(lamp);
            var @switch = new Switch(switchClose, switchOpen);

            switch (argument)
            {
                case "ON":
                    @switch.Close();
                    break;
                case "OFF":
                    @switch.Open();
                    break;
                default:
                    Console.WriteLine("Argument \"ON\" or \"OFF\" is required.");
                    break;
            }
        }
    }

    public interface ICommand
    {
        void Execute();
    }

    public class Switch
    {
        private readonly ICommand _openedCommand;
        private readonly ICommand _closedCommand;

        public Switch(ICommand openedCommand, ICommand closedCommand)
        {
            _openedCommand = openedCommand;
            _closedCommand = closedCommand;
        }

        public void Open()
        {
            _openedCommand.Execute();
        }

        public void Close()
        {
            _closedCommand.Execute();
        }
    }

    public interface ISwitchable
    {
        void PowerOn();
        void PowerOff();
    }

    public class Light : ISwitchable
    {
        public void PowerOn()
        {
            Console.WriteLine("The light in on");
        }

        public void PowerOff()
        {
            Console.WriteLine("The light if off");
        }
    }

    public class CloseSwitchCommand : ICommand
    {
        private readonly ISwitchable _switchable;

        public CloseSwitchCommand(ISwitchable switchable)
        {
            _switchable = switchable;
        }

        public void Execute()
        {
            _switchable.PowerOff();
        }
    }

    public class OpenSwitchCommand : ICommand
    {
        private readonly ISwitchable _switchable;

        public OpenSwitchCommand(ISwitchable switchable)
        {
            _switchable = switchable;
        }

        public void Execute()
        {
            _switchable.PowerOn();
        }
    }
}
