//1. Фасад: Управление отелем
//Подсистемы
// Подсистема бронирования номеров
public class RoomBookingSystem
{
    public void BookRoom(string roomType) => Console.WriteLine($"Room {roomType} booked.");
    public void CancelBooking(string roomType) => Console.WriteLine($"Room {roomType} booking canceled.");
    public bool CheckAvailability(string roomType) => true;
}

// Подсистема ресторана
public class RestaurantSystem
{
    public void ReserveTable(int numberOfSeats) => Console.WriteLine($"Table for {numberOfSeats} reserved.");
    public void OrderFood(string foodItem) => Console.WriteLine($"{foodItem} ordered.");
}

// Подсистема управления мероприятиями
public class EventManagementSystem
{
    public void BookEventHall(string hallType) => Console.WriteLine($"{hallType} hall booked.");
    public void OrderEquipment(string equipment) => Console.WriteLine($"{equipment} ordered for event.");
}

// Служба уборки
public class CleaningService
{
    public void ScheduleCleaning(int roomNumber) => Console.WriteLine($"Cleaning scheduled for room {roomNumber}.");
    public void PerformCleaning(int roomNumber) => Console.WriteLine($"Cleaning performed for room {roomNumber}.");
}
//Фасад для отеля
// Фасад для отеля
public class HotelFacade
{
    private RoomBookingSystem roomBookingSystem;
    private RestaurantSystem restaurantSystem;
    private EventManagementSystem eventManagementSystem;
    private CleaningService cleaningService;

    public HotelFacade()
    {
        roomBookingSystem = new RoomBookingSystem();
        restaurantSystem = new RestaurantSystem();
        eventManagementSystem = new EventManagementSystem();
        cleaningService = new CleaningService();
    }

    public void BookRoomWithServices(string roomType, string foodItem)
    {
        roomBookingSystem.BookRoom(roomType);
        restaurantSystem.OrderFood(foodItem);
        cleaningService.ScheduleCleaning(1);
        Console.WriteLine("Room booked with food and cleaning services.");
    }

    public void OrganizeEventWithRoomBooking(string hallType, string equipment, string roomType)
    {
        eventManagementSystem.BookEventHall(hallType);
        eventManagementSystem.OrderEquipment(equipment);
        roomBookingSystem.BookRoom(roomType);
        Console.WriteLine("Event organized with room booking and equipment.");
    }

    public void ReserveRestaurantWithTaxi(int numberOfSeats)
    {
        restaurantSystem.ReserveTable(numberOfSeats);
        Console.WriteLine("Table reserved with taxi booking.");
    }
}
//Клиентский код
public class Client
{
    public static void Main(string[] args)
    {
        HotelFacade hotel = new HotelFacade();

        hotel.BookRoomWithServices("Deluxe", "Pasta");
        hotel.OrganizeEventWithRoomBooking("Conference", "Projector", "Executive");
        hotel.ReserveRestaurantWithTaxi(4);
    }
}
//2. Компоновщик: Корпоративная иерархия
//Базовый абстрактный класс
public abstract class OrganizationComponent
{
    public string Name { get; set; }

    public OrganizationComponent(string name) => Name = name;

    public abstract decimal GetBudget();
    public abstract int GetEmployeeCount();

    public virtual void Add(OrganizationComponent component) { }
    public virtual void Remove(OrganizationComponent component) { }
    public virtual void Display(int depth = 0)
    {
        Console.WriteLine(new String('-', depth) + Name);
    }
}
//Класс сотрудника
public class Employee : OrganizationComponent
{
    public string Position { get; set; }
    public decimal Salary { get; set; }

    public Employee(string name, string position, decimal salary)
        : base(name)
    {
        Position = position;
        Salary = salary;
    }

    public override decimal GetBudget() => Salary;
    public override int GetEmployeeCount() => 1;

    public override void Display(int depth = 0)
    {
        base.Display(depth);
        Console.WriteLine(new String(' ', depth + 2) + $"Position: {Position}, Salary: {Salary}");
    }
}
//Класс отдела
public class Department : OrganizationComponent
{
    private List<OrganizationComponent> components = new List<OrganizationComponent>();

    public Department(string name) : base(name) { }

    public override void Add(OrganizationComponent component) => components.Add(component);
    public override void Remove(OrganizationComponent component) => components.Remove(component);

    public override decimal GetBudget()
    {
        decimal budget = 0;
        foreach (var component in components)
        {
            budget += component.GetBudget();
        }
        return budget;
    }

    public override int GetEmployeeCount()
    {
        int count = 0;
        foreach (var component in components)
        {
            count += component.GetEmployeeCount();
        }
        return count;
    }

    public override void Display(int depth = 0)
    {
        base.Display(depth);
        foreach (var component in components)
        {
            component.Display(depth + 2);
        }
    }
}
//Клиентский код
public class Client
{
    public static void Main(string[] args)
    {
        Department rootDepartment = new Department("Head Office");

        Employee employee1 = new Employee("Alice", "Manager", 70000);
        Employee employee2 = new Employee("Bob", "Developer", 50000);
        rootDepartment.Add(employee1);
        rootDepartment.Add(employee2);

        Department subDepartment = new Department("IT Department");
        Employee employee3 = new Employee("Charlie", "Tech Lead", 90000);
        Employee employee4 = new Employee("Dave", "Developer", 60000);
        subDepartment.Add(employee3);
        subDepartment.Add(employee4);

        rootDepartment.Add(subDepartment);

        Console.WriteLine("Organization Structure:");
        rootDepartment.Display();

        Console.WriteLine($"\nTotal Budget: {rootDepartment.GetBudget()}");
        Console.WriteLine($"Total Employee Count: {rootDepartment.GetEmployeeCount()}");
    }
}
