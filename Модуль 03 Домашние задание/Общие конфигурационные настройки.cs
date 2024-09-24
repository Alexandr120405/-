public class Configuration
{
    public static string ConnectionString => "Server=myServer;Database=myDb;User Id=myUser;Password=myPass;";
}

public class DatabaseService
{
    public void Connect()
    {
        string connectionString = Configuration.ConnectionString;
    }
}

public class LoggingService
{
    public void Log(string message)
    {
        string connectionString = Configuration.ConnectionString;
    }
}
