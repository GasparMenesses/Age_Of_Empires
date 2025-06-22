namespace Library.Units;

public class Villager : Unit
{
    public int Villagers { get; set; }

    public Villager(int cantidad)
    {
        Villagers = cantidad; 
    }
    
}