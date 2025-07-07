using Library.Buildings;
using Library.Core;

namespace Library.Units;
// Esta clase representa una unidad de tipo Infantería en el juego, que hereda de la clase base Unit.
// Infantería tiene propiedades específicas como velocidad, ataque, defensa, tiempo de entrenamiento y costo.
public class Infantry : Unit
{
    public Infantry(Player player,Building building) : base(player,building)
    {
        Speed = 1;
        Attack = 3;
        Defense = 0;
        TimeTraining = 26;
        Cost = 50;
        Life = 100;
    }
}