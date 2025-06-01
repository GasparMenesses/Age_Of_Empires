namespace Library;

public class StoneStorage
{
    public int Stone { get; set; }
    public int Capacity { get; set; }
    public StoneStorage()
    {
        Stone = 100;
        Capacity = 1000;
    }

    public void AddStone(int stone)
    {
        Stone += stone;
    }

}