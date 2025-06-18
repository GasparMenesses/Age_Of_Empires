using Library;

public class Map
{
    public string[,] Board;
    public Map()
    {
        Board = new string[100, 100];
        for (int i = 0; i < 100; i++)
             for (int j = 0; j < 100; j++)
                     Board[i, j] = ".";
    }

    public string CheckMap(int x, int y)
    {
        return Board[x, y];
    }
}