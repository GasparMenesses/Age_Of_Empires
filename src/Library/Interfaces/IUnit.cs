namespace Library.Interfaces;

public interface IUnit
{
    public int TimeTraining { get; }
    public string Type { get; }
    public Resources Cost { get; }
    public int Speed { get; }
    public int Attack { get; }
    public int Defense { get; }
}