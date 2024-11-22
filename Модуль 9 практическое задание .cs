//Задача 1: Система управления отчетностью с применением паттерна "Декоратор"
public interface IReport
{
    string Generate();
}

public class SalesReport : IReport
{
    public string Generate()
    {
        return "Sales Report Data: Sample sales data.";
    }
}

public class UserReport : IReport
{
    public string Generate()
    {
        return "User Report Data: Sample user data.";
    }
}
//Абстрактный декоратор:
public abstract class ReportDecorator : IReport
{
    protected IReport _report;

    protected ReportDecorator(IReport report)
    {
        _report = report;
    }

    public virtual string Generate()
    {
        return _report.Generate();
    }
}
//Реализация декораторов:
public class DateFilterDecorator : ReportDecorator
{
    private DateTime _startDate;
    private DateTime _endDate;

    public DateFilterDecorator(IReport report, DateTime startDate, DateTime endDate)
        : base(report)
    {
        _startDate = startDate;
        _endDate = endDate;
    }

    public override string Generate()
    {
        return $"{base.Generate()} | Filtered by dates: {_startDate.ToShortDateString()} - {_endDate.ToShortDateString()}";
    }
}

public class SortingDecorator : ReportDecorator
{
    private string _sortBy;

    public SortingDecorator(IReport report, string sortBy)
        : base(report)
    {
        _sortBy = sortBy;
    }

    public override string Generate()
    {
        return $"{base.Generate()} | Sorted by: {_sortBy}";
    }
}

public class CsvExportDecorator : ReportDecorator
{
    public CsvExportDecorator(IReport report) : base(report) { }

    public override string Generate()
    {
        return $"{base.Generate()} | Exported as CSV";
    }
}

public class PdfExportDecorator : ReportDecorator
{
    public PdfExportDecorator(IReport report) : base(report) { }

    public override string Generate()
    {
        return $"{base.Generate()} | Exported as PDF";
    }
}
//Клиентский код:
class Program
{
    static void Main()
    {
        IReport salesReport = new SalesReport();
        IReport decoratedReport = new DateFilterDecorator(
            new SortingDecorator(
                new CsvExportDecorator(salesReport),
                "Amount"),
            new DateTime(2024, 1, 1),
            new DateTime(2024, 12, 31));

        Console.WriteLine(decoratedReport.Generate());
    }
}
//Задача 2: Система мониторинга логистики с паттерном "Адаптер"
//Интерфейс и внутренняя служба доставки:
public interface IInternalDeliveryService
{
    void DeliverOrder(string orderId);
    string GetDeliveryStatus(string orderId);
}

public class InternalDeliveryService : IInternalDeliveryService
{
    public void DeliverOrder(string orderId)
    {
        Console.WriteLine($"Internal Delivery: Order {orderId} is being delivered.");
    }

    public string GetDeliveryStatus(string orderId)
    {
        return $"Internal Delivery: Order {orderId} is in transit.";
    }
}
//Внешние службы логистики:
public class ExternalLogisticsServiceA
{
    public void ShipItem(int itemId)
    {
        Console.WriteLine($"ExternalLogisticsServiceA: Shipping item {itemId}.");
    }

    public string TrackShipment(int shipmentId)
    {
        return $"ExternalLogisticsServiceA: Shipment {shipmentId} is on the way.";
    }
}

public class ExternalLogisticsServiceB
{
    public void SendPackage(string packageInfo)
    {
        Console.WriteLine($"ExternalLogisticsServiceB: Sending package {packageInfo}.");
    }

    public string CheckPackageStatus(string trackingCode)
    {
        return $"ExternalLogisticsServiceB: Tracking code {trackingCode} shows the package is delivered.";
    }
}
//Адаптеры
public class LogisticsAdapterA : IInternalDeliveryService
{
    private ExternalLogisticsServiceA _service;

    public LogisticsAdapterA(ExternalLogisticsServiceA service)
    {
        _service = service;
    }

    public void DeliverOrder(string orderId)
    {
        _service.ShipItem(int.Parse(orderId));
    }

    public string GetDeliveryStatus(string orderId)
    {
        return _service.TrackShipment(int.Parse(orderId));
    }
}

public class LogisticsAdapterB : IInternalDeliveryService
{
    private ExternalLogisticsServiceB _service;

    public LogisticsAdapterB(ExternalLogisticsServiceB service)
    {
        _service = service;
    }

    public void DeliverOrder(string orderId)
    {
        _service.SendPackage(orderId);
    }

    public string GetDeliveryStatus(string orderId)
    {
        return _service.CheckPackageStatus(orderId);
    }
}
//Фабрика
public static class DeliveryServiceFactory
{
    public static IInternalDeliveryService GetDeliveryService(string type)
    {
        return type switch
        {
            "Internal" => new InternalDeliveryService(),
            "ServiceA" => new LogisticsAdapterA(new ExternalLogisticsServiceA()),
            "ServiceB" => new LogisticsAdapterB(new ExternalLogisticsServiceB()),
            _ => throw new ArgumentException("Invalid delivery service type.")
        };
    }
}
//Клиентский код:
class Program
{
    static void Main()
    {
        IInternalDeliveryService service = DeliveryServiceFactory.GetDeliveryService("ServiceA");
        service.DeliverOrder("123");
        Console.WriteLine(service.GetDeliveryStatus("123"));
    }
}





