namespace Library;

public class Mill
{
    public int Food { get; set; }

    public Mill()
    {
        Food = 100;
    }

    public void AddFood(int food)
    {
        Food = Food + food;
    }
}