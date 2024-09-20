public interface IWorker
{
    void Work();
}

public interface IEater
{
    void Eat();
}

public interface ISleeper
{
    void Sleep();
}

public class HumanWorker : IWorker, IEater, ISleeper
{
    public void Work() { /* Логика работы */ }
    public void Eat() { /* Логика питания */ }
    public void Sleep() { /* Логика сна */ }
}

public class RobotWorker : IWorker
{
    public void Work() { /* Логика работы */ }
}
