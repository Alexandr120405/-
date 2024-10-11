//Реализация класса ConfigurationManager
using System;
using System.Collections.Generic;
using System.IO;

public sealed class ConfigurationManager
{
    // Статическая переменная для хранения единственного экземпляра
    private static ConfigurationManager instance = null;

    // Блокировка для потокобезопасности
    private static readonly object lockObject = new object();

    // Словарь для хранения настроек
    private Dictionary<string, string> settings;

    // Приватный конструктор для предотвращения создания экземпляра вне класса
    private ConfigurationManager()
    {
        settings = new Dictionary<string, string>();
    }

    // Метод для получения единственного экземпляра класса (с ленивой инициализацией)
    public static ConfigurationManager GetInstance()
    {
        // Потокобезопасность с использованием блокировки
        if (instance == null)
        {
            lock (lockObject)
            {
                if (instance == null)
                {
                    instance = new ConfigurationManager();
                }
            }
        }
        return instance;
    }

    // Метод для загрузки настроек из файла
    public void LoadSettings(string filePath)
    {
        if (File.Exists(filePath))
        {
            string[] lines = File.ReadAllLines(filePath);
            foreach (string line in lines)
            {
                string[] parts = line.Split('=');
                if (parts.Length == 2)
                {
                    settings[parts[0].Trim()] = parts[1].Trim();
                }
            }
        }
        else
        {
            throw new FileNotFoundException("Файл настроек не найден.");
        }
    }

    // Метод для получения значения настройки
    public string GetSetting(string key)
    {
        if (settings.ContainsKey(key))
        {
            return settings[key];
        }
        throw new KeyNotFoundException($"Настройка с ключом '{key}' не найдена.");
    }

    // Метод для изменения значения настройки
    public void SetSetting(string key, string value)
    {
        settings[key] = value;
    }

    // Метод для сохранения настроек в файл
    public void SaveSettings(string filePath)
    {
        using (StreamWriter writer = new StreamWriter(filePath))
        {
            foreach (var setting in settings)
            {
                writer.WriteLine($"{setting.Key}={setting.Value}");
            }
        }
    }
}
//Тестирование класса в многопоточном приложении
using System;
using System.Threading;
using System.Threading.Tasks;

class Program
{
    static void Main(string[] args)
    {
        // Загрузка настроек из файла
        var manager = ConfigurationManager.GetInstance();
        manager.LoadSettings("appsettings.txt");

        // Тестирование работы с многопоточностью
        Task[] tasks = new Task[5];
        for (int i = 0; i < 5; i++)
        {
            tasks[i] = Task.Run(() =>
            {
                var configManager = ConfigurationManager.GetInstance();
                Console.WriteLine($"Настройка 'AppName': {configManager.GetSetting("AppName")}");
            });
        }

        Task.WaitAll(tasks);

        // Изменение и сохранение настроек
        manager.SetSetting("AppName", "NewApp");
        manager.SaveSettings("appsettings.txt");
    }
}
