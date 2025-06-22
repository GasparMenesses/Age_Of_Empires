namespace Library.Interfaces;
using Library;
public interface IUnit//Todas las unidades deben implementar esta interfaz ya que incluso los villagers en el juego pueden atacar
{
    public int TimeTraining { get; }
    public int Cost { get; }
    public Dictionary<string, int> Position { get; set; }
    public int Speed { get; }
    public int Attack { get; }
    public int Defense { get; }
}