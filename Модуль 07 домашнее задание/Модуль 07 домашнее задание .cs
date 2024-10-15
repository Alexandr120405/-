//Интерфейс IPaymentStrategy
public interface IPaymentStrategy
{
    void Pay(double amount);
}
//Реализация стратегий оплаты
public class CreditCardPayment : IPaymentStrategy
{
    public void Pay(double amount)
    {
        Console.WriteLine($"Оплата {amount} с использованием банковской карты.");
    }
}
//Оплата через PayPal:
public class PayPalPayment : IPaymentStrategy
{
    public void Pay(double amount)
    {
        Console.WriteLine($"Оплата {amount} через PayPal.");
    }
}
//Оплата криптовалютой:
public class CryptoPayment : IPaymentStrategy
{
    public void Pay(double amount)
    {
        Console.WriteLine($"Оплата {amount} с использованием криптовалюты.");
    }
}
//Контекст, который работает с разными стратегиями оплаты
public class PaymentContext
{
    private IPaymentStrategy _paymentStrategy;

    public void SetPaymentStrategy(IPaymentStrategy paymentStrategy)
    {
        _paymentStrategy = paymentStrategy;
    }

    public void Pay(double amount)
    {
        if (_paymentStrategy == null)
        {
            Console.WriteLine("Способ оплаты не выбран.");
        }
        else
        {
            _paymentStrategy.Pay(amount);
        }
    }
}
//Клиентский код для тестирования
public class Program
{
    public static void Main(string[] args)
    {
        PaymentContext context = new PaymentContext();

        // Выбор оплаты картой
        context.SetPaymentStrategy(new CreditCardPayment());
        context.Pay(1000);

        // Переключение на PayPal
        context.SetPaymentStrategy(new PayPalPayment());
        context.Pay(500);

        // Переключение на криптовалюту
        context.SetPaymentStrategy(new CryptoPayment());
        context.Pay(300);
    }
}
//Интерфейс наблюдателя IObserver
public interface IObserver
{
    void Update(double usdRate, double eurRate);
}
//Интерфейс субъекта ISubject
public interface ISubject
{
    void RegisterObserver(IObserver observer);
    void RemoveObserver(IObserver observer);
    void NotifyObservers();
}
//Реализация класса субъекта CurrencyExchange
public class CurrencyExchange : ISubject
{
    private List<IObserver> observers;
    private double usdRate;
    private double eurRate;

    public CurrencyExchange()
    {
        observers = new List<IObserver>();
    }

    public void SetRates(double usd, double eur)
    {
        usdRate = usd;
        eurRate = eur;
        NotifyObservers();
    }

    public void RegisterObserver(IObserver observer)
    {
        observers.Add(observer);
    }

    public void RemoveObserver(IObserver observer)
    {
        observers.Remove(observer);
    }

    public void NotifyObservers()
    {
        foreach (var observer in observers)
        {
            observer.Update(usdRate, eurRate);
        }
    }
}
//Наблюдатель для банков:
public class BankObserver : IObserver
{
    public void Update(double usdRate, double eurRate)
    {
        Console.WriteLine($"Банк получил обновление: USD = {usdRate}, EUR = {eurRate}");
    }
}
//Наблюдатель для брокеров:
public class BrokerObserver : IObserver
{
    public void Update(double usdRate, double eurRate)
    {
        Console.WriteLine($"Брокер получил обновление: USD = {usdRate}, EUR = {eurRate}");
    }
}
//Наблюдатель для пользователей:
public class UserObserver : IObserver
{
    public void Update(double usdRate, double eurRate)
    {
        Console.WriteLine($"Пользователь получил обновление: USD = {usdRate}, EUR = {eurRate}");
    }
}
//Клиентский код для тестирования
public class Program
{
    public static void Main(string[] args)
    {
        CurrencyExchange exchange = new CurrencyExchange();

        IObserver bank = new BankObserver();
        IObserver broker = new BrokerObserver();
        IObserver user = new UserObserver();

        exchange.RegisterObserver(bank);
        exchange.RegisterObserver(broker);
        exchange.RegisterObserver(user);

        // Обновление курсов валют
        exchange.SetRates(75.5, 88.7);

        // Удаление брокера из подписчиков
        exchange.RemoveObserver(broker);

        // Обновление курсов валют после удаления брокера
        exchange.SetRates(76.3, 89.1);
    }
}
