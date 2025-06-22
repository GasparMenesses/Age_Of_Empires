using Library.Core;

namespace Library.Buildings;

public class CivicCenter : Building
{
    public static string Symbol => "CC";
    public static string[,] Coordenadas = new string[0, 0];

    public int Gold { get; set; }
    public int Wood { get; set; }
    public int Food { get; set; }
    public int Stone { get; set; }
    public int Capacity { get; }
    public int MaxCapacityAldeano { get; }
    private Resources _resources;

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
        _resources.AddLimitResources(true, true, true, true);
    }

    public void Ubicacion(string[,] coordenadas)
    {
        Coordenadas = coordenadas;

        if (int.TryParse(coordenadas[0, 0], out int x) && int.TryParse(coordenadas[0, 1], out int y))
        {
            Posicion = (x, y); // propiedad heredada de Building
        }
    }

    public void AddStone(int stone)
    {
        Stone = Math.Min(Stone + stone, Capacity); // Asegura que no se exceda la capacidad
        _resources.AddResources(stone: stone);
    }

    public void AddGold(int gold)
    {
        Gold = Math.Min(Gold + gold, Capacity); 
        _resources.AddResources(gold: gold);
    }

    public void AddWood(int wood)
    {
        Wood = Math.Min(Wood + wood, Capacity);
        _resources.AddResources(wood: wood);
    }

    public void AddFood(int food)
    {
        Food = Math.Min(Food + food, Capacity);
        _resources.AddResources(food: food);
    }
}