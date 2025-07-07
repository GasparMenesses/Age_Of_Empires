namespace Library.Farming;

// Esta clase representa una cantera en el juego, que es un tipo de recolectable.
// La cantera tiene una posición en el mapa, una cantidad inicial de recursos y un tiempo de recolección específico.

public class Quarry: Recolection
{
    public override string Symbol => "⛏️🪨";
    

    public Quarry((int x, int y) posicion, int cantidadinicial)
        : base(cantidadinicial,120) 
    {
    }
}