using Library.Core;

namespace Library.Buildings;

// Representa el centro cívico del jugador, encargado de almacenar y gestionar recursos principales
public class CivicCenter : Building
{
    // Símbolo identificador del centro cívico en el mapa
    public new static string Symbol => "CC";

    // Recursos almacenados en el centro cívico
    public int Gold { get; set; }
    public int Wood { get; set; }
    public int Food { get; set; }
    public int Stone { get; set; }
    // Capacidad máxima de almacenamiento de recursos
    public int Capacity { get; set; }
    // Capacidad máxima de aldeanos
    public int MaxCapacityAldeano { get; set; }
    private Player _player;

    // Constructor: inicializa recursos, capacidad y asocia los recursos del jugador
    public CivicCenter(Player player)
        : base((0, 0), 0, 0, 0) // Ajusta los valores según tu lógica
    {
        Position = new Dictionary<string, int>
        {
            { "x", 0 },
            { "y", 0 }
        };
        _player = player;
        Gold = 0;
        Stone = 0;
        Wood = 100;
        Food = 100;
        Capacity = 1000;
        MaxCapacityAldeano = 10;
        // Establece los límites máximos de recursos
        player.Resources.AddLimitResources(true, true, true, true); // Aumenta el límite de cada recurso
        player.Buildings.Add(this);
    }

    // Agrega piedra al centro cívico, sin exceder la capacidad máxima
    public void AddStone(int stone)
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

    // Agrega oro al centro cívico, sin exceder la capacidad máxima
    public void AddGold(int gold)
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

    // Agrega madera al centro cívico, sin exceder la capacidad máxima
    public void AddWood(int wood)
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

    // Agrega comida al centro cívico, sin exceder la capacidad máxima
    public void AddFood(int food)
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
}
