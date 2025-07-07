using Library.Buildings;
using Library.Farming;

namespace Library.Core;

// Representa el mapa del juego, donde se colocan los edificios y recolectables

public class Map
{
    private static string[,] Board = new string[100, 100];

    static Map()
    {
        for (int i = 0; i < 100; i++)
            for (int j = 0; j < 100; j++)
                Board[i, j] = "..";
    }
    
    public static string CheckMap(int x, int y)
    {
        if (x < 0 || x >= Board.GetLength(0) || y < 0 || y >= Board.GetLength(1))
            return null;
        return Board[x, y];
    }

    public static void ChangeMap((int x, int y) position, string simbolo)
    {
        Board[position.x, position.y] = simbolo;
    }
    public static int ReturnLength0()
    {
        return Board.GetLength(0);
    }
    public static int ReturnLength1()
    {
        return Board.GetLength(1);
    }

    public static string [,] ReturnBoard()
    {
        return Board;
    }
    
}