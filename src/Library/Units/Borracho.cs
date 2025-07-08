using Library.Buildings;
using Library.Core;

namespace Library.Units;

// Esta clase representa una unidad de tipo Borracho en el juego, que hereda de la clase base Unit.
// Un Borracho tiene propiedades específicas como velocidad (menor), ataque, defensa, tiempo de entrenamiento y costo.

public class Borracho : Unit
{
    public Borracho(Player player,(int x,int y) position) : base(player,position)
    {
        Speed = 5;
        Attack = 5;
        Defense = 1;
        TimeTraining = 90;
        Cost = 90;
        Life = 100;
    }
}