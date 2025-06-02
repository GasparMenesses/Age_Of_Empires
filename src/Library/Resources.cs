namespace Library;

public class Resources
{
    public List<int> Wood { get; private set; }
    public List<int> Stone { get; private set; }
    public List<int> Gold { get; private set; }
    public List<int> Food { get; private set; }

    public Resources()
    {
        Wood = new List<int> {100, 1000};
        Stone = new List<int> {0, 1000};
        Gold = new List<int> {0, 1000};
        Food = new List<int>{100, 1000};
    }

    public bool AddResources(int wood = 0, int stone = 0, int gold = 0, int food = 0)
    {
        if (wood < 0 || stone < 0 || gold < 0 || food < 0)
            return false;
        if ((Wood[0] + wood) > Wood[1])
            Wood[0] = Wood[1];
        else
            Wood[0] += wood;
        if ((Stone[0] + stone) > Stone[1])
            Stone[0] = Stone[1];
        else
            Stone[0] += stone;
        if ((Gold[0] + gold) > Gold[1])
            Gold[0] = Gold[1];
        else
            Gold[0] += gold;
        if ((Food[0] + food) > Food[1])
            Food[0] = Food[1];
        else
            Food[0] += food;
        return true;
    }

    public bool RemoveResources(int wood = 0, int stone = 0, int gold = 0, int food = 0)
    {
        if ((wood < 0 || stone < 0 || gold < 0 || food < 0) || (wood > Wood[0] || stone > Stone[0] || gold > Gold[0] || food > Food[0]))
            return false;
        Wood[0] -= wood;
        Stone[0] -= stone;
        Gold[0] -= gold;
        Food[0] -= food;
        return true;
    }

    public void AddLimitResources(bool wood = false, bool stone = false, bool gold = false, bool food = false)
    {
        if (wood)
            Wood[1] += 1000;
        if (stone)
            Stone[1] += 1000;
        if (gold)
            Gold[1] += 1000;
        if (food)
            Food[1] += 1000;
    }
}
