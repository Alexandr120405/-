//Интерфейс для расчета стоимости поездки
public interface ICostCalculationStrategy
{
    double CalculateCost(double distance, int passengers, string serviceClass, List<string> discounts, List<string> extraServices);
}
//Реализация различных стратегий для транспорта
public class AirplaneCostStrategy : ICostCalculationStrategy
{
    public double CalculateCost(double distance, int passengers, string serviceClass, List<string> discounts, List<string> extraServices)
    {
        double baseCost = distance * 0.2 * passengers;
        double serviceMultiplier = serviceClass == "business" ? 1.5 : 1;
        double totalCost = baseCost * serviceMultiplier;

        if (discounts.Contains("child"))
        {
            totalCost *= 0.85;
        }

        if (discounts.Contains("senior"))
        {
            totalCost *= 0.90;
        }

        if (extraServices.Contains("baggage"))
        {
            totalCost += 50 * passengers;
        }

        return totalCost;
    }
}

public class TrainCostStrategy : ICostCalculationStrategy
{
    public double CalculateCost(double distance, int passengers, string serviceClass, List<string> discounts, List<string> extraServices)
    {
        double baseCost = distance * 0.1 * passengers;
        double serviceMultiplier = serviceClass == "business" ? 1.3 : 1;
        double totalCost = baseCost * serviceMultiplier;

        if (discounts.Contains("child"))
        {
            totalCost *= 0.80;
        }

        if (discounts.Contains("senior"))
        {
            totalCost *= 0.85;
        }

        return totalCost;
    }
}

public class BusCostStrategy : ICostCalculationStrategy
{
    public double CalculateCost(double distance, int passengers, string serviceClass, List<string> discounts, List<string> extraServices)
    {
        double baseCost = distance * 0.05 * passengers;

        if (discounts.Contains("child"))
        {
            baseCost *= 0.75;
        }

        if (discounts.Contains("senior"))
        {
            baseCost *= 0.90;
        }

        return baseCost;
    }
}
// Контекст для выбора стратегии
public class TravelBookingContext
{
    private ICostCalculationStrategy _strategy;

    public TravelBookingContext(ICostCalculationStrategy strategy)
    {
        _strategy = strategy;
    }

    public void SetStrategy(ICostCalculationStrategy strategy)
    {
        _strategy = strategy;
    }

    public double CalculateTotalCost(double distance, int passengers, string serviceClass, List<string> discounts, List<string> extraServices)
    {
        return _strategy.CalculateCost(distance, passengers, serviceClass, discounts, extraServices);
    }
}
//Клиентский код
using System;
using System.Collections.Generic;

public class Program
{
    public static void Main(string[] args)
    {
        double distance = 1000;  // В километрах
        int passengers = 2;
        string serviceClass = "economy";  // или "business"
        List<string> discounts = new List<string> { "child" };
        List<string> extraServices = new List<string> { "baggage" };

        // Выбор стратегии для самолета
        TravelBookingContext booking = new TravelBookingContext(new AirplaneCostStrategy());
        double cost = booking.CalculateTotalCost(distance, passengers, serviceClass, discounts, extraServices);
        Console.WriteLine($"Стоимость поездки на самолете: {cost} USD");

        // Изменение стратегии на поезд
        booking.SetStrategy(new TrainCostStrategy());
        cost = booking.CalculateTotalCost(distance, passengers, serviceClass, discounts, extraServices);
        Console.WriteLine($"Стоимость поездки на поезде: {cost} USD");

        // Изменение стратегии на автобус
        booking.SetStrategy(new BusCostStrategy());
        cost = booking.CalculateTotalCost(distance, passengers, serviceClass, discounts, extraServices);
        Console.WriteLine($"Стоимость поездки на автобусе: {cost} USD");
    }
}
//Интерфейс IObserver
public interface IObserver
{
    void Update(string stockSymbol, double price);
}
//Интерфейс ISubject
public interface ISubject
{
    void RegisterObserver(IObserver observer);
    void RemoveObserver(IObserver observer);
    void NotifyObservers();
}
//Реализация класса биржи
using System.Collections.Generic;

public class StockExchange : ISubject
{
    private Dictionary<string, double> _stocks = new Dictionary<string, double>();
    private List<IObserver> _observers = new List<IObserver>();

    public void RegisterObserver(IObserver observer)
    {
        _observers.Add(observer);
    }

    public void RemoveObserver(IObserver observer)
    {
        _observers.Remove(observer);
    }

    public void NotifyObservers()
    {
        foreach (var observer in _observers)
        {
            foreach (var stock in _stocks)
            {
                observer.Update(stock.Key, stock.Value);
            }
        }
    }

    public void SetStockPrice(string stockSymbol, double price)
    {
        _stocks[stockSymbol] = price;
        NotifyObservers();
    }
}
//Реализация наблюдателей
using System;

public class Trader : IObserver
{
    private string _name;

    public Trader(string name)
    {
        _name = name;
    }

    public void Update(string stockSymbol, double price)
    {
        Console.WriteLine($"{_name} получил обновление: Акция {stockSymbol} теперь стоит {price}");
    }
}

public class TradingRobot : IObserver
{
    private double _threshold;

    public TradingRobot(double threshold)
    {
        _threshold = threshold;
    }

    public void Update(string stockSymbol, double price)
    {
        if (price > _threshold)
        {
            Console.WriteLine($"Робот покупает акцию {stockSymbol}, так как цена выше {_threshold}");
        }
        else
        {
            Console.WriteLine($"Робот продает акцию {stockSymbol}, так как цена ниже {_threshold}");
        }
    }
}
//Клиентский код
using System;

public class Program
{
    public static void Main(string[] args)
    {
        StockExchange stockExchange = new StockExchange();

        Trader trader1 = new Trader("Трейдер 1");
        TradingRobot robot = new TradingRobot(100);

        stockExchange.RegisterObserver(trader1);
        stockExchange.RegisterObserver(robot);

        stockExchange.SetStockPrice("AAPL", 120);
        stockExchange.SetStockPrice("GOOG", 90);

        // Удалить трейдера
        stockExchange.RemoveObserver(trader1);

        // Обновить цену акций
        stockExchange.SetStockPrice("AAPL", 80);
    }
}
