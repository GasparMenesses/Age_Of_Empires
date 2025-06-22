namespace Library.Units;

public class Villager : Unit
{
    public int Villages { get; set; }

    public Villager(int cantidad)
    {
        Villages = cantidad;
    }
    
}