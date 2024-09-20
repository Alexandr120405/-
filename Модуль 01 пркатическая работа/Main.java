import java.util.ArrayList;
import java.util.List;

class Vehicle {
    String марка, модель;
    int год;

    public Vehicle(String марка, String модель, int год) {
        this.марка = марка;
        this.модель = модель;
        this.год = год;
    }

    public void запуск() {
        System.out.println(марка + " " + модель + " запущен.");
    }

    public void остановка() {
        System.out.println(марка + " " + модель + " остановлен.");
    }

    @Override
    public String toString() {
        return год + " " + марка + " " + модель;
    }
}

class Car extends Vehicle {
    int двери;
    String трансмиссия;

    public Car(String марка, String модель, int год, int двери, String трансмиссия) {
        super(марка, модель, год);
        this.двери = двери;
        this.трансмиссия = трансмиссия;
    }
}

class Motorcycle extends Vehicle {
    String типКузова;
    boolean естьБокс;

    public Motorcycle(String марка, String модель, int год, String типКузова, boolean естьБокс) {
        super(марка, модель, год);
        this.типКузова = типКузова;
        this.естьБокс = естьБокс;
    }
}

class Garage {
    List<Vehicle> транспорт = new ArrayList<>();

    public void добавитьТС(Vehicle тс) {
        транспорт.add(тс);
    }

    public void удалитьТС(Vehicle тс) {
        транспорт.remove(тс);
    }
}

class Fleet {
    List<Garage> гаражи = new ArrayList<>();

    public void добавитьГараж(Garage гараж) {
        гаражи.add(гараж);
    }

    public void удалитьГараж(Garage гараж) {
        гаражи.remove(гараж);
    }

    public Vehicle поискТС(String марка) {
        for (Garage г : гаражи) {
            for (Vehicle тс : г.транспорт) {
                if (тс.марка.equals(марка)) {
                    return тс;
                }
            }
        }
        return null;
    }
}

public class Main {
    public static void main(String[] args) {
        Car авто = new Car("BMW", "e34", 1995, 4, "механика");
        Motorcycle мото = new Motorcycle("cawasaki", "sport", 2024, "Спортбайк", true);

        Garage гараж = new Garage();
        гараж.добавитьТС(авто);
        гараж.добавитьТС(мото);

        Fleet автопарк = new Fleet();
        автопарк.добавитьГараж(гараж);

        System.out.println("Найдено ТС: " + автопарк.поискТС("BMW"));
    }
}
