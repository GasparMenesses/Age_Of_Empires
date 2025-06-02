namespace Library.Buildings;
using Core;
public class StoneStorage
{
    public int Stone { get; set; }
    public int Capacity { get; set; }
    private Resources resources;
    public StoneStorage(Player player)
    {
        Stone = 0;
        Capacity = 1000;
        resources = player.Resources;
        resources.AddLimitResources(stone:true);
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
        resources.AddResources(stone: stone);
    }
}