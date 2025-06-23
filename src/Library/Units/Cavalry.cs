using Library.Buildings;
namespace Library.Units
{
    public class Cavalry : Unit
    {

        public Cavalry(Building building) : base(building)
        {
            Speed = 80;
            Attack = 45;
            Defense = 30;
            TimeTraining = 50;
            Cost = 100;
        }
    }
}