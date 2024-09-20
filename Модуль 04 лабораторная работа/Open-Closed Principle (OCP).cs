public interface IDiscount
{
    double ApplyDiscount(double amount);
}

public class RegularDiscount : IDiscount
{
    public double ApplyDiscount(double amount) => amount;
}

public class SilverDiscount : IDiscount
{
    public double ApplyDiscount(double amount) => amount * 0.9; // 10% скидка
}

public class GoldDiscount : IDiscount
{
    public double ApplyDiscount(double amount) => amount * 0.8; // 20% скидка
}

public class DiscountCalculator
{
    public double CalculateDiscount(IDiscount discountStrategy, double amount)
    {
        return discountStrategy.ApplyDiscount(amount);
    }
}
