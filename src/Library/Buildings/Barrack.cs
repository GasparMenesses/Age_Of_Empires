namespace Library.Buildings;

public class Barrack
{
    public double CreationTime { get; set; }
    public int CreationCostWood { get; set; }
    public int CreationCostStone { get; set; }

    public Barrack()
    {
        CreationTime = 1.2; 
        CreationCostStone = 500;
        CreationCostWood = 300;
    }

}