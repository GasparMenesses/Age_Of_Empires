namespace Library.Farming;
using System.Timers;

/// <summary>
/// Representa una granja en el juego, que es un tipo de recolectable.
/// La granja tiene una posición en el mapa, una cantidad inicial de recursos y un tiempo de recolección específico.
/// </summary>
public class Farm : Recolection
{
    /// <summary>
    /// Símbolo que representa la granja en el mapa.
    /// </summary>
    public override string Symbol => "🌾🌾";

    /// <summary>
    /// Constructor que inicializa la granja con una posición y cantidad inicial de recursos.
    /// </summary>
    /// <param name="posicion">Posición en el mapa (x, y).</param>
    /// <param name="cantidadinicial">Cantidad inicial de recursos disponibles en la granja.</param>
    public Farm((int x, int y) posicion, int cantidadinicial)
        : base(cantidadinicial, 120)
    {
    }
}