import java.util.Arrays;

public class Main {
    public static void main(String[] args) {
        Order order = new Order();
        order.addItem(new Item("Товар 1", 100, 2));
        order.addItem(new Item("Товар 2", 200, 1));

        order.setPayment(new CreditCardPayment());
        order.setDelivery(new CourierDelivery());

        DiscountCalculator discountCalculator = new DiscountCalculator(Arrays.asList(
            new PercentageDiscount(10) 
        ));

        double total = order.calculateTotal(discountCalculator);
        order.getPayment().processPayment(total);

        INotification notification = new EmailNotification();
        notification.sendNotification("Ваш заказ успешно оформлен!");
    }
}
