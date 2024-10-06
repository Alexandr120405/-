//Интерфейс или абстрактный класс Document
using System;

public abstract class Document
{
    public abstract void Open();
}
//Классы для конкретных документов
public class Report : Document
{
    public override void Open()
    {
        Console.WriteLine("Открыт отчет.");
    }
}

public class Resume : Document
{
    public override void Open()
    {
        Console.WriteLine("Открыто резюме.");
    }
}

public class Letter : Document
{
    public override void Open()
    {
        Console.WriteLine("Открыто письмо.");
    }
}
//Абстрактный класс DocumentCreator
public abstract class DocumentCreator
{
    public abstract Document CreateDocument();
}
//Классы для конкретных фабрик документов
public class ReportCreator : DocumentCreator
{
    public override Document CreateDocument()
    {
        return new Report();
    }
}

public class ResumeCreator : DocumentCreator
{
    public override Document CreateDocument()
    {
        return new Resume();
    }
}

public class LetterCreator : DocumentCreator
{
    public override Document CreateDocument()
    {
        return new Letter();
    }
}
//Тестирование фабричного метода
public class Program
{
    public static void ClientCode(DocumentCreator creator)
    {
        Document document = creator.CreateDocument();
        document.Open();
    }

    public static void Main()
    {
        Console.WriteLine("Создаем отчет:");
        ClientCode(new ReportCreator());

        Console.WriteLine("\nСоздаем резюме:");
        ClientCode(new ResumeCreator());

        Console.WriteLine("\nСоздаем письмо:");
        ClientCode(new LetterCreator());
    }
}
//Дополнительные задания:
//Добавляем новый тип документа (Invoice)
public class Invoice : Document
{
    public override void Open()
    {
        Console.WriteLine("Открыт счет-фактура.");
    }
}
//Добавляем фабрику для нового типа документа
public class InvoiceCreator : DocumentCreator
{
    public override Document CreateDocument()
    {
        return new Invoice();
    }
}
//Модифицируем фабричный метод для динамического выбора типа документа
public class Program
{
    public static Document CreateDocumentByType(string docType)
    {
        switch (docType.ToLower())
        {
            case "report":
                return new ReportCreator().CreateDocument();
            case "resume":
                return new ResumeCreator().CreateDocument();
            case "letter":
                return new LetterCreator().CreateDocument();
            case "invoice":
                return new InvoiceCreator().CreateDocument();
            default:
                throw new ArgumentException("Неизвестный тип документа.");
        }
    }

    public static void Main()
    {
        Console.WriteLine("Введите тип документа (report, resume, letter, invoice):");
        string docType = Console.ReadLine();
        try
        {
            Document document = CreateDocumentByType(docType);
            document.Open();
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}

