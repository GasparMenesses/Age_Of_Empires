namespace Library.Interfaces;
using Library;
public interface IConstruction
{
    public int ConstructionTime { get;  }
    public string Type { get; }
    public Resources Cost { get; }
    public List<int> Position { get; }
}