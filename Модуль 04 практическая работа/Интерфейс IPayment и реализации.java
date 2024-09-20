public interface IPayment {
    void processPayment(double amount);
}

public class CreditCardPayment implements IPayment {
    @Override
    public void processPayment(double amount) {
    }
}

public class PayPalPayment implements IPayment {
    @Override
    public void processPayment(double amount) {
    }
}

public class BankTransferPayment implements IPayment {
    @Override
    public void processPayment(double amount) {
    }
}
