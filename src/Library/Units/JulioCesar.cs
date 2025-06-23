using Library.Buildings;

namespace Library.Units;

public class JulioCesar : Unit
{
    public JulioCesar(Building building) : base(building)
    {
        Speed = 2;
        Attack = 9;
        Defense = 2;
        TimeTraining = 120;
        Cost = 100;
    }
}