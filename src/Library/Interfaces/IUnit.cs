namespace Library.Interfaces;
using Library;

// Esta interfaz define las propiedades y métodos necesarios para manejar una unidad en el juego, como un soldado o un aldeano.
public interface IUnit//Todas las unidades deben implementar esta interfaz ya que incluso los villagers en el juego pueden atacar
{
    public int TimeTraining { get; }
    public int Cost { get; }
    public Dictionary<string, int> Position { get; set; }
    public int Speed { get; }
    public int Attack { get; }
    public int Defense { get; }
    public int Life { get; set; }
}