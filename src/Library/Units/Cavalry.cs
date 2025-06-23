using Library.Buildings;
namespace Library.Units
{
    public class Cavalry : Unit
    {

        public Cavalry(Building building) : base(building)
        {
            Speed = 3;
            Attack = 8;
            Defense = 3;
            TimeTraining = 40;
            Cost = 70;
        }
    }
}