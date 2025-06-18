namespace Library.Core;

using Library.Interfaces;


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
                    Board[i, j] = ".";
        }
    }
    
    public static void PlaceBuildings(int cantidad, string simbolo)
    {
        var rand = new Random();
        int colocados = 0;

        while (colocados < cantidad)
        {
            int x = rand.Next(Board.GetLength(0));
            int y = rand.Next(Board.GetLength(1));
            if (Board[x, y] == ".")
            {
                Board[x, y] = simbolo;
                colocados++;
            }
        }
    }

    public static string CheckMap(int x, int y)
    {
        return Board[x, y];
    }
}