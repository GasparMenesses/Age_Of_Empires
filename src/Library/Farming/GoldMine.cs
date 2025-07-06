namespace Library.Farming;

// Esta clase representa una mina de oro en el juego, que es un tipo de recolectable.
// La mina de oro tiene una posición en el mapa, una cantidad inicial de recursos y un tiempo de recolección específico.

public class GoldMine: Recolection
{
    public static string Symbol => "⛏️💰";

    public Dictionary<string, int> Position { get; set; }

    public GoldMine((int x, int y) posicion, int cantidadinicial)
        : base(posicion, cantidadinicial,120) 
    {
        
    }
}
