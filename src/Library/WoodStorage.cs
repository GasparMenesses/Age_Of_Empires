namespace Library;

public class WoodStorage
{
    public int Wood { get; set; }

    public WoodStorage()
    {
        Wood = 100;
    }

    public void AddGold(int wood)
    {
        Wood = Wood + wood;
    }

}