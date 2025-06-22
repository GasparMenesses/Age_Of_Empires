using Library.Buildings;
namespace Library.Units;
public class Cavalry : Unit
{
    public int Speed { get; set; }
    public int Attack { get; set; }
    public int Defense { get; set; }
    public int CreationTime { get; set; }
    public Cavalry(Building building) : base(building)
    {
        Speed = 40;
        Attack = 23;
        Defense = 45;
        TimeTraining = 43;
        Cost = 100;
    }
}