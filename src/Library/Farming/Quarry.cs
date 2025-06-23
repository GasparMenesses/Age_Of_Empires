namespace Library.Farming;

public class Quarry: Recolection
{
    
    
    public static string Symbol => "Qy";
    public Quarry((int x, int y) posicion, int cantidadinicial)
        : base(posicion, cantidadinicial,200)
    {
        
    }
}