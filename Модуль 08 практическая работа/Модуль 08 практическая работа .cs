//Реализация паттерна Команда (Command) для системы "Умный дом"
//Интерфейс ICommand
public interface ICommand
{
    void Execute();
    void Undo();
}
//Класс Light — устройство "Свет"
public class Light
{
    public void On() => Console.WriteLine("Свет включен.");
    public void Off() => Console.WriteLine("Свет выключен.");
}
//Класс AirConditioner — устройство "Кондиционер"
public class AirConditioner
{
    public void TurnOn() => Console.WriteLine("Кондиционер включен.");
    public void TurnOff() => Console.WriteLine("Кондиционер выключен.");
}
//Класс TV — устройство "Телевизор"
public class TV
{
    public void TurnOn() => Console.WriteLine("Телевизор включен.");
    public void TurnOff() => Console.WriteLine("Телевизор выключен.");
}
//Команды для управления светом
public class LightOnCommand : ICommand
{
    private readonly Light _light;

    public LightOnCommand(Light light) => _light = light;

    public void Execute() => _light.On();
    public void Undo() => _light.Off();
}

public class LightOffCommand : ICommand
{
    private readonly Light _light;

    public LightOffCommand(Light light) => _light = light;

    public void Execute() => _light.Off();
    public void Undo() => _light.On();
}
//Команды для управления кондиционером
public class AirConditionerOnCommand : ICommand
{
    private readonly AirConditioner _ac;

    public AirConditionerOnCommand(AirConditioner ac) => _ac = ac;

    public void Execute() => _ac.TurnOn();
    public void Undo() => _ac.TurnOff();
}

public class AirConditionerOffCommand : ICommand
{
    private readonly AirConditioner _ac;

    public AirConditionerOffCommand(AirConditioner ac) => _ac = ac;

    public void Execute() => _ac.TurnOff();
    public void Undo() => _ac.TurnOn();
}
//Команды для управления телевизором
public class TVOnCommand : ICommand
{
    private readonly TV _tv;

    public TVOnCommand(TV tv) => _tv = tv;

    public void Execute() => _tv.TurnOn();
    public void Undo() => _tv.TurnOff();
}

public class TVOffCommand : ICommand
{
    private readonly TV _tv;

    public TVOffCommand(TV tv) => _tv = tv;

    public void Execute() => _tv.TurnOff();
    public void Undo() => _tv.TurnOn();
}
//Класс RemoteControl
public class RemoteControl
{
    private readonly ICommand[] _onCommands;
    private readonly ICommand[] _offCommands;
    private ICommand _lastCommand;

    public RemoteControl()
    {
        _onCommands = new ICommand[5];
        _offCommands = new ICommand[5];
    }

    public void SetCommand(int slot, ICommand onCommand, ICommand offCommand)
    {
        _onCommands[slot] = onCommand;
        _offCommands[slot] = offCommand;
    }

    public void OnButtonPressed(int slot)
    {
        _onCommands[slot]?.Execute();
        _lastCommand = _onCommands[slot];
    }

    public void OffButtonPressed(int slot)
    {
        _offCommands[slot]?.Execute();
        _lastCommand = _offCommands[slot];
    }

    public void UndoButtonPressed() => _lastCommand?.Undo();
}
//Макрокоманда
public class MacroCommand : ICommand
{
    private readonly ICommand[] _commands;

    public MacroCommand(ICommand[] commands) => _commands = commands;

    public void Execute()
    {
        foreach (var command in _commands)
        {
            command.Execute();
        }
    }

    public void Undo()
    {
        foreach (var command in _commands)
        {
            command.Undo();
        }
    }
}
//Клиентский код
public class Program
{
    public static void Main()
    {
        var remote = new RemoteControl();

        var light = new Light();
        var ac = new AirConditioner();
        var tv = new TV();

        var lightOn = new LightOnCommand(light);
        var lightOff = new LightOffCommand(light);
        var acOn = new AirConditionerOnCommand(ac);
        var acOff = new AirConditionerOffCommand(ac);
        var tvOn = new TVOnCommand(tv);
        var tvOff = new TVOffCommand(tv);

        remote.SetCommand(0, lightOn, lightOff);
        remote.SetCommand(1, acOn, acOff);
        remote.SetCommand(2, tvOn, tvOff);

        // Тестирование обычных команд
        remote.OnButtonPressed(0);
        remote.OffButtonPressed(0);
        remote.UndoButtonPressed();

        // Тестирование макрокоманды
        var macroCommands = new ICommand[] { lightOn, acOn, tvOn };
        var macro = new MacroCommand(macroCommands);

        remote.SetCommand(3, macro, new MacroCommand(new ICommand[] { lightOff, acOff, tvOff }));
        remote.OnButtonPressed(3);
        remote.UndoButtonPressed();
    }
}
//Обработка ошибок
public void OnButtonPressed(int slot)
{
    if (_onCommands[slot] != null)
    {
        _onCommands[slot].Execute();
        _lastCommand = _onCommands[slot];
    }
    else
    {
        Console.WriteLine("Команда не назначена.");
    }
}
//