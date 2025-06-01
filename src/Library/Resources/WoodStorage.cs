namespace Library;

public class WoodStorage
{
    public int Wood { get; set; }
    public int Capacity { get; set; }

    public WoodStorage()
        {
            Wood = 100;
            Capacity = 1000;
        }

        public void AddWood(int wood)
        {
            Wood += wood;
        }
}