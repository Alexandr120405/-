//Интерфейс ICommand
public interface ICommand
{
    void Execute();
    void Undo();
}
//Классы устройств (Light, Door, Thermostat)
public class Light
{
    public void On() => Console.WriteLine("Light is ON");
    public void Off() => Console.WriteLine("Light is OFF");
}

public class Door
{
    public void Open() => Console.WriteLine("Door is OPEN");
    public void Close() => Console.WriteLine("Door is CLOSED");
}

public class Thermostat
{
    public void IncreaseTemperature() => Console.WriteLine("Temperature is INCREASED");
    public void DecreaseTemperature() => Console.WriteLine("Temperature is DECREASED");
}
//Реализация конкретных команд
public class LightOnCommand : ICommand
{
    private Light _light;

    public LightOnCommand(Light light) => _light = light;

    public void Execute() => _light.On();
    public void Undo() => _light.Off();
}

public class LightOffCommand : ICommand
{
    private Light _light;

    public LightOffCommand(Light light) => _light = light;

    public void Execute() => _light.Off();
    public void Undo() => _light.On();
}

public class DoorOpenCommand : ICommand
{
    private Door _door;

    public DoorOpenCommand(Door door) => _door = door;

    public void Execute() => _door.Open();
    public void Undo() => _door.Close();
}

public class DoorCloseCommand : ICommand
{
    private Door _door;

    public DoorCloseCommand(Door door) => _door = door;

    public void Execute() => _door.Close();
    public void Undo() => _door.Open();
}

public class IncreaseTemperatureCommand : ICommand
{
    private Thermostat _thermostat;

    public IncreaseTemperatureCommand(Thermostat thermostat) => _thermostat = thermostat;

    public void Execute() => _thermostat.IncreaseTemperature();
    public void Undo() => _thermostat.DecreaseTemperature();
}

public class DecreaseTemperatureCommand : ICommand
{
    private Thermostat _thermostat;

    public DecreaseTemperatureCommand(Thermostat thermostat) => _thermostat = thermostat;

    public void Execute() => _thermostat.DecreaseTemperature();
    public void Undo() => _thermostat.IncreaseTemperature();
}
//Класс Invoker
public class SmartHomeController
{
    private ICommand _lastCommand;

    public void ExecuteCommand(ICommand command)
    {
        command.Execute();
        _lastCommand = command;
    }

    public void UndoCommand()
    {
        if (_lastCommand != null)
        {
            _lastCommand.Undo();
        }
    }
}
//Клиентский код
class Program
{
    static void Main()
    {
        SmartHomeController controller = new SmartHomeController();

        Light light = new Light();
        Door door = new Door();
        Thermostat thermostat = new Thermostat();

        ICommand lightOn = new LightOnCommand(light);
        ICommand lightOff = new LightOffCommand(light);
        ICommand doorOpen = new DoorOpenCommand(door);
        ICommand doorClose = new DoorCloseCommand(door);
        ICommand increaseTemp = new IncreaseTemperatureCommand(thermostat);
        ICommand decreaseTemp = new DecreaseTemperatureCommand(thermostat);

        controller.ExecuteCommand(lightOn);
        controller.ExecuteCommand(doorOpen);
        controller.ExecuteCommand(increaseTemp);
        
        controller.UndoCommand();
        controller.UndoCommand();
    }
}
//Абстрактный класс Beverage
public abstract class Beverage
{
    public void PrepareRecipe()
    {
        BoilWater();
        Brew();
        PourInCup();
        if (CustomerWantsCondiments())
        {
            AddCondiments();
        }
    }

    protected abstract void Brew();
    protected abstract void AddCondiments();

    private void BoilWater() => Console.WriteLine("Boiling water");
    private void PourInCup() => Console.WriteLine("Pouring into cup");

    protected virtual bool CustomerWantsCondiments() => true;
}
//Реализация класса Tea
public class Tea : Beverage
{
    protected override void Brew() => Console.WriteLine("Steeping the tea");
    protected override void AddCondiments() => Console.WriteLine("Adding lemon");
}
//Реализация класса Coffee
public class Coffee : Beverage
{
    protected override void Brew() => Console.WriteLine("Dripping coffee through filter");
    protected override void AddCondiments() => Console.WriteLine("Adding sugar and milk");

    protected override bool CustomerWantsCondiments()
    {
        Console.WriteLine("Would you like sugar and milk with your coffee (y/n)?");
        string answer = Console.ReadLine();
        return answer.ToLower() == "y";
    }
}
//Клиентский код
class Program
{
    static void Main()
    {
        Tea tea = new Tea();
        Coffee coffee = new Coffee();

        Console.WriteLine("Making tea...");
        tea.PrepareRecipe();

        Console.WriteLine("\nMaking coffee...");
        coffee.PrepareRecipe();
    }
}
//Интерфейс IMediator
public interface IMediator
{
    void SendMessage(string message, User user);
    void AddUser(User user);
}
//Класс ChatRoom
public class ChatRoom : IMediator
{
    private List<User> _users = new List<User>();

    public void AddUser(User user) => _users.Add(user);

    public void SendMessage(string message, User sender)
    {
        foreach (var user in _users)
        {
            if (user != sender)
            {
                user.ReceiveMessage(message);
            }
        }
    }
}
//Класс User
public abstract class User
{
    protected IMediator _mediator;
    public string Name { get; }

    public User(IMediator mediator, string name)
    {
        _mediator = mediator;
        Name = name;
    }

    public abstract void ReceiveMessage(string message);
    public void SendMessage(string message) => _mediator.SendMessage(message, this);
}
//Конкретный пользователь
public class BasicUser : User
{
    public BasicUser(IMediator mediator, string name) : base(mediator, name) {}

    public override void ReceiveMessage(string message) =>
        Console.WriteLine($"{Name} received: {message}");
}
//Клиентский код
class Program
{
    static void Main()
    {
        IMediator chatRoom = new ChatRoom();

        User user1 = new BasicUser(chatRoom, "Alice");
        User user2 = new BasicUser(chatRoom, "Bob");
        User user3 = new BasicUser(chatRoom, "Charlie");

        chatRoom.AddUser(user1);
        chatRoom.AddUser(user2);
        chatRoom.AddUser(user3);

        user1.SendMessage("Hello, everyone!");
    }
}
//