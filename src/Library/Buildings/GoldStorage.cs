namespace Library.Buildings;
using Core;
public class GoldStorage
{
    public int Gold { get; set; }
    public int Capacity { get; }
    private Resources resources;
    public GoldStorage(Player player)
    {
        Gold = 0;
        Capacity = 1000;
        resources = player.Resources;
        resources.AddLimitResources(gold:true);
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
        resources.AddResources(gold: gold);
    }
}
