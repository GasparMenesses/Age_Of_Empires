using Library.Core;
namespace Library.Buildings;

public class CivicCenter : Building
{
    public new static string Symbol => "CC";
    
    public int Gold { get; set; }
    public int Wood { get; set; }
    public int Food { get; set; }
    public int Stone { get; set; }
    public int Capacity { get; set; } 
    public int MaxCapacityAldeano { get; set; }
    private Player _player;
    
    public CivicCenter(Player player)  : base((0,0),0,0,0)
    {
        _player = player;
        Gold = 0;
        Stone = 0;
        Wood = 100;
        Food = 100;
        Capacity = 1000;
        MaxCapacityAldeano = 10;
        player.Resources.AddLimitResources(true, true, true, true); //Aumenta el limite de cada recurso en 1000
        player.Buildings.Add(this);
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
        _player.Resources.AddResources(stone: stone);
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
        _player.Resources.AddResources(gold: gold);
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
        _player.Resources.AddResources(wood: wood);
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
        _player.Resources.AddResources(food: food);
    }
}