namespace Library.Farming;

public class Quarry: Recolection
{
    public static string Symbol => "Qy";
    public Dictionary<string, int> Position { get; set; }

    public Quarry((int x, int y) posicion, int cantidadinicial)
        : base(posicion, cantidadinicial,120) 
    {
        
    }
}