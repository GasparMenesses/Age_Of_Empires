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
    
    public static void PlaceRandom(int cantidad, string simbolo, Building building = null)
    {
        var rand = new Random();
        int colocados = 0;

        while (colocados < cantidad)
        {
            int x = rand.Next(Board.GetLength(0));
            int y = rand.Next(Board.GetLength(1));
            if (Board[x, y] == "..")
            {
                ChangeMap(x,y,simbolo);
                if (building != null)
                {
                    building.Position["x"] = x;
                    building.Position["y"] = y;
                }
                colocados++;
            }
        }
    }
    public static void PlaceRandom(int cantidad, Building building)
    {
        PlaceRandom(cantidad, building.Symbol, building);
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