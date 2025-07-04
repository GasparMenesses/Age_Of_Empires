﻿namespace Library.Core;

// Cumple con SRP porque este codigo se encarga de imprimir el mapa en la consola
public static class MapPrinter
{
    private static string _map = "";// Mapa que se va a imprimir en la consola(como string)
    private static string _html1 = "<!doctype html> <html lang=\"en\"> <head> <meta charset=\"UTF-8\"> <meta name=\"viewport\" content=\"width=device-width, user-scalable=no, initial-scale=1.0, maximum-scale=1.0, minimum-scale=1.0\"> <meta http-equiv=\"X-UA-Compatible\" content=\"ie=edge\"> <title>Document</title> <link rel=\"stylesheet\" href=\"styles.css\"> </head> <body> <div class=\"border\"><h1 class=\"encabezado\"> 1 2 3 4 5 6 7 8 9 10 11 12 13 14 15 16 17 18 19 20 21 22 23 24 25 26 27 28 29 30 31 32 33 34 35 36 37 38 39 40 41 42 43 44 45 46 47 48 49 50 51 52 53 54 55 56 57 58 59 60 61 62 63 64 65 66 67 68 69 70 71 72 73 74 75 76 77 78 79 80 81 82 83 84 85 86 87 88 89 90 91 92 93 94 95 96 97 98 99 100</h1><h1>";
    private static string _html2 = "</h1><h1 class=\"encabezado\">1 2 3 4 5 6 7 8 9 10 11 12 13 14 15 16 17 18 19 20 21 22 23 24 25 26 27 28 29 30 31 32 33 34 35 36 37 38 39 40 41 42 43 44 45 46 47 48 49 50 51 52 53 54 55 56 57 58 59 60 61 62 63 64 65 66 67 68 69 70 71 72 73 74 75 76 77 78 79 80 81 82 83 84 85 86 87 88 89 90 91 92 93 94 95 96 97 98 99 100</h1></div></body></html>";
    public static string PrintMap() // Este metodo devuelve el mapa como una matriz de strings
    {
        _map = ""; // Reinicia el mapa antes de imprimir
        for (int i = 0; i < Map.ReturnLength0(); i++) // Recorre las filas del mapa
        {
            for (int j = 0; j < Map.ReturnLength1(); j++) // Recorre las columnas del mapa
            {
                _map += (Map.CheckMap(i, j) + " "); // Coloca el simbolo del mapa en la posicion actual
            }
            _map += "<br>"; // Añade un salto de línea al final de cada fila
        }
        return _html1 + _map + _html2; // Devuelve el mapa
    }
}