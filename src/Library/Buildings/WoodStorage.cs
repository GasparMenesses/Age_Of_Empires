using Library.Interfaces;

namespace Library.Buildings;
using Core;

public class WoodStorage
{
    public int Wood { get; set; }
    public int Capacity { get; set; }
    private Resources resources;
    public WoodStorage(Player player)
    {
        Wood = 0;
        Capacity = 1000;
        resources = player.Resources;
        resources.AddLimitResources(wood:true);
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
        resources.AddResources(wood: wood);
    }
}