namespace Library.Core;

// Cumple con SRP porque este codigo se encarga de imprimir el mapa en la consola
public static class MapPrinter
{
    private static string _map = "";// Mapa que se va a imprimir en la consola(como string)
    public static string PrintMap() // Este metodo devuelve el mapa como una matriz de strings
    {
        for (int i = 0; i < Map.ReturnLength0(); i++) // Recorre las filas del mapa
        {
            for (int j = 0; j < Map.ReturnLength1(); j++) // Recorre las columnas del mapa
            {
                _map += (Map.CheckMap(i, j) + " "); // Coloca el simbolo del mapa en la posicion actual
            }
            _map += "\n"; // Añade un salto de línea al final de cada fila
        }
        return _map; // Devuelve el mapa
    }
}