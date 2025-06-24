using Library.Buildings;
using Library.Farming;

namespace Library.Core;

// Representa el mapa del juego, donde se colocan los edificios y recolectables

public class Map
{
    public static string[,] Board = new string[100, 100];

    static Map()
    {
        for (int i = 0; i < 100; i++)
            for (int j = 0; j < 100; j++)
                Board[i, j] = "..";
    }

    // Coloca un edificio en una posición aleatoria y retorna la posición
    public static void PlaceRandom(string simbolo, Building building = null, Recolection recolection = null)
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

        if (recolection != null)
        {
            recolection.Position["x"] = x;
            recolection.Position["y"] = y;
        }
    }

    public static string CheckMap(int x, int y)
    {
        return Board[x, y];
    }

    public static void ChangeMap((int x, int y) position, string simbolo, Building building = null)
    {
        Board[position.x, position.y] = simbolo;
        if (building != null)
        {
            building.Position["x"] = position.x;
            building.Position["y"] = position.y;
        }
    }
}