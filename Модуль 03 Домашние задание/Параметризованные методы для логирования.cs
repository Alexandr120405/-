public enum LogLevel
{
    Error,
    Warning,
    Info
}

public void Log(LogLevel level, string message)
{
    Console.WriteLine($"{level.ToString().ToUpper()}: {message}");
}
