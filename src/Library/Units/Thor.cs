using Library.Buildings;

namespace Library.Units;

public class Thor : Unit
{
    public Thor(Building building) : base(building)
    {
        Speed = 1;
        Attack = 11;
        Defense = 5;
        TimeTraining = 150;
        Cost = 150;
    }
}