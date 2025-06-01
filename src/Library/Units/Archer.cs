namespace Library;

public class Archer
{
    public int Speed { get; set; }
    public int Attack { get; set; }
    public int Defense { get; set; }
    public int CreationTime { get; set; }

    public Archer()
    {
        Speed = 20;
        Attack = 25;
        Defense = 40;
        CreationTime = 20;
    }
}