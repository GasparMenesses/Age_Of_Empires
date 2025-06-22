namespace Library.Units;
using Interfaces;


public class Unit : IUnit
{
    public int TimeTraining { get; }
    public string Type { get; }
    public Resources Cost { get; }
    public List<int> Position { get; set; }
}