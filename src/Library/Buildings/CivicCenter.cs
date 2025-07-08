using Library.Core;
using Library.Units;

namespace Library.Buildings;

/// <summary>
/// Representa el centro cívico del jugador, encargado de almacenar y gestionar recursos principales.
/// Cumple con SRP ya que se encarga exclusivamente de la lógica relacionada con el centro cívico y la gestión de recursos.
/// </summary>
public class CivicCenter : Building
{
    /// <summary>
    /// Símbolo identificador del centro cívico en el mapa.
    /// </summary>
    public override string Symbol => "🏰🏰";

    /// <summary>
    /// Cantidad de oro almacenado en el centro cívico.
    /// </summary>
    public int Gold { get; set; }

    /// <summary>
    /// Cantidad de madera almacenada en el centro cívico.
    /// </summary>
    public int Wood { get; set; }

    /// <summary>
    /// Cantidad de comida almacenada en el centro cívico.
    /// </summary>
    public int Food { get; set; }

    /// <summary>
    /// Cantidad de piedra almacenada en el centro cívico.
    /// </summary>
    public int Stone { get; set; }

    /// <summary>
    /// Capacidad máxima de almacenamiento de recursos.
    /// </summary>
    public int Capacity { get; set; }

    /// <summary>
    /// Capacidad máxima de aldeanos.
    /// </summary>
    public int MaxCapacityAldeano { get; set; }

    private Villager _villager;

    /// <summary>
    /// Constructor del centro cívico. Inicializa recursos, capacidad y configuración base.
    /// </summary>
    public CivicCenter()
        : base(0, 0, 0) // Ajusta los valores según tu lógica
    {
        Gold = 0;
        Stone = 0;
        Wood = 100;
        Food = 100;
        Capacity = 1000;
        MaxCapacityAldeano = 10;
    }

    /// <summary>
    /// Agrega piedra al centro cívico, sin exceder la capacidad máxima.
    /// </summary>
    /// <param name="_player">Jugador asociado.</param>
    /// <param name="stone">Cantidad de piedra a agregar.</param>
    public void AddStone(Player _player, int stone)
    {
        if ((Stone + stone) > Capacity)
        {
            stone = Capacity - Stone;
            Stone = Capacity;
        }
        else
            Stone += stone;
        _player.Resources.AddResources(stone: stone);
    }

    /// <summary>
    /// Agrega oro al centro cívico, sin exceder la capacidad máxima.
    /// </summary>
    /// <param name="_player">Jugador asociado.</param>
    /// <param name="gold">Cantidad de oro a agregar.</param>
    public void AddGold(Player _player, int gold)
    {
        if ((Gold + gold) > Capacity)
        {
            gold = Capacity - Gold;
            Gold = Capacity;
        }
        else
            Gold += gold;
        _player.Resources.AddResources(gold: gold);
    }

    /// <summary>
    /// Agrega madera al centro cívico, sin exceder la capacidad máxima.
    /// </summary>
    /// <param name="_player">Jugador asociado.</param>
    /// <param name="wood">Cantidad de madera a agregar.</param>
    public void AddWood(Player _player, int wood)
    {
        if ((Wood + wood) > Capacity)
        {
            wood = Capacity - Wood;
            Wood = Capacity;
        }
        else
            Wood += wood;
        _player.Resources.AddResources(wood: wood);
    }

    /// <summary>
    /// Agrega comida al centro cívico, sin exceder la capacidad máxima.
    /// </summary>
    /// <param name="_player">Jugador asociado.</param>
    /// <param name="food">Cantidad de comida a agregar.</param>
    public void AddFood(Player _player, int food)
    {
        if ((Food + food) > Capacity)
        {
            food = Capacity - Food;
            Food = Capacity;
        }
        else
            Food += food;
        _player.Resources.AddResources(food: food);
    }

    /// <summary>
    /// Entrena aldeanos si hay suficiente comida disponible.
    /// </summary>
    /// <param name="_player">Jugador que entrena las unidades.</param>
    /// <param name="quantity">Cantidad de aldeanos a entrenar.</param>
    public void TrainingUnit(Player _player, int quantity)
    {
        // Lógica para generar villagers en el CivicCenter
        int totalCost = _villager.Cost * quantity;
        if (_player.Resources.Food >= totalCost)
        {
            for (int i = 0; i < quantity; i++)
            {
                _player.Units.Add(new Villager(_player, this));
            }
            _player.Resources.Food -= totalCost;
        }
        
    }
}
