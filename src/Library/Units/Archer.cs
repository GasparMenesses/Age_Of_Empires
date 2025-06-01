using Library.Interfaces;

namespace Library;

public class Archer 
{
    public int Speed { get; set; }
    public int Attack { get; set; }
    public int Defense { get; set; }
    public int TimeTraining { get; set; }

    public Archer()
    {
        Speed = 20;
        Attack = 25;
        Defense = 40;
        TimeTraining = 20;
    }
}