namespace Library.Farming;
using System.Timers;

public class Farm: Recolection 
{
    public static string Symbol => "Fm";
    
    public Dictionary<string, int> Position { get; set; }

    public Farm((int x, int y) posicion, int cantidadinicial)
        : base(posicion, cantidadinicial,120) 
    {
        
    }
}   
