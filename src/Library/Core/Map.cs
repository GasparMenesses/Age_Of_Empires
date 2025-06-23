using Library.Buildings;
namespace Library.Core;

public class Map
{
    public static string[,] Board;

    public Map()
    {
        if (Board == null)
        {
            Board = new string[100, 100];
            for (int i = 0; i < 100; i++)
            for (int j = 0; j < 100; j++)
                Board[i, j] = "..";
        }
    }

    // Coloca un edificio en una posición aleatoria y retorna la posición
    public static void PlaceBuilding(string simbolo, Building building = null)
    {
        var rand = new Random();
        int x, y;
        do
        {
            x = rand.Next(1, 100);
            y = rand.Next(1, 100);
        } while (Board[x, y] != "..");

        Board[x, y] = simbolo;
        if (building != null)
        {
            building.Position["x"] = x;
            building.Position["y"] = y;
        }
        return;
    }

    // Sobrecarga para usar el símbolo del edificio
    public static void PlaceRandom(int cantidad, Building building)
    {
        for (int i = 0; i < cantidad; i++)
        {
            PlaceBuilding(building.Symbol, building);
        }
    }

    public static string CheckMap(int x, int y)
    {
        return Board[x, y];
    }
    public static void ChangeMap(int x, int y, string simbolo)
    {
        Board[x, y] = simbolo;
    }
}