import java.util.List;

public class DiscountCalculator {
    private List<IDiscountRule> discountRules;

    public DiscountCalculator(List<IDiscountRule> discountRules) {
        this.discountRules = discountRules;
    }

    public double calculateDiscount(double total) {
        double discount = 0;
        for (IDiscountRule rule : discountRules) {
            discount += rule.apply(total);
        }
        return discount;
    }
}

public interface IDiscountRule {
    double apply(double total);
}

public class PercentageDiscount implements IDiscountRule {
    private final double percentage;

    public PercentageDiscount(double percentage) {
        this.percentage = percentage;
    }

    @Override
    public double apply(double total) {
        return total * (percentage / 100);
    }
}
