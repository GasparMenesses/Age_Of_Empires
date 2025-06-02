namespace Library.Buildings;
using Core;
public class Mill
{
    public int Food { get; set; }
    public int Capacity { get; set; }
    private Resources resources;
    public Mill(Player player)
    {
        Food = 0;
        Capacity = 1000;
        resources = player.Resources;
        resources.AddLimitResources(food:true);
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
        resources.AddResources(food: food);
    }
}