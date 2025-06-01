namespace Library;

public class Map
{
    public string[][] Board;

    public Map()
    {
        Board = new string[100][];

        for (int i = 0; i < 100; i++)
        {
            Board[i] = new string[100];

            for (int j = 0; j < 100; j++)
            {
                Board[i][j] = "."; // Valor por defecto
            }
        }
    }

    // public void Imprimir()   ESTO NO VA ACÁ, NO ES SRP PERO FUNCA
    // {
    //     for (int i = 0; i < 100; i++)
    //     {
    //         for (int j = 0; j < 100; j++)
    //         {
    //             Console.Write(Board[i][j] + " ");
    //         }
    //         Console.WriteLine(); // Nueva línea al terminar una fila
    //     }
    // }
}