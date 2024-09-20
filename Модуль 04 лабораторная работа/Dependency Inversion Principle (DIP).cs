public interface INotificationService
{
    void Send(string message);
}

public class EmailService : INotificationService
{
    public void Send(string message)
    {
        Console.WriteLine($"Отправка Email: {message}");
    }
}

public class Notification
{
    private readonly INotificationService _notificationService;

    public Notification(INotificationService notificationService)
    {
        _notificationService = notificationService;
    }

    public void Send(string message)
    {
        _notificationService.Send(message);
    }
}
