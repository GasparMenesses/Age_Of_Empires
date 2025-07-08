using Library.Buildings;
using Library.Core;

namespace Library.Units;

// Esta clase representa una unidad de tipo arquero en el juego, que hereda de la clase base Unit.
// Un arquero tiene propiedades específicas como velocidad, ataque, defensa, tiempo de entrenamiento y costo.

public class Archer : Unit
{
    public int Speed { get; set; }
    public int Attack { get; set; }
    public int Defense { get; set; }
    public int TimeTraining { get; set; }

    public Archer(Player player,(int x,int y) position) : base(player,position)
    {
        Speed = 1;
        Attack = 3;
        Defense = 0;
        TimeTraining = 35;
        Cost = 40;
        Life = 100;
    }
}