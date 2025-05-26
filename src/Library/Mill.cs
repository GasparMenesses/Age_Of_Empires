namespace Library;

public class Mill
{
    public int Food { get; set; }
    public int Capacity { get; set; }
    public Mill()
    {
        Food = 100;
        Capacity = 1000;
    }

    public void AddFood(int food)
    {
        Food += food;
    }
}