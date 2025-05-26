namespace Library;

public class Barrack
{
    public int CreationTime { get; set; }
    public int CreationCostWood { get; set; }
    public int CreationCostStone { get; set; }

    public Barrack()
    {
        CreationTime =
            CreationCostStone = 500;
        CreationCostWood = 300;
    }

}