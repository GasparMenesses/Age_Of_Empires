namespace Library.Units;
using Interfaces;
using Buildings;


public class Unit : IUnit
{
    public int TimeTraining { get; set; }
    public int Cost { get; set; }
    public Dictionary<string, int> Position { get; set; }
    public int Speed { get; set; }
    public int Attack { get; set; }
    public int Defense { get; set; }

    public Unit(Building building)
    {
        Position = new Dictionary<string, int>
        {
            { "x", building.Position["x"] },
            { "y", building.Position["y"] },
        };
    }
}