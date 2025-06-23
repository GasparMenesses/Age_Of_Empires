namespace Library.Farming;

public class Woods: Recolection
{
    public static string Symbol => "Wd";
    public Dictionary<string, int> Position { get; set; }

    public Woods((int x, int y) posicion, int cantidadinicial)
        : base(posicion, cantidadinicial,120) 
    {
        
    }
}
