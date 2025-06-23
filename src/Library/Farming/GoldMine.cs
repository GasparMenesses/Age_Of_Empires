namespace Library.Farming;

public class GoldMine: Recolection
{
    public static string Symbol => "GM";

    public Dictionary<string, int> Position { get; set; }

    public GoldMine((int x, int y) posicion, int cantidadinicial)
        : base(posicion, cantidadinicial,120) 
    {
        
    }
}
