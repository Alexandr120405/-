//Часть 1. Фасад: Система управления мультимедиа
//Шаг 1: Реализация классов подсистем
public class TV
{
    public void On() => Console.WriteLine("TV is On");
    public void Off() => Console.WriteLine("TV is Off");
    public void SetChannel(int channel) => Console.WriteLine($"TV channel set to {channel}");
}

public class AudioSystem
{
    public void On() => Console.WriteLine("Audio System is On");
    public void Off() => Console.WriteLine("Audio System is Off");
    public void SetVolume(int volume) => Console.WriteLine($"Audio volume set to {volume}");
}

public class DVDPlayer
{
    public void Play() => Console.WriteLine("DVD is playing");
    public void Pause() => Console.WriteLine("DVD is paused");
    public void Stop() => Console.WriteLine("DVD is stopped");
}

public class GameConsole
{
    public void On() => Console.WriteLine("Game Console is On");
    public void StartGame() => Console.WriteLine("Game has started on Console");
}
//Шаг 2: Создание класса HomeTheaterFacade
public class HomeTheaterFacade
{
    private readonly TV _tv;
    private readonly AudioSystem _audioSystem;
    private readonly DVDPlayer _dvdPlayer;
    private readonly GameConsole _gameConsole;

    public HomeTheaterFacade(TV tv, AudioSystem audioSystem, DVDPlayer dvdPlayer, GameConsole gameConsole)
    {
        _tv = tv;
        _audioSystem = audioSystem;
        _dvdPlayer = dvdPlayer;
        _gameConsole = gameConsole;
    }

    public void WatchMovie()
    {
        _tv.On();
        _audioSystem.On();
        _dvdPlayer.Play();
        Console.WriteLine("Ready to watch a movie!");
    }

    public void StopMovie()
    {
        _dvdPlayer.Stop();
        _audioSystem.Off();
        _tv.Off();
        Console.WriteLine("Movie stopped and system turned off");
    }

    public void PlayGame()
    {
        _tv.On();
        _audioSystem.On();
        _gameConsole.On();
        _gameConsole.StartGame();
        Console.WriteLine("Ready to play a game!");
    }

    public void ListenToMusic()
    {
        _tv.On();
        _audioSystem.On();
        _audioSystem.SetVolume(10);
        Console.WriteLine("Ready to listen to music!");
    }

    public void SetVolume(int volume)
    {
        _audioSystem.SetVolume(volume);
    }
}
//Шаг 3: Клиентский код
public class Client
{
    public static void Main(string[] args)
    {
        TV tv = new TV();
        AudioSystem audioSystem = new AudioSystem();
        DVDPlayer dvdPlayer = new DVDPlayer();
        GameConsole gameConsole = new GameConsole();

        HomeTheaterFacade homeTheater = new HomeTheaterFacade(tv, audioSystem, dvdPlayer, gameConsole);

        homeTheater.WatchMovie();
        homeTheater.SetVolume(15);
        homeTheater.StopMovie();

        homeTheater.PlayGame();
        homeTheater.ListenToMusic();
    }
}
//Часть 2. Компоновщик: Файловая система
//Шаг 1: Интерфейс FileSystemComponent
public abstract class FileSystemComponent
{
    public string Name { get; protected set; }
    
    public FileSystemComponent(string name)
    {
        Name = name;
    }
    
    public abstract void Display(int indent = 0);
    public abstract int GetSize();
}
//Шаг 2: Класс File
public class File : FileSystemComponent
{
    public int Size { get; private set; }
    
    public File(string name, int size) : base(name)
    {
        Size = size;
    }
    
    public override void Display(int indent = 0)
    {
        Console.WriteLine($"{new string(' ', indent)}File: {Name}, Size: {Size}KB");
    }
    
    public override int GetSize()
    {
        return Size;
    }
}
//Шаг 3: Класс Directory
public class Directory : FileSystemComponent
{
    private List<FileSystemComponent> _components = new List<FileSystemComponent>();
    
    public Directory(string name) : base(name) { }
    
    public void Add(FileSystemComponent component)
    {
        if (!_components.Contains(component))
        {
            _components.Add(component);
            Console.WriteLine($"Added {component.Name} to {Name}");
        }
    }
    
    public void Remove(FileSystemComponent component)
    {
        if (_components.Contains(component))
        {
            _components.Remove(component);
            Console.WriteLine($"Removed {component.Name} from {Name}");
        }
    }
    
    public override void Display(int indent = 0)
    {
        Console.WriteLine($"{new string(' ', indent)}Directory: {Name}");
        foreach (var component in _components)
        {
            component.Display(indent + 2);
        }
    }
    
    public override int GetSize()
    {
        int size = 0;
        foreach (var component in _components)
        {
            size += component.GetSize();
        }
        return size;
    }
}
//Шаг 4: Клиентский код
public class Client
{
    public static void Main(string[] args)
    {
        Directory root = new Directory("Root");
        
        File file1 = new File("File1.txt", 10);
        File file2 = new File("File2.txt", 20);
        
        Directory subDir = new Directory("SubDirectory");
        File file3 = new File("File3.txt", 30);
        
        root.Add(file1);
        root.Add(file2);
        
        subDir.Add(file3);
        root.Add(subDir);
        
        root.Display();
        Console.WriteLine($"Total Size: {root.GetSize()}KB");
    }
}
