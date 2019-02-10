using System;

namespace AdapterPattern
{
    class Program
    {
        /*
         * Intent
         * Convert the interface of a class into another interface clients expect.
         * Adapter lets classes work together, that could not otherwise because of incompatible interfaces.
         */
        static void Main(string[] args)
        {
            var microUsbRecharger = new MicroUsbRecharger();
            var iPhoneRecharger = new IPhoneRecharger();
            var androidRecharger = new AndroidRecharger();
        }
    }

    public interface IFormatIPhone
    {
        void Recharge();
        void UseLightning();
    }

    public interface IFormatAndroid
    {
        void Recharge();
        void UseMicroUsb();
    }

    // Adaptee
    public class IPhone : IFormatIPhone
    {
        private bool _connectorOk;

        public void UseLightning()
        {
            _connectorOk = true;
            Console.WriteLine("Lightning connected");
        }

        public void Recharge()
        {
            if (_connectorOk)
            {
                Console.WriteLine("Recharge Started");
                Console.WriteLine("Recharge 20%");
                Console.WriteLine("Recharge 50%");
                Console.WriteLine("Recharge 70%");
                Console.WriteLine("Recharge Finished");
            }
            else
            {
                Console.WriteLine("Connect Lightning first");
            }
        }
    }

    // Adapter
    public class IPhoneAdapter : IFormatAndroid
    {
        private readonly IFormatIPhone _mobile;

        public IPhoneAdapter(IFormatIPhone mobile)
        {
            _mobile = mobile;
        }

        public void Recharge()
        {
            _mobile.Recharge();
        }

        public void UseMicroUsb()
        {
            Console.WriteLine("MicroUsb connected -> ");
            _mobile.UseLightning();
        }
    }

    public class Android : IFormatAndroid
    {
        private bool _connectorOk;

        public void UseMicroUsb()
        {
            _connectorOk = true;
            Console.WriteLine("MicroUsb connected ->");
        }

        public void Recharge()
        {
            if (_connectorOk)
            {
                Console.WriteLine("Recharge Started");
                Console.WriteLine("Recharge 20%");
                Console.WriteLine("Recharge 50%");
                Console.WriteLine("Recharge 70%");
                Console.WriteLine("Recharge Finished");
            }
            else
            {
                Console.WriteLine("Connect MicroUsb first");
            }
        }
    }

    // client
    public class MicroUsbRecharger
    {
        public MicroUsbRecharger()
        {
            Console.WriteLine("---Recharging iPhone with Generic Recharger---");
            var phone = new IPhone();
            var iPhoneAdapter = new IPhoneAdapter(phone);
            iPhoneAdapter.UseMicroUsb();
            iPhoneAdapter.Recharge();
            Console.WriteLine("---iPhone Ready for use---");
        }
    }

    public class IPhoneRecharger
    {
        public IPhoneRecharger()
        {
            Console.WriteLine("---Recharging iPhone with iPhone Recharger---");
            var phone = new IPhone();
            phone.UseLightning();
            phone.Recharge();
            Console.WriteLine("---iPhone Ready for use---");
        }
    }

    public class AndroidRecharger
    {
        public AndroidRecharger()
        {
            Console.WriteLine("---Recharging Android Phone with Generic Recharger---");
            var phone = new Android();
            phone.UseMicroUsb();
            phone.Recharge();
            Console.WriteLine("---Phone Ready for use---");
        }
    }
}
