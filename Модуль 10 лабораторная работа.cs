//task 1
using System;

public class AudioSystem
{
    public void TurnOn()
    {
        Console.WriteLine("Audio on");
    }

    public void TurnOff()
    {
        Console.WriteLine("Audio off");
    }

    public void SetVolume(float volume)
    {
        Console.WriteLine($"Audio volume is set to {volume}");
    }
}

public class VideoProjector
{
    public void TurnOn()
    {
        Console.WriteLine("Video on");
    }

    public void TurnOff()
    {
        Console.WriteLine("Video off");
    }

    public void SetResolution(string resolution)
    {
        Console.WriteLine($"Resolution is set to {resolution}");
    }
}

public class LightingSystem
{
    public void TurnOn()
    {
        Console.WriteLine("Lights on");
    }

    public void TurnOff()
    {
        Console.WriteLine("Lights off");
    }

    public void SetBrightness(int level)
    {
        Console.WriteLine($"Light brightness is set to {level}");
    }
}

public class HomeTheaterFacade
{
    private AudioSystem _audioSystem;
    private VideoProjector _videoProjector;
    private LightingSystem _lightingSystem;

    public HomeTheaterFacade(AudioSystem audioSystem, VideoProjector videoProjector, LightingSystem lightingSystem)
    {
        _audioSystem = audioSystem;
        _videoProjector = videoProjector;
        _lightingSystem = lightingSystem;
    }

    public void StartMovie()
    {
        Console.WriteLine("Loading...");
        _lightingSystem.TurnOn();
        _lightingSystem.SetBrightness(5);
        _videoProjector.TurnOn();
        _videoProjector.SetResolution("HD");
        _audioSystem.TurnOn();
        _audioSystem.SetVolume(8);
        Console.WriteLine("Movie started");
    }

    public void StopMovie()
    {
        Console.WriteLine("Pausing Movie...");
        _lightingSystem.TurnOn();
        _lightingSystem.SetBrightness(2);
        _videoProjector.TurnOn();
        _audioSystem.TurnOff();
        Console.WriteLine("Movie paused");
    }

    public void EndMovie()
    {
        Console.WriteLine("Shutting down movie...");
        _lightingSystem.TurnOff();
        _videoProjector.TurnOff();
        _audioSystem.TurnOff();
        Console.WriteLine("Movie ended");
    }
}

class Program
{
    static void Main(string[] args)
    {
        AudioSystem audio = new AudioSystem();
        VideoProjector video = new VideoProjector();
        LightingSystem lighting = new LightingSystem();

        HomeTheaterFacade homeTheater = new HomeTheaterFacade(audio, video, lighting);

        homeTheater.StartMovie();

        Console.WriteLine();

        homeTheater.EndMovie();
    }
}
//Task 2
using System;
using System.Collections.Generic;

public interface IMenuComponent
{
    void Display(int depth);
    void Add(IMenuComponent component);
    void Remove(IMenuComponent component);
}

public class MenuItem : IMenuComponent
{
    private string _name;

    public MenuItem(string name)
    {
        _name = name;
    }

    public void Display(int depth)
    {
        Console.WriteLine(new string(' ', depth) + _name);
    }

    public void Add(IMenuComponent component)
    {
        throw new NotSupportedException("Cannot add to a MenuItem.");
    }

    public void Remove(IMenuComponent component)
    {
        throw new NotSupportedException("Cannot remove from a MenuItem.");
    }
}

public class Menu : IMenuComponent
{
    private List<IMenuComponent> _components;
    private string _name;

    public Menu(string name)
    {
        _components = new List<IMenuComponent>();
        _name = name;
    }

    public void Display(int depth)
    {
        Console.WriteLine(new string('-', depth) + _name);
        foreach (var item in _components)
        {
            item.Display(depth + 2);
        }
    }

    public void Add(IMenuComponent component)
    {
        _components.Add(component);
    }

    public void Remove(IMenuComponent component)
    {
        _components.Remove(component);
    }
}

class Program
{
    static void Main(string[] args)
    {
        var pizza = new MenuItem("Pizza");
        var pasta = new MenuItem("Pasta");
        var salad = new MenuItem("Salad");

        var drinksMenu = new Menu("Drinks");
        var dessertMenu = new Menu("Dessert");

        drinksMenu.Add(new MenuItem("Fanta"));
        drinksMenu.Add(new MenuItem("Water"));
        drinksMenu.Add(new MenuItem("Vodka"));

        dessertMenu.Add(new MenuItem("Cake"));

        var mainMenu = new Menu("Menu");
        mainMenu.Add(pizza);
        mainMenu.Add(pasta);
        mainMenu.Add(salad);
        mainMenu.Add(drinksMenu);
        mainMenu.Add(dessertMenu);

        mainMenu.Display(1);
    }
}