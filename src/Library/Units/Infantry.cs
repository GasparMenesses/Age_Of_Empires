using Library.Buildings;
namespace Library.Units;

public class Infantry : Unit
{
    public Infantry(Building building) : base(building)
    {
        Speed = 40;
        Attack = 23;
        Defense = 45;
        TimeTraining = 43;
        Cost = 100;
    }
}