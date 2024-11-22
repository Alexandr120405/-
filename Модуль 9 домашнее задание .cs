//Задача 1: Система управления заказами в кафе (паттерн "Декоратор")
using System;

namespace CafeOrderSystem
{
    // Базовый интерфейс для напитков
    public abstract class Beverage
    {
        public abstract string GetDescription();
        public abstract double Cost();
    }

    // Конкретные напитки
    public class Espresso : Beverage
    {
        public override string GetDescription()
        {
            return "Espresso";
        }

        public override double Cost()
        {
            return 50.0; // Цена эспрессо
        }
    }

    public class Tea : Beverage
    {
        public override string GetDescription()
        {
            return "Tea";
        }

        public override double Cost()
        {
            return 30.0; // Цена чая
        }
    }

    // Абстрактный декоратор
    public abstract class BeverageDecorator : Beverage
    {
        protected Beverage _beverage;

        public BeverageDecorator(Beverage beverage)
        {
            _beverage = beverage;
        }

        public override string GetDescription()
        {
            return _beverage.GetDescription();
        }

        public override double Cost()
        {
            return _beverage.Cost();
        }
    }

    // Конкретные декораторы (добавки)
    public class Milk : BeverageDecorator
    {
        public Milk(Beverage beverage) : base(beverage) { }

        public override string GetDescription()
        {
            return _beverage.GetDescription() + ", Milk";
        }

        public override double Cost()
        {
            return _beverage.Cost() + 10.0; // Цена молока
        }
    }

    public class Sugar : BeverageDecorator
    {
        public Sugar(Beverage beverage) : base(beverage) { }

        public override string GetDescription()
        {
            return _beverage.GetDescription() + ", Sugar";
        }

        public override double Cost()
        {
            return _beverage.Cost() + 5.0; // Цена сахара
        }
    }

    public class WhippedCream : BeverageDecorator
    {
        public WhippedCream(Beverage beverage) : base(beverage) { }

        public override string GetDescription()
        {
            return _beverage.GetDescription() + ", Whipped Cream";
        }

        public override double Cost()
        {
            return _beverage.Cost() + 15.0; // Цена взбитых сливок
        }
    }

    // Клиентский код
    class Program
    {
        static void Main(string[] args)
        {
            // Базовый напиток
            Beverage beverage = new Espresso();
            Console.WriteLine($"{beverage.GetDescription()} costs {beverage.Cost()}");

            // Добавление добавок
            beverage = new Milk(beverage);
            beverage = new Sugar(beverage);
            beverage = new WhippedCream(beverage);

            Console.WriteLine($"{beverage.GetDescription()} costs {beverage.Cost()}");

            // Новый тип напитка с другими добавками
            Beverage tea = new Tea();
            tea = new Sugar(tea);
            tea = new Milk(tea);

            Console.WriteLine($"{tea.GetDescription()} costs {tea.Cost()}");
        }
    }
}
//Задача 2: Адаптер для сторонней системы оплаты
using System;

namespace PaymentAdapterPattern
{
    // Интерфейс для процессора платежей
    public interface IPaymentProcessor
    {
        void ProcessPayment(double amount);
    }

    // Реализация для PayPal
    public class PayPalPaymentProcessor : IPaymentProcessor
    {
        public void ProcessPayment(double amount)
        {
            Console.WriteLine($"Processing PayPal payment of {amount} units.");
        }
    }

    // Сторонний сервис Stripe
    public class StripePaymentService
    {
        public void MakeTransaction(double totalAmount)
        {
            Console.WriteLine($"Processing Stripe payment of {totalAmount} units.");
        }
    }

    // Адаптер для Stripe
    public class StripePaymentAdapter : IPaymentProcessor
    {
        private readonly StripePaymentService _stripePaymentService;

        public StripePaymentAdapter(StripePaymentService stripePaymentService)
        {
            _stripePaymentService = stripePaymentService;
        }

        public void ProcessPayment(double amount)
        {
            _stripePaymentService.MakeTransaction(amount);
        }
    }

    // Ещё один сторонний сервис (например, ApplePay)
    public class ApplePayService
    {
        public void Pay(double sum)
        {
            Console.WriteLine($"Processing ApplePay payment of {sum} units.");
        }
    }

    // Адаптер для ApplePay
    public class ApplePayAdapter : IPaymentProcessor
    {
        private readonly ApplePayService _applePayService;

        public ApplePayAdapter(ApplePayService applePayService)
        {
            _applePayService = applePayService;
        }

        public void ProcessPayment(double amount)
        {
            _applePayService.Pay(amount);
        }
    }

    // Клиентский код
    class Program
    {
        static void Main(string[] args)
        {
            // Работа с PayPal
            IPaymentProcessor paypalProcessor = new PayPalPaymentProcessor();
            paypalProcessor.ProcessPayment(100.0);

            // Работа с Stripe через адаптер
            StripePaymentService stripeService = new StripePaymentService();
            IPaymentProcessor stripeProcessor = new StripePaymentAdapter(stripeService);
            stripeProcessor.ProcessPayment(200.0);

            // Работа с ApplePay через адаптер
            ApplePayService applePayService = new ApplePayService();
            IPaymentProcessor applePayProcessor = new ApplePayAdapter(applePayService);
            applePayProcessor.ProcessPayment(300.0);
        }
    }
}

