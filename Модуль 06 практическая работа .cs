//Реализация класса Logger с паттерном "Одиночка"
using System;
using System.IO;
using System.Threading;

public class Logger
{
    private static Logger _instance;
    private static readonly object _lock = new object();
    private string _logFilePath;
    private LogLevel _currentLogLevel;

    // Закрытый конструктор
    private Logger(string logFilePath, LogLevel logLevel)
    {
        _logFilePath = logFilePath;
        _currentLogLevel = logLevel;
    }

    // Статический метод для получения единственного экземпляра
    public static Logger GetInstance(string logFilePath = "log.txt", LogLevel logLevel = LogLevel.INFO)
    {
        if (_instance == null)
        {
            lock (_lock)  // Обеспечение потокобезопасности
            {
                if (_instance == null)
                {
                    _instance = new Logger(logFilePath, logLevel);
                }
            }
        }
        return _instance;
    }

    // Метод для записи логов
    public void Log(string message, LogLevel level)
    {
        if (level >= _currentLogLevel)
        {
            lock (_lock)
            {
                using (StreamWriter writer = new StreamWriter(_logFilePath, true))
                {
                    writer.WriteLine($"{DateTime.Now} [{level}] - {message}");
                }
            }
        }
    }

    // Метод для изменения уровня логирования
    public void SetLogLevel(LogLevel logLevel)
    {
        lock (_lock)
        {
            _currentLogLevel = logLevel;
        }
    }
}

// Перечисление для уровней логирования
public enum LogLevel
{
    INFO,
    WARNING,
    ERROR
}
//Пример использования логгера в многопоточном приложении
using System;
using System.Threading;

class Program
{
    static void Main(string[] args)
    {
        Logger logger = Logger.GetInstance("app_log.txt", LogLevel.INFO);

        Thread thread1 = new Thread(() => LogMessages(logger, "Thread 1"));
        Thread thread2 = new Thread(() => LogMessages(logger, "Thread 2"));

        thread1.Start();
        thread2.Start();

        thread1.Join();
        thread2.Join();
    }

    static void LogMessages(Logger logger, string threadName)
    {
        for (int i = 0; i < 5; i++)
        {
            logger.Log($"{threadName}: Message {i}", LogLevel.INFO);
            Thread.Sleep(100);
        }
    }
}
