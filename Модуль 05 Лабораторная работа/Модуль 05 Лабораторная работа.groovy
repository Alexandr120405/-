//Интерфейс или абстрактный класс Transport
public interface ITransport
{
    void Move();
    void FuelUp();
}
//Классы Car, Motorcycle, Plane, Bicycle
// Автомобиль
public class Car : ITransport
{
    public string Model { get; set; }
    public int Speed { get; set; }

    public Car(string model, int speed)
    {
        Model = model;
        Speed = speed;
    }

    public void Move()
    {
        Console.WriteLine($"Автомобиль {Model} движется со скоростью {Speed} км/ч.");
    }

    public void FuelUp()
    {
        Console.WriteLine("Автомобиль заправляется бензином.");
    }
}

// Мотоцикл
public class Motorcycle : ITransport
{
    public string Model { get; set; }
    public int Speed { get; set; }

    public Motorcycle(string model, int speed)
    {
        Model = model;
        Speed = speed;
    }

    public void Move()
    {
        Console.WriteLine($"Мотоцикл {Model} движется со скоростью {Speed} км/ч.");
    }

    public void FuelUp()
    {
        Console.WriteLine("Мотоцикл заправляется бензином.");
    }
}

// Самолет
public class Plane : ITransport
{
    public string Model { get; set; }
    public int Speed { get; set; }

    public Plane(string model, int speed)
    {
        Model = model;
        Speed = speed;
    }

    public void Move()
    {
        Console.WriteLine($"Самолет {Model} летит со скоростью {Speed} км/ч.");
    }

    public void FuelUp()
    {
        Console.WriteLine("Самолет заправляется авиационным топливом.");
    }
}

// Велосипед
public class Bicycle : ITransport
{
    public string Model { get; set; }
    public int Speed { get; set; }

    public Bicycle(string model, int speed)
    {
        Model = model;
        Speed = speed;
    }

    public void Move()
    {
        Console.WriteLine($"Велосипед {Model} движется со скоростью {Speed} км/ч.");
    }

    public void FuelUp()
    {
        Console.WriteLine("Велосипед не требует топлива.");
    }
}
//Абстрактный класс TransportFactory
public abstract class TransportFactory
{
    public abstract ITransport CreateTransport(string model, int speed);
}
//Конкретные фабрики для каждого типа транспорта
// Фабрика для автомобилей
public class CarFactory : TransportFactory
{
    public override ITransport CreateTransport(string model, int speed)
    {
        return new Car(model, speed);
    }
}

// Фабрика для мотоциклов
public class MotorcycleFactory : TransportFactory
{
    public override ITransport CreateTransport(string model, int speed)
    {
        return new Motorcycle(model, speed);
    }
}

// Фабрика для самолетов
public class PlaneFactory : TransportFactory
{
    public override ITransport CreateTransport(string model, int speed)
    {
        return new Plane(model, speed);
    }
}

// Фабрика для велосипедов
public class BicycleFactory : TransportFactory
{
    public override ITransport CreateTransport(string model, int speed)
    {
        return new Bicycle(model, speed);
    }
}
//Основной класс программы
class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Введите тип транспорта (car, motorcycle, plane, bicycle):");
        string transportType = Console.ReadLine();

        Console.WriteLine("Введите модель транспорта:");
        string model = Console.ReadLine();

        Console.WriteLine("Введите скорость транспорта:");
        int speed = int.Parse(Console.ReadLine());

        TransportFactory factory = null;

        switch (transportType.ToLower())
        {
            case "car":
                factory = new CarFactory();
                break;
            case "motorcycle":
                factory = new MotorcycleFactory();
                break;
            case "plane":
                factory = new PlaneFactory();
                break;
            case "bicycle":
                factory = new BicycleFactory();
                break;
            default:
                Console.WriteLine("Неправильный тип транспорта.");
                return;
        }

        ITransport transport = factory.CreateTransport(model, speed);

        transport.Move();
        transport.FuelUp();
    }
}
