using Library.Buildings;

namespace Library.Units;

public class Borracho : Unit
{
    public Borracho(Building building) : base(building)
    {
        Speed = 5;
        Attack = 5;
        Defense = 1;
        TimeTraining = 90;
        Cost = 90;
    }
}