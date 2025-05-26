namespace Library;

public class StoneStorage
{
    public int Stone { get; set; }

    public StoneStorage()
    {
        Stone = 0;
    }

    public void AddStone(int stone)
    {
        Stone += stone;
    }

}