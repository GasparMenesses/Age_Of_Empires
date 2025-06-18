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
                    Board[i, j] = ".";
        }
    }
    public static string CheckMap(int x, int y)
    {
        return Board[x, y];
    }
    public static void ChangeMap(int x, int y, string change)
    {
        Board[x, y] = change;
    }
}