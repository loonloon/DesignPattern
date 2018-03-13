using System;
using System.Collections.Generic;
using System.Linq;

namespace StrategyPattern
{
    class Program
    {
        static void Main(string[] args)
        {
            var firstCustomer = new Customer(new NormalStrategy());
            firstCustomer.Add(1, 1);

            firstCustomer.BillingStrategy = new HappyHourStrategy();
            firstCustomer.Add(1, 2);

            firstCustomer.PrintBill();

            var secondCustomer = new Customer(new HappyHourStrategy());
            secondCustomer.Add(0.8, 1);

            secondCustomer.BillingStrategy = new NormalStrategy();
            secondCustomer.Add(1.3, 2);
            secondCustomer.Add(2.5, 1);

            secondCustomer.PrintBill();
        }
    }

    public class Customer
    {
        private readonly IList<double> _drinks;
        public IBillingStrategy BillingStrategy { get; set; }

        public Customer(IBillingStrategy billingStrategy)
        {
            _drinks = new List<double>();
            BillingStrategy = billingStrategy;
        }

        public void Add(double price, int quantity)
        {
            _drinks.Add(BillingStrategy.GetActPrice(price * quantity));
        }

        public void PrintBill()
        {
            Console.WriteLine($"Total due: {_drinks.Sum()}");
            _drinks.Clear();
        }
    }

    public interface IBillingStrategy
    {
        double GetActPrice(double rawPrice);
    }

    public class NormalStrategy : IBillingStrategy
    {
        public double GetActPrice(double rawPrice)
        {
            return rawPrice;
        }
    }

    public class HappyHourStrategy : IBillingStrategy
    {
        public double GetActPrice(double rawPrice)
        {
            return rawPrice * 0.5;
        }
    }
}
