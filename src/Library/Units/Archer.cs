using Library.Buildings;
namespace Library.Units;

public class Archer : Unit
{
    public int Speed { get; set; }
    public int Attack { get; set; }
    public int Defense { get; set; }
    public int TimeTraining { get; set; }

    public Archer(Building building) : base(building)
    {
        Speed = 40;
        Attack = 23;
        Defense = 45;
        TimeTraining = 43;
        Cost = 100;
    }
}