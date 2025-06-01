namespace Library;

public class Resources
{
    public int Wood { get; private set; }
    public int Stone { get; private set; }
    public int Gold { get; private set; }
    public int Food { get; private set; }

    public Resources()
    {
        Wood = 100;
        Stone = 0;
        Gold = 0;
        Food = 100;
    }

    public bool AddResources(int wood = 0, int stone = 0, int gold = 0, int food = 0)
    {
        if (wood < 0 || stone < 0 || gold < 0 || food < 0)
            return false;
        Wood += wood;
        Stone += stone;
        Gold += gold;
        Food += food;
        return true;
    }

    public bool RemoveResources(int wood = 0, int stone = 0, int gold = 0, int food = 0)
    {
        if ((wood < 0 || stone < 0 || gold < 0 || food < 0) || (wood > Wood || stone > Stone || gold > Gold || food > Food))
            return false;
        Wood -= wood;
        Stone -= stone;
        Gold -= gold;
        Food -= food;
        return true;
    }
}
