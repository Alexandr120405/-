//Интерфейс IShippingStrategy:
public interface IShippingStrategy
{
    decimal CalculateShippingCost(decimal weight, decimal distance);
}
//Реализация стратегий доставки:
// Стандартная доставка
public class StandardShippingStrategy : IShippingStrategy
{
    public decimal CalculateShippingCost(decimal weight, decimal distance)
    {
        return weight * 0.5m + distance * 0.1m;
    }
}

// Экспресс-доставка
public class ExpressShippingStrategy : IShippingStrategy
{
    public decimal CalculateShippingCost(decimal weight, decimal distance)
    {
        return (weight * 0.75m + distance * 0.2m) + 10; // Дополнительная плата за скорость
    }
}

// Международная доставка
public class InternationalShippingStrategy : IShippingStrategy
{
    public decimal CalculateShippingCost(decimal weight, decimal distance)
    {
        return weight * 1.0m + distance * 0.5m + 15; // Дополнительные сборы за международную доставку
    }
}

// Ночная доставка (новая стратегия)
public class NightShippingStrategy : IShippingStrategy
{
    public decimal CalculateShippingCost(decimal weight, decimal distance)
    {
        return weight * 0.6m + distance * 0.15m + 20; // Фиксированная доплата за срочность
    }
}
//Класс DeliveryContext:
public class DeliveryContext
{
    private IShippingStrategy _shippingStrategy;

    // Метод для установки стратегии доставки
    public void SetShippingStrategy(IShippingStrategy strategy)
    {
        _shippingStrategy = strategy;
    }

    // Метод для расчета стоимости доставки
    public decimal CalculateCost(decimal weight, decimal distance)
    {
        if (_shippingStrategy == null)
        {
            throw new InvalidOperationException("Стратегия доставки не установлена.");
        }
        return _shippingStrategy.CalculateShippingCost(weight, distance);
    }
}
//Клиентский код:
class Program
{
    static void Main(string[] args)
    {
        DeliveryContext deliveryContext = new DeliveryContext();

        Console.WriteLine("Выберите тип доставки: 1 - Стандартная, 2 - Экспресс, 3 - Международная, 4 - Ночная");
        string choice = Console.ReadLine();

        switch (choice)
        {
            case "1":
                deliveryContext.SetShippingStrategy(new StandardShippingStrategy());
                break;
            case "2":
                deliveryContext.SetShippingStrategy(new ExpressShippingStrategy());
                break;
            case "3":
                deliveryContext.SetShippingStrategy(new InternationalShippingStrategy());
                break;
            case "4":
                deliveryContext.SetShippingStrategy(new NightShippingStrategy());
                break;
            default:
                Console.WriteLine("Неверный выбор.");
                return;
        }

        Console.WriteLine("Введите вес посылки (кг):");
        decimal weight = Convert.ToDecimal(Console.ReadLine());

        Console.WriteLine("Введите расстояние доставки (км):");
        decimal distance = Convert.ToDecimal(Console.ReadLine());

        if (weight < 0 || distance < 0)
        {
            Console.WriteLine("Вес и расстояние не могут быть отрицательными.");
            return;
        }

        decimal cost = deliveryContext.CalculateCost(weight, distance);
        Console.WriteLine($"Стоимость доставки: {cost:C}");
    }
}
//Интерфейс IObserver:
public interface IObserver
{
    void Update(float temperature);
}
//Интерфейс ISubject:
public interface ISubject
{
    void RegisterObserver(IObserver observer);
    void RemoveObserver(IObserver observer);
    void NotifyObservers();
}
//Класс WeatherStation (субъект):
using System;
using System.Collections.Generic;

public class WeatherStation : ISubject
{
    private List<IObserver> observers;
    private float temperature;

    public WeatherStation()
    {
        observers = new List<IObserver>();
    }

    public void RegisterObserver(IObserver observer)
    {
        observers.Add(observer);
    }

    public void RemoveObserver(IObserver observer)
    {
        if (observers.Contains(observer))
        {
            observers.Remove(observer);
        }
        else
        {
            Console.WriteLine("Наблюдатель не зарегистрирован.");
        }
    }

    public void NotifyObservers()
    {
        foreach (var observer in observers)
        {
            observer.Update(temperature);
        }
    }

    public void SetTemperature(float newTemperature)
    {
        if (newTemperature < -50 || newTemperature > 50)
        {
            Console.WriteLine("Ошибка: Некорректная температура.");
            return;
        }

        Console.WriteLine($"Изменение температуры: {newTemperature}°C");
        temperature = newTemperature;
        NotifyObservers();
    }
}
//Класс WeatherDisplay (наблюдатель):
public class WeatherDisplay : IObserver
{
    private string _name;

    public WeatherDisplay(string name)
    {
        _name = name;
    }

    public void Update(float temperature)
    {
        Console.WriteLine($"{_name} показывает новую температуру: {temperature}°C");
    }
}
//Клиентский код:
class Program
{
    static void Main(string[] args)
    {
        WeatherStation weatherStation = new WeatherStation();

        // Создаем несколько наблюдателей
        WeatherDisplay mobileApp = new WeatherDisplay("Мобильное приложение");
        WeatherDisplay digitalBillboard = new WeatherDisplay("Электронное табло");

        // Регистрируем наблюдателей в системе
        weatherStation.RegisterObserver(mobileApp);
        weatherStation.RegisterObserver(digitalBillboard);

        // Изменяем температуру на станции
        weatherStation.SetTemperature(25.0f);
        weatherStation.SetTemperature(30.0f);

        // Убираем один из наблюдателей и меняем температуру
        weatherStation.RemoveObserver(digitalBillboard);
        weatherStation.SetTemperature(28.0f);
    }
}
