namespace Library.Units;
using Interfaces;
using Buildings;
using Core;

// Esta clase representa una unidad genérica en el juego, que implementa la interfaz IUnit.
// La clase contiene propiedades comunes a todas las unidades, como tiempo de entrenamiento, costo, posición, velocidad, ataque y defensa.
// La posición se define como un diccionario con coordenadas "x" e "y".
public class Unit : IUnit
{
    public int TimeTraining { get; set; }
    public int Cost { get; set; }
    public Dictionary<string, int> Position { get; set; }
    public int Speed { get; set; }
    public int Attack { get; set; }
    public int Defense { get; set; }
    public int Life { get; set; }

    public Unit(Player player,Building building)
    {
        Position = new Dictionary<string, int>
        {
            { "x", player.Buildings[building].x },
            { "y", player.Buildings[building].y },
        };
    }
}