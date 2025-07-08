using Library.Buildings;
using Library.Core;

namespace Library.Units;

// Esta clase representa una unidad de tipo Julio César en el juego, que hereda de la clase base Unit.
// Julio César tiene propiedades específicas como velocidad, ataque, defensa, tiempo de entrenamiento y costo.

public class JulioCesar : Unit
{
    public JulioCesar(Player player,(int x,int y) position) : base(player,position)
    {
        Speed = 2;
        Attack = 9;
        Defense = 2;
        TimeTraining = 120;
        Cost = 100;
        Life = 100;
    }
}