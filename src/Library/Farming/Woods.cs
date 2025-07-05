namespace Library.Farming;

// Esta clase representa un bosque en el juego, que es un tipo de recolectable.
// El bosque tiene una posición en el mapa, una cantidad inicial de recursos y un tiempo de recolección específico.

public class Woods: Recolection
{
    public static string Symbol => "Wd";
    

    public Woods((int x, int y) posicion, int cantidadinicial)
        : base(posicion, cantidadinicial,120) 
    {
        
    }
}
