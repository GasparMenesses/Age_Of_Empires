using Library.Buildings;
using Library.Core;

// Esta clase representa una unidad de tipo Villager en el juego, que hereda de la clase base Unit.
// Un Villager tiene propiedades específicas como velocidad, ataque, defensa, tiempo de entrenamiento y costo.

namespace Library.Units
{
    public class Villager : Unit
    {
        public Villager(Player player,(int x,int y) position) : base(player,position)
        {
            Speed = 1;
            Attack = 1;
            Defense = 0;
            TimeTraining = 20;
            Cost = 50;
            Life = 100;
        }
    }
}