using Library.Buildings;

namespace Library.Units
{
    public class Villager : Unit
    {
        public Villager(Building building) : base(building)
        {
            Speed = 1;
            Attack = 1;
            Defense = 0;
            TimeTraining = 20;
            Cost = 50;
        }
    }
}