//Задание 1
using System;
using System.IO;
using System.Threading;

public enum LogLevel
{
    INFO,
    WARNING,
    ERROR
}

public class Logger
{
    private static Logger _instance;
    private static readonly object _lock = new object();
    private LogLevel _currentLogLevel;
    private string _logFilePath;

    private Logger()
    {
        _currentLogLevel = LogLevel.INFO;
        _logFilePath = "log.txt";
    }

    public static Logger GetInstance()
    {
        if (_instance == null)
        {
            lock (_lock)
            {
                if (_instance == null)
                {
                    _instance = new Logger();
                }
            }
        }
        return _instance;
    }

    public void SetLogLevel(LogLevel level)
    {
        _currentLogLevel = level;
    }

    public void SetLogFilePath(string path)
    {
        _logFilePath = path;
    }

    public void Log(string message, LogLevel level)
    {
        if (level >= _currentLogLevel)
        {
            lock (_lock) // для потокобезопасной записи в файл
            {
                using (StreamWriter writer = new StreamWriter(_logFilePath, true))
                {
                    writer.WriteLine($"{DateTime.Now} [{level}]: {message}");
                }
            }
        }
    }
}

// Тестирование в многопоточном приложении
public class Program
{
    public static void Main(string[] args)
    {
        Logger logger = Logger.GetInstance();
        logger.SetLogLevel(LogLevel.WARNING); // Записываем только WARNING и ERROR
        
        Thread thread1 = new Thread(() => LogMessages("Thread1"));
        Thread thread2 = new Thread(() => LogMessages("Thread2"));
        
        thread1.Start();
        thread2.Start();
        
        thread1.Join();
        thread2.Join();
    }

    private static void LogMessages(string threadName)
    {
        Logger logger = Logger.GetInstance();
        logger.Log($"{threadName}: This is an info message.", LogLevel.INFO);
        logger.Log($"{threadName}: This is a warning message.", LogLevel.WARNING);
        logger.Log($"{threadName}: This is an error message.", LogLevel.ERROR);
    }
}
//Задание 2
using System;

// Класс продукта
public class Computer
{
    public string CPU { get; set; }
    public string RAM { get; set; }
    public string Storage { get; set; }
    public string GPU { get; set; }
    public string OS { get; set; }

    public override string ToString()
    {
        return $"Компьютер: CPU - {CPU}, RAM - {RAM}, Накопитель - {Storage}, GPU - {GPU}, ОС - {OS}";
    }
}

// Интерфейс строителя
public interface IComputerBuilder
{
    void SetCPU();
    void SetRAM();
    void SetStorage();
    void SetGPU();
    void SetOS();
    Computer GetComputer();
}

// Конкретный строитель для офисного компьютера
public class OfficeComputerBuilder : IComputerBuilder
{
    private Computer _computer = new Computer();

    public void SetCPU() => _computer.CPU = "Intel i3";
    public void SetRAM() => _computer.RAM = "8GB";
    public void SetStorage() => _computer.Storage = "1TB HDD";
    public void SetGPU() => _computer.GPU = "Integrated";
    public void SetOS() => _computer.OS = "Windows 10";

    public Computer GetComputer() => _computer;
}

// Конкретный строитель для игрового компьютера
public class GamingComputerBuilder : IComputerBuilder
{
    private Computer _computer = new Computer();

    public void SetCPU() => _computer.CPU = "Intel i9";
    public void SetRAM() => _computer.RAM = "32GB";
    public void SetStorage() => _computer.Storage = "1TB SSD";
    public void SetGPU() => _computer.GPU = "NVIDIA RTX 3080";
    public void SetOS() => _computer.OS = "Windows 11";

    public Computer GetComputer() => _computer;
}

// Директор, управляющий процессом сборки
public class ComputerDirector
{
    private IComputerBuilder _builder;

    public ComputerDirector(IComputerBuilder builder)
    {
        _builder = builder;
    }

    public void ConstructComputer()
    {
        _builder.SetCPU();
        _builder.SetRAM();
        _builder.SetStorage();
        _builder.SetGPU();
        _builder.SetOS();
    }

    public Computer GetComputer()
    {
        return _builder.GetComputer();
    }
}

// Клиентский код
public class Program
{
    public static void Main(string[] args)
    {
        // Создаем офисный компьютер
        IComputerBuilder officeBuilder = new OfficeComputerBuilder();
        ComputerDirector director = new ComputerDirector(officeBuilder);
        director.ConstructComputer();
        Computer officeComputer = director.GetComputer();
        Console.WriteLine(officeComputer);

        // Создаем игровой компьютер
        IComputerBuilder gamingBuilder = new GamingComputerBuilder();
        director = new ComputerDirector(gamingBuilder);
        director.ConstructComputer();
        Computer gamingComputer = director.GetComputer();
        Console.WriteLine(gamingComputer);
    }
}
//Задание 3
using System;
using System.Collections.Generic;

// Интерфейс прототипа
public interface IPrototype<T>
{
    T Clone();
}

// Класс Section (раздел)
public class Section : IPrototype<Section>
{
    public string Title { get; set; }
    public string Content { get; set; }

    public Section(string title, string content)
    {
        Title = title;
        Content = content;
    }

    public Section Clone()
    {
        return new Section(Title, Content); // Глубокое копирование
    }

    public override string ToString()
    {
        return $"Section: {Title}, Content: {Content}";
    }
}

// Класс Image (изображение)
public class Image : IPrototype<Image>
{
    public string Url { get; set; }

    public Image(string url)
    {
        Url = url;
    }

    public Image Clone()
    {
        return new Image(Url); // Глубокое копирование
    }

    public override string ToString()
    {
        return $"Image: URL - {Url}";
    }
}

// Класс Document (документ)
public class Document : IPrototype<Document>
{
    public string Title { get; set; }
    public string Content { get; set; }
    public List<Section> Sections { get; private set; } = new List<Section>();
    public List<Image> Images { get; private set; } = new List<Image>();

    public Document(string title, string content)
    {
        Title = title;
        Content = content;
    }

    // Метод для добавления разделов
    public void AddSection(Section section)
    {
        Sections.Add(section);
    }

    // Метод для добавления изображений
    public void AddImage(Image image)
    {
        Images.Add(image);
    }

    // Метод клонирования документа
    public Document Clone()
    {
        Document clonedDocument = new Document(Title, Content);
        
        // Глубокое копирование секций
        foreach (var section in Sections)
        {
            clonedDocument.AddSection(section.Clone());
        }
        
        // Глубокое копирование изображений
        foreach (var image in Images)
        {
            clonedDocument.AddImage(image.Clone());
        }

        return clonedDocument;
    }

    public override string ToString()
    {
        string docInfo = $"Document: {Title}, Content: {Content}\nSections:\n";
        foreach (var section in Sections)
        {
            docInfo += section + "\n";
        }

        docInfo += "Images:\n";
        foreach (var image in Images)
        {
            docInfo += image + "\n";
        }

        return docInfo;
    }
}

// Класс DocumentManager для управления документами
public class DocumentManager
{
    public Document CreateDocument(IPrototype<Document> prototype)
    {
        return prototype.Clone();
    }
}

// Клиентский код
public class Program
{
    public static void Main(string[] args)
    {
        // Создаем оригинальный документ
        Document originalDocument = new Document("Оригинальный документ", "Основной текст документа");
        originalDocument.AddSection(new Section("Введение", "Текст введения"));
        originalDocument.AddSection(new Section("Заключение", "Текст заключения"));
        originalDocument.AddImage(new Image("https://example.com/image1.png"));

        Console.WriteLine("Оригинальный документ:");
        Console.WriteLine(originalDocument);

        // Клонируем документ
        DocumentManager documentManager = new DocumentManager();
        Document clonedDocument = documentManager.CreateDocument(originalDocument);

        // Изменяем заголовок и добавляем новый раздел в клонированный документ
        clonedDocument.Title = "Клонированный документ";
        clonedDocument.AddSection(new Section("Дополнительный раздел", "Текст нового раздела"));

        Console.WriteLine("\nКлонированный документ (с изменениями):");
        Console.WriteLine(clonedDocument);
    }
}
