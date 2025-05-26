namespace Library;

public class House
{
    public int CreationTime { get; set; }
    public int CreationCostWood { get; set; }
    public int CreationCostStone { get; set; }
    public int PopulationIncrease { get; set; } //cada clasa construida aumenta la población máxima

    public House()
    {
        CreationTime = 60; //60 seg por ejemplo despues lo podemos cambiar
        CreationCostStone = 500;
        CreationCostWood = 300;
        PopulationIncrease = 5;
    }
    
}