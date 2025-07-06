namespace Library.Core;

// Cumple con SRP porque este codigo se encarga de imprimir el mapa en la consola
public static class MapPrinter
{
    private static string _map = "";// Mapa que se va a imprimir en la consola(como string)
    private static string _html1 = "<!doctype html>\n<html lang=\"en\">\n<head>\n    <meta charset=\"UTF-8\">\n    <meta name=\"viewport\"\n          content=\"width=device-width, user-scalable=no, initial-scale=1.0, maximum-scale=1.0, minimum-scale=1.0\">\n    <meta http-equiv=\"X-UA-Compatible\" content=\"ie=edge\">\n    <link rel=\"stylesheet\" href=\"styles.css\">\n    <title>AgeOfEmpiresMap</title>\n</head>\n<body>\n<table class=\"tabla\">";
    private static string _html2 = "</table>\n</body>\n</html>";
    public static string PrintMap() // Este metodo devuelve el mapa como una matriz de strings
    {
        _map = "";

        int filas = Map.ReturnLength0();
        int columnas = Map.ReturnLength1();

        // Fila superior con índices de columnas y celdas vacías en los extremos
        _map += "<tr><td></td>"; // esquina superior izquierda vacía
        for (int col = 0; col < columnas; col++)
        {
            _map += "<td>" + (col + 1) + "</td>";
        }
        _map += "<td></td></tr>"; // esquina superior derecha vacía

        // Filas con índices laterales y datos del mapa
        for (int fila = 0; fila < filas; fila++)
        {
            _map += "<tr>";
            _map += "<td>" + (fila + 1) + "</td>"; // índice izquierdo

            for (int col = 0; col < columnas; col++)
            {
                _map += "<td>" + Map.CheckMap(fila, col) + "</td>";
            }

            _map += "<td>" + (fila + 1) + "</td>"; // índice derecho
            _map += "</tr>";
        }

        // Fila inferior con índices de columnas y celdas vacías en los extremos
        _map += "<tr><td></td>";
        for (int col = 0; col < columnas; col++)
        {
            _map += "<td>" + (col + 1) + "</td>";
        }
        _map += "<td></td></tr>";

        // Devolver el HTML completo
        return _html1 + _map + _html2;
    }
}