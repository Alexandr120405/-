import java.util.ArrayList;
import java.util.HashMap;
import java.util.Map;

abstract class Employee {
    String name;
    int id;
    String position;

    Employee(String name, int id, String position) {
        this.name = name;
        this.id = id;
        this.position = position;
    }

    abstract double calculateSalary();
}

class Worker extends Employee {
    double hourlyRate;
    int hoursWorked;

    Worker(String name, int id, double hourlyRate, int hoursWorked) {
        super(name, id, "Рабочий");
        this.hourlyRate = hourlyRate;
        this.hoursWorked = hoursWorked;
    }

    double calculateSalary() {
        return hourlyRate * hoursWorked;
    }
}

class Manager extends Employee {
    double salary;
    double bonus;

    Manager(String name, int id, double salary, double bonus) {
        super(name, id, "Менеджер");
        this.salary = salary;
        this.bonus = bonus;
    }

    double calculateSalary() {
        return salary + bonus;
    }
}

public class Main {
    public static void main(String[] args) {
        Employee worker = new Worker("Санек", 101, 500, 40);
        Employee manager = new Manager("Александр", 102, 50000, 10000);

        System.out.println(worker.name + ": " + worker.calculateSalary() + " тг.");
        System.out.println(manager.name + ": " + manager.calculateSalary() + " тг.");
    }
}
