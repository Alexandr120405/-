public interface INotification {
    void sendNotification(String message);
}

public class EmailNotification implements INotification {
    @Override
    public void sendNotification(String message) {
    }
}

public class SmsNotification implements INotification {
    @Override
    public void sendNotification(String message) {
    }
}
