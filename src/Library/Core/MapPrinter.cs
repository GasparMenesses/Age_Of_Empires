namespace Library.Core
{
    // Cumple con SRP porque este código se encarga de imprimir el mapa en la consola/HTML
    public static class MapPrinter
    {
        private static string _map = "";
        private static string _html1 = "<!doctype html>\n<html lang=\"en\">\n<head>\n    <meta charset=\"UTF-8\">\n    <meta name=\"viewport\"\n          content=\"width=device-width, user-scalable=no, initial-scale=1.0, maximum-scale=1.0, minimum-scale=1.0\">\n    <meta http-equiv=\"X-UA-Compatible\" content=\"ie=edge\">\n    <link rel=\"stylesheet\" href=\"styles.css\">\n    <title>AgeOfEmpiresMap</title>\n</head>\n<body>\n<table class=\"tabla\">";
        private static string _html2 = "</table>\n</body>\n</html>";

        public static string PrintMap()
        {
            _map = "";

            int filas = Map.ReturnLength0();
            int columnas = Map.ReturnLength1();

            // Fila superior: 0..99
            _map += "<tr><td></td>";
            for (int col = 0; col < columnas; col++)
                _map += "<td>" + col + "</td>";
            _map += "<td></td></tr>";

            // Filas con índice izquierdo y derecho 0..99
            for (int fila = 0; fila < filas; fila++)
            {
                _map += "<tr>";
                _map += "<td>" + fila + "</td>";      // índice izquierdo

                for (int col = 0; col < columnas; col++)
                    _map += "<td>" + Map.CheckMap(fila, col) + "</td>";

                _map += "<td>" + fila + "</td>";     // índice derecho
                _map += "</tr>";
            }

            // Fila inferior: 0..99
            _map += "<tr><td></td>";
            for (int col = 0; col < columnas; col++)
                _map += "<td>" + col + "</td>";
            _map += "<td></td></tr>";

            return _html1 + _map + _html2;
        }
    }
}