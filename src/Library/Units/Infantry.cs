using Library.Buildings;
namespace Library.Units;

public class Infantry : Unit
{
    public Infantry(Building building) : base(building)
    {
        Speed = 1;
        Attack = 3;
        Defense = 0;
        TimeTraining = 26;
        Cost = 50;
    }
}