namespace Library;

public class GoldStorage
{
    public int Gold { get; set; }

    public GoldStorage()
    {
        Gold = 0;
    }

    public void AddGold(int gold)
    {
        Gold += gold;
    }
}