public interface IPrintable
{
    void Print(string content);
}

public interface IScannable
{
    void Scan(string content);
}

public interface IFaxable
{
    void Fax(string content);
}

public class AllInOnePrinter : IPrintable, IScannable, IFaxable
{
    public void Print(string content)
    {
        Console.WriteLine("Printing: " + content);
    }

    public void Scan(string content)
    {
        Console.WriteLine("Scanning: " + content);
    }

    public void Fax(string content)
    {
        Console.WriteLine("Faxing: " + content);
    }
}

public class BasicPrinter : IPrintable
{
    public void Print(string content)
    {
        Console.WriteLine("Printing: " + content);
    }
}
