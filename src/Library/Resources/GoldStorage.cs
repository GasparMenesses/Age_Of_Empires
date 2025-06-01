namespace Library;

public class GoldStorage
{
    public int Gold { get; set; }
    public int Capacity { get; set; }
    public GoldStorage()
    {
        Gold = 0;
        Capacity = 1000;
    }

    public void AddGold(int gold)
    {
        Gold += gold;
    }
}