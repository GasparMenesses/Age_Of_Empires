using Library.Buildings;
namespace Library.Units
// Esta clase representa una unidad de tipo Caballería en el juego, que hereda de la clase base Unit.
// Un Caballero tiene propiedades específicas como velocidad, ataque, defensa, tiempo de entrenamiento y costo.
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