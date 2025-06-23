
using Library.Core;

namespace Library.Buildings;

// Representa el centro cívico del jugador, encargado de almacenar y gestionar recursos principales
public class CivicCenter : Building
{
    // Símbolo identificador del centro cívico en el mapa
    public static string Symbol => "CC";
    // Coordenadas del centro cívico en el mapa (pueden ser modificadas)
    public static string[,] Coordenadas = new string[0, 0];

    // Recursos almacenados en el centro cívico
    public int Gold { get; set; }
    public int Wood { get; set; }
    public int Food { get; set; }
    public int Stone { get; set; }
    // Capacidad máxima de almacenamiento de recursos
    public int Capacity { get; }
    // Capacidad máxima de aldeanos
    public int MaxCapacityAldeano { get; }
    // Referencia a los recursos del jugador
    private Resources _resources;

    // Constructor: inicializa recursos, capacidad y asocia los recursos del jugador
    public CivicCenter(Player player)
        : base(player.Resources, woodCost: 0, stoneCost: 0, constructionTime: 0, posicion: (0, 0)) // Valores dummy por ahora
    {
        Gold = 0;
        Stone = 0;
        Wood = 100;
        Food = 100;
        Capacity = 1000;
        MaxCapacityAldeano = 10;
        _resources = player.Resources;
        // Establece los límites máximos de recursos
        _resources.AddLimitResources(true, true, true, true);
    }

    // Establece la ubicación del centro cívico a partir de coordenadas dadas
    public void Ubicacion(string[,] coordenadas)
    {
        Coordenadas = coordenadas;

        // Intenta convertir las coordenadas a enteros y asignarlas a la posición
        if (int.TryParse(coordenadas[0, 0], out int x) && int.TryParse(coordenadas[0, 1], out int y))
        {
            Posicion = (x, y); // propiedad heredada de Building
        }
    }

    // Agrega piedra al centro cívico, sin exceder la capacidad máxima
    public void AddStone(int stone)
    {
        Stone = Math.Min(Stone + stone, Capacity); // Asegura que no se exceda la capacidad
        _resources.AddResources(stone: stone);
    }

    // Agrega oro al centro cívico, sin exceder la capacidad máxima
    public void AddGold(int gold)
    {
        Gold = Math.Min(Gold + gold, Capacity);
        _resources.AddResources(gold: gold);
    }

    // Agrega madera al centro cívico, sin exceder la capacidad máxima
    public void AddWood(int wood)
    {
        Wood = Math.Min(Wood + wood, Capacity);
        _resources.AddResources(wood: wood);
    }

    // Agrega comida al centro cívico, sin exceder la capacidad máxima
    public void AddFood(int food)
    {
        Food = Math.Min(Food + food, Capacity);
        _resources.AddResources(food: food);
    }
}
