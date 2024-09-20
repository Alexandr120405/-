public interface IDelivery {
    void deliverOrder(Order order);
}

public class CourierDelivery implements IDelivery {
    @Override
    public void deliverOrder(Order order) {
    }
}

public class PostDelivery implements IDelivery {
    @Override
    public void deliverOrder(Order order) {
    }
}

public class PickUpPointDelivery implements IDelivery {
    @Override
    public void deliverOrder(Order order) {
    }
}
