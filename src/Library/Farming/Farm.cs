namespace Library.Farming;
using System.Timers;

// Esta clase representa una granja en el juego, que es un tipo de recolectable.
// La granja tiene una posición en el mapa, una cantidad inicial de recursos y un tiempo de recolección específico.

public class Farm: Recolection 
{
    public static string Symbol => "🌾🌾";
    
  

    public Farm((int x, int y) posicion, int cantidadinicial)
        : base(posicion, cantidadinicial,120) 
    {
        
    }
}   
