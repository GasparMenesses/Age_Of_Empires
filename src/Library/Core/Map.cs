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
                    Board[i, j] = "..";
        }
    }
    
    public static string[,] PlaceBuildings(string simbolo)
    {
        int x = new Random().Next(1, 100);
        int y = new Random().Next(1, 100);
        // Checkeo si la posición está ocupada
        while (Board[x, y] != "..")
        {
            x = new Random().Next(1, 100);
            y = new Random().Next(1, 100);
        }
        Board[x, y] = simbolo;
        return new string[1, 2] { { x.ToString(), y.ToString() } };
    }

    public static string CheckMap(int x, int y)
    {
        return Board[x, y];
    }
}