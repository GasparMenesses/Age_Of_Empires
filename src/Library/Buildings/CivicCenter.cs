using Library.Core;

namespace Library.Buildings;

public class CivicCenter
{
    public int Gold { get; set; }
    public int Wood { get; set; }
    public int Food { get; set; }
    public int Stone { get; set; }
    public int Capacity { get;  } 
    public int MaxCapacityAldeano { get; }
    private Resources _resources;
    public CivicCenter(Player player)
    {
        Gold = 0;
        Stone = 0;
        Wood = 100;
        Food = 100;
        Capacity = 1000;
        MaxCapacityAldeano = 10;
        _resources = player.Resources;
        _resources.AddLimitResources(true, true, true, true); //Aumenta el limite de cada recurso a 1000
    }
    public void AddStone(int stone)
    {
        if ((Stone + stone) > Capacity)
        {
            stone = Capacity - Stone;
            Stone = Capacity;
        }
        else
            Stone += stone;
        _resources.AddResources(stone: stone);
    }
    public void AddGold(int gold)
    {
        if ((Gold + gold) > Capacity)
        {
            gold = Capacity - Gold;
            Gold = Capacity;
        }
        else
            Gold += gold;
        _resources.AddResources(gold: gold);
    }
    public void AddWood(int wood)
    {
        if ((Wood + wood) > Capacity)
        {
            wood = Capacity - Wood;
            Wood = Capacity;
        }
        else
            Wood += wood;
        _resources.AddResources(wood: wood);
    }
    public void AddFood(int food)
    {
        if ((Food + food) > Capacity)
        {
            food = Capacity - Food;
            Food = Capacity;
        }
        else
            Food += food;
        _resources.AddResources(food: food);
    }
}