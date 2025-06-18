using Library.Core;

// Cumple con SRP porque este codigo se encarga de imprimir el mapa en la consola
public static class MapPrinter
{
    public static void PrintMap() // Este metodo imprime el mapa en la consola
    {
        for (int i = 0; i < Map.Board.GetLength(0); i++) // Recorre las filas del mapa
        {
            for (int j = 0; j < Map.Board.GetLength(1); j++) // Recorre las columnas del mapa
            {
                Console.Write(Map.Board[i, j] + " "); // Imprime el simbolo del mapa en la posicion actual
            }
            Console.WriteLine();    // Imprime un salto de linea al final de cada fila
        }
    }
}