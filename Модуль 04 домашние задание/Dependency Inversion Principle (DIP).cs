public interface INotificationSender
{
    void Send(string message);
}

public class EmailSender : INotificationSender
{
    public void Send(string message)
    {
        Console.WriteLine("Email sent: " + message);
    }
}

public class SmsSender : INotificationSender
{
    public void Send(string message)
    {
        Console.WriteLine("SMS sent: " + message);
    }
}

public class NotificationService
{
    private readonly List<INotificationSender> _notificationSenders;

    public NotificationService(List<INotificationSender> notificationSenders)
    {
        _notificationSenders = notificationSenders;
    }

    public void SendNotification(string message)
    {
        foreach (var sender in _notificationSenders)
        {
            sender.Send(message);
        }
    }
}
