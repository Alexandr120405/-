// Интерфейс IVehicle
using System;

public interface IVehicle
{
    void Drive();
    void Refuel();
}
//Конкретные классы транспортных средств
public class Car : IVehicle
{
    public string Brand { get; }
    public string Model { get; }
    public string FuelType { get; }

    public Car(string brand, string model, string fuelType)
    {
        Brand = brand;
        Model = model;
        FuelType = fuelType;
    }

    public void Drive()
    {
        Console.WriteLine($"The car {Brand} {Model} is driving.");
    }

    public void Refuel()
    {
        Console.WriteLine($"Refueling the car with {FuelType}.");
    }
}
//Motorcycle
public class Motorcycle : IVehicle
{
    public string BikeType { get; }
    public int EngineCapacity { get; }

    public Motorcycle(string bikeType, int engineCapacity)
    {
        BikeType = bikeType;
        EngineCapacity = engineCapacity;
    }

    public void Drive()
    {
        Console.WriteLine($"The {BikeType} motorcycle with {EngineCapacity}cc is driving.");
    }

    public void Refuel()
    {
        Console.WriteLine($"Refueling the motorcycle with {EngineCapacity}cc engine.");
    }
}
//Truck
public class Truck : IVehicle
{
    public double LoadCapacity { get; }
    public int NumAxles { get; }

    public Truck(double loadCapacity, int numAxles)
    {
        LoadCapacity = loadCapacity;
        NumAxles = numAxles;
    }

    public void Drive()
    {
        Console.WriteLine($"The truck with {LoadCapacity} tons capacity and {NumAxles} axles is driving.");
    }

    public void Refuel()
    {
        Console.WriteLine("Refueling the truck.");
    }
}
//Абстрактная фабрика VehicleFactory
public abstract class VehicleFactory
{
    public abstract IVehicle CreateVehicle();
}
//Конкретные фабрики для каждого типа транспортных средств
//CarFactory
public class CarFactory : VehicleFactory
{
    private string _brand;
    private string _model;
    private string _fuelType;

    public CarFactory(string brand, string model, string fuelType)
    {
        _brand = brand;
        _model = model;
        _fuelType = fuelType;
    }

    public override IVehicle CreateVehicle()
    {
        return new Car(_brand, _model, _fuelType);
    }
}
//MotorcycleFactory
public class MotorcycleFactory : VehicleFactory
{
    private string _bikeType;
    private int _engineCapacity;

    public MotorcycleFactory(string bikeType, int engineCapacity)
    {
        _bikeType = bikeType;
        _engineCapacity = engineCapacity;
    }

    public override IVehicle CreateVehicle()
    {
        return new Motorcycle(_bikeType, _engineCapacity);
    }
}
//TruckFactory
public class TruckFactory : VehicleFactory
{
    private double _loadCapacity;
    private int _numAxles;

    public TruckFactory(double loadCapacity, int numAxles)
    {
        _loadCapacity = loadCapacity;
        _numAxles = numAxles;
    }

    public override IVehicle CreateVehicle()
    {
        return new Truck(_loadCapacity, _numAxles);
    }
}
//Новый тип транспорта — Bus
//Bus
public class Bus : IVehicle
{
    public int PassengerCapacity { get; }
    public string RouteNumber { get; }

    public Bus(int passengerCapacity, string routeNumber)
    {
        PassengerCapacity = passengerCapacity;
        RouteNumber = routeNumber;
    }

    public void Drive()
    {
        Console.WriteLine($"The bus with route number {RouteNumber} is driving with {PassengerCapacity} passengers.");
    }

    public void Refuel()
    {
        Console.WriteLine("Refueling the bus.");
    }
}
//BusFactory
public class BusFactory : VehicleFactory
{
    private int _passengerCapacity;
    private string _routeNumber;

    public BusFactory(int passengerCapacity, string routeNumber)
    {
        _passengerCapacity = passengerCapacity;
        _routeNumber = routeNumber;
    }

    public override IVehicle CreateVehicle()
    {
        return new Bus(_passengerCapacity, _routeNumber);
    }
}
//Ввод данных от пользователя
using System;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Enter vehicle type (car, motorcycle, truck, bus): ");
        string vehicleType = Console.ReadLine().ToLower();

        VehicleFactory factory = null;

        switch (vehicleType)
        {
            case "car":
                Console.WriteLine("Enter car brand: ");
                string brand = Console.ReadLine();
                
                Console.WriteLine("Enter car model: ");
                string model = Console.ReadLine();
                
                Console.WriteLine("Enter fuel type: ");
                string fuelType = Console.ReadLine();
                
                factory = new CarFactory(brand, model, fuelType);
                break;

            case "motorcycle":
                Console.WriteLine("Enter bike type (sport, touring): ");
                string bikeType = Console.ReadLine();
                
                Console.WriteLine("Enter engine capacity (cc): ");
                int engineCapacity = int.Parse(Console.ReadLine());
                
                factory = new MotorcycleFactory(bikeType, engineCapacity);
                break;

            case "truck":
                Console.WriteLine("Enter load capacity (tons): ");
                double loadCapacity = double.Parse(Console.ReadLine());
                
                Console.WriteLine("Enter number of axles: ");
                int numAxles = int.Parse(Console.ReadLine());
                
                factory = new TruckFactory(loadCapacity, numAxles);
                break;

            case "bus":
                Console.WriteLine("Enter passenger capacity: ");
                int passengerCapacity = int.Parse(Console.ReadLine());
                
                Console.WriteLine("Enter route number: ");
                string routeNumber = Console.ReadLine();
                
                factory = new BusFactory(passengerCapacity, routeNumber);
                break;

            default:
                Console.WriteLine("Unknown vehicle type!");
                return;
        }

        IVehicle vehicle = factory.CreateVehicle();
        vehicle.Drive();
        vehicle.Refuel();
    }
}

