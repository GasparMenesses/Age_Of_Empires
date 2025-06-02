namespace Library.Interfaces;
using Library;
public interface IUnit
{
    public int TimeTraining { get; }
    public string Type { get; }
    public Resources Cost { get; }
    public List<int> Position { get; set; }
    public int Speed { get; }
    public int Attack { get; }
    public int Defense { get; }
}