using Library.Core;
using Library.Units;
namespace Library.Buildings;

// Representa un cuartel en el juego, donde se entrenan unidades militares
// Cumple con SRP ya que se encarga exclusivamente de la lógica relacionada con el cuartel y el entrenamiento de unidades

public class Barrack : Building
{
    // Símbolo que identifica al cuartel en el mapa
    public override string Symbol => "🏯⚔️";
    private Player _player;
    public Dictionary<string, Unit> Unit { get; set; }

    public Barrack(Player player, (int x, int y) position) : base(woodCost: 25, stoneCost: 55, constructionTime: 30)
    {
        // Representa un edificio cuartel en el juego
        //cumple con srp porque solo se encarga de la lógica del cuartel    
        _player = player;
        Unit = new Dictionary<string, Unit>
        {
            { "Archer", new Archer(_player,this) },
            { "Cavalry", new Cavalry(_player,this) },
            { "Infantry", new Infantry(_player,this) },
            {"Thor",  new Thor(_player,this)},
            {"Borracho", new Borracho(_player,this)},
            {"JulioCesar", new JulioCesar(_player,this)}
        };
    }

    public void TrainingUnit(string unit, int quantity)
    {
        // Lógica para entrenar unidades en el cuartel
        int totalCost = Unit[unit].Cost * quantity;
        if (_player.Resources.Food >= totalCost)
        {
            for (int i = 0; i < quantity; i++)
            {
                Unit newUnit = unit switch
                {
                    "Archer" => new Archer(_player,this),
                    "Cavalry" => new Cavalry(_player,this),
                    "Infantry" => new Infantry(_player,this),
                    "Thor" => new Thor(_player,this),
                    "Borracho" => new Borracho(_player,this),
                    "JulioCesar" => new JulioCesar(_player,this),
                };
                _player.Units.Add(newUnit);
            }
            _player.Resources.Food -= totalCost;
        }
    }
}