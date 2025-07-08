using Library.Core;
using Library.Units;

namespace Library.Buildings;

/// <summary>
/// Representa un cuartel en el juego, donde se entrenan unidades militares.
/// Cumple con SRP ya que se encarga exclusivamente de la lógica relacionada con el cuartel y el entrenamiento de unidades.
/// </summary>
public class Barrack : Building
{
    /// <summary>
    /// Símbolo que identifica al cuartel en el mapa.
    /// </summary>
    public override string Symbol => "🏯⚔️";

    /// <summary>
    /// Referencia al jugador propietario del cuartel.
    /// </summary>
    private Player _player;

    /// <summary>
    /// Diccionario que contiene los tipos de unidades disponibles para entrenar en el cuartel.
    /// </summary>
    public Dictionary<string, Unit> Unit { get; set; }

    /// <summary>
    /// Constructor de la clase Barrack.
    /// </summary>
    /// <param name="player">Jugador propietario del cuartel.</param>
    /// <param name="position">Posición (x, y) del cuartel.</param>
    public Barrack(Player player, (int x, int y) position) : base(woodCost: 25, stoneCost: 55, constructionTime: 30)
    {
        // Representa un edificio cuartel en el juego
        // Cumple con SRP porque solo se encarga de la lógica del cuartel    
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

    /// <summary>
    /// Entrena una cantidad específica de unidades en el cuartel si hay suficientes recursos.
    /// </summary>
    /// <param name="unit">Tipo de unidad a entrenar (clave del diccionario).</param>
    /// <param name="quantity">Cantidad de unidades a entrenar.</param>
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
