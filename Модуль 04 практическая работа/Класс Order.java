import java.util.ArrayList;
import java.util.List;

public class Order {
    private List<Item> items = new ArrayList<>();
    private IPayment payment;
    private IDelivery delivery;

    public void addItem(Item item) {
        items.add(item);
    }

    public double calculateTotal(DiscountCalculator discountCalculator) {
        double total = items.stream().mapToDouble(item -> item.getPrice() * item.getQuantity()).sum();
        total -= discountCalculator.calculateDiscount(total);
        return total;
    }

    public void setPayment(IPayment payment) {
        this.payment = payment;
    }

    public void setDelivery(IDelivery delivery) {
        this.delivery = delivery;
    }
}
