using Library.Core;

namespace Library;

// Esta clase representa los recursos del jugador en el juego, incluyendo madera, piedra, oro y comida.
// Cada recurso tiene un valor actual y un límite máximo.
// Los recursos pueden ser añadidos o eliminados, y los límites de cada recurso pueden ser incrementados.
// Además, se proporciona un método para mostrar los recursos del jugador.

public class Resources
{
    public int Wood { get;  set; }
    public int WoodLimit { get; private set; }
    public int Stone { get;  set; }
    public int StoneLimit { get; private set; }
    public int Gold { get;  set; }
    public int GoldLimit { get; private set; }
    public int Food { get;  set; }
    public int FoodLimit { get; private set; }

    public Resources()
    {
        Wood = 100;
        WoodLimit = 1000;
        Stone = 0;
        StoneLimit = 1000;
        Gold = 0;
        GoldLimit = 1000;
        Food = 100;
        FoodLimit = 1000;
    }

    public bool AddResources(int wood = 0, int stone = 0, int gold = 0, int food = 0)
    {
        if (wood < 0 || stone < 0 || gold < 0 || food < 0)
            return false;
        Wood = Math.Min(Wood + wood, WoodLimit);
        Stone = Math.Min(Stone + stone, StoneLimit);
        Gold = Math.Min(Gold + gold, GoldLimit);
        Food = Math.Min(Food + food, FoodLimit);
        return true;
    }

    public bool RemoveResources(int wood = 0, int stone = 0, int gold = 0, int food = 0)
    {
        if (wood < 0 || stone < 0 || gold < 0 || food < 0)
            return false;
        if (wood > Wood || stone > Stone || gold > Gold || food > Food)
            return false;
        Wood -= wood;
        Stone -= stone;
        Gold -= gold;
        Food -= food;
        return true;
    }

    public void AddLimitResources(bool wood = false, bool stone = false, bool gold = false, bool food = false)
    {
        if (wood)
            WoodLimit += 1000;
        if (stone)
            StoneLimit += 1000;
        if (gold)
            GoldLimit += 1000;
        if (food)
            FoodLimit += 1000;
    }

    public Resources ShowResources(Player player)
    {
        return player.Resources;
    }
}
