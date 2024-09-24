using System;

public abstract class Transport
{
    public abstract void Move();
    public abstract void FuelUp();
}

public class Car : Transport
{
    public override void Move()
    {
        Console.WriteLine("BMW");
    }

    public override void FuelUp()
    {
        Console.WriteLine("100");
    }
}

public class Motorcycle : Transport
{
    public override void Move()
    {
        Console.WriteLine("Yamaha");
    }

    public override void FuelUp()
    {
        Console.WriteLine("120");
    }
}

public class Plane : Transport
{
    public override void Move()
    {
        Console.WriteLine("Boeing");
    }

    public override void FuelUp()
    {
        Console.WriteLine("150");
    }
}

public abstract class TransportFactory
{
    public abstract Transport CreateTransport();
}

public class CarFactory : TransportFactory
{
    public override Transport CreateTransport()
    {
        return new Car();
    }
}

public class MotorcycleFactory : TransportFactory
{
    public override Transport CreateTransport()
    {
        return new Motorcycle();
    }
}

public class PlaneFactory : TransportFactory
{
    public override Transport CreateTransport()
    {
        return new Plane();
    }
}

public class Program
{
    public static void Main(string[] args)
    {
        TransportFactory tFactory = new CarFactory();
        var car = tFactory.CreateTransport();
        
        car.Move();
        car.FuelUp();
    }
}
