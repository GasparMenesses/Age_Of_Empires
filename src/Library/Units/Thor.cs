using Library.Buildings;

namespace Library.Units;

// Esta clase representa una unidad de tipo Thor en el juego, que hereda de la clase base Unit.
// Thor tiene propiedades específicas como velocidad, ataque, defensa, tiempo de entrenamiento y costo.

public class Thor : Unit
{
    public Thor(Building building) : base(building)
    {
        Speed = 1;
        Attack = 11;
        Defense = 5;
        TimeTraining = 150;
        Cost = 150;
    }
}