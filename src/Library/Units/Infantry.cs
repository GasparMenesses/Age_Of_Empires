namespace Library;

public class Infantry
{
    public int Speed { get; set; }
    public int Attack { get; set; }
    public int Defense { get; set; }
    public int CreationTime { get; set; }
    public int CreationCost { get; set; }
    

    public Infantry()
    {
        Speed = 40;
        Attack = 23;
        Defense = 45;
        CreationTime = 43;
        CreationCost = 100;
    }
}