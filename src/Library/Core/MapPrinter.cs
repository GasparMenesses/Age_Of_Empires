namespace Library.Core
{
    /// <summary>
    /// Clase estática encargada de imprimir el mapa en formato HTML para visualización.
    /// Cumple con SRP porque solo se encarga de la presentación del mapa.
    /// </summary>
    public static class MapPrinter
    {
        /// <summary>
        /// Cadena que contiene el contenido HTML generado para el mapa.
        /// </summary>
        private static string _map = "";

        /// <summary>
        /// Cabecera HTML y estilos para la página que mostrará el mapa.
        /// </summary>
        private static string _html1 = "<!doctype html>\n<html lang=\"en\">\n<head>\n    <meta charset=\"UTF-8\">\n    <meta name=\"viewport\"\n          content=\"width=device-width, user-scalable=no, initial-scale=1.0, maximum-scale=1.0, minimum-scale=1.0\">\n    <meta http-equiv=\"X-UA-Compatible\" content=\"ie=edge\">\n    <link rel=\"stylesheet\" href=\"styles.css\">\n    <title>AgeOfEmpiresMap</title>\n</head>\n<body>\n<table class=\"tabla\">";

        /// <summary>
        /// Pie de página HTML que cierra las etiquetas abiertas.
        /// </summary>
        private static string _html2 = "</table>\n</body>\n</html>";

        /// <summary>
        /// Genera y retorna el mapa completo en formato HTML con encabezados de fila y columna.
        /// </summary>
        /// <returns>Cadena HTML que representa el mapa con sus contenidos y bordes de índice.</returns>
        public static string PrintMap()
        {
            _map = "";

            int filas = Map.ReturnLength0();
            int columnas = Map.ReturnLength1();

            // Fila superior: índices de columnas 0..n
            _map += "<tr><td></td>";
            for (int col = 0; col < columnas; col++)
                _map += "<td>" + col + "</td>";
            _map += "<td></td></tr>";

            // Filas con índices a izquierda y derecha y el contenido del mapa
            for (int fila = 0; fila < filas; fila++)
            {
                _map += "<tr>";
                _map += "<td>" + fila + "</td>";      // índice izquierdo

                for (int col = 0; col < columnas; col++)
                    _map += "<td>" + Map.CheckMap(fila, col) + "</td>";

                _map += "<td>" + fila + "</td>";     // índice derecho
                _map += "</tr>";
            }

            // Fila inferior: índices de columnas 0..n
            _map += "<tr><td></td>";
            for (int col = 0; col < columnas; col++)
                _map += "<td>" + col + "</td>";
            _map += "<td></td></tr>";

            return _html1 + _map + _html2;
        }
    }
}
