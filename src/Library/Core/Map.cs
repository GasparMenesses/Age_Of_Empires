using Library.Buildings;
using Library.Farming;

namespace Library.Core
{
    /// <summary>
    /// Representa el mapa del juego, donde se colocan los edificios y recolectables.
    /// </summary>
    public class Map
    {
        /// <summary>
        /// Matriz estática que representa el tablero del mapa.
        /// </summary>
        private static string[,] Board = new string[100, 100];

        /// <summary>
        /// Constructor estático que inicializa el tablero con posiciones vacías ("..").
        /// </summary>
        static Map()
        {
            for (int i = 0; i < 100; i++)
                for (int j = 0; j < 100; j++)
                    Board[i, j] = "..";
        }

        /// <summary>
        /// Consulta el símbolo que hay en la posición (x, y) del tablero.
        /// </summary>
        /// <param name="x">Coordenada x.</param>
        /// <param name="y">Coordenada y.</param>
        /// <returns>El símbolo en la posición dada o null si está fuera del rango.</returns>
        public static string CheckMap(int x, int y)
        {
            if (x < 0 || x >= Board.GetLength(0) || y < 0 || y >= Board.GetLength(1))
                return null;
            return Board[x, y];
        }

        /// <summary>
        /// Cambia el símbolo en la posición dada del tablero.
        /// </summary>
        /// <param name="position">Tupla con las coordenadas (x, y).</param>
        /// <param name="simbolo">Símbolo a colocar en el tablero.</param>
        public static void ChangeMap((int x, int y) position, string simbolo)
        {
            Board[position.x, position.y] = simbolo;
        }

        /// <summary>
        /// Devuelve la longitud del tablero en la dimensión 0 (filas).
        /// </summary>
        /// <returns>Entero con la cantidad de filas del tablero.</returns>
        public static int ReturnLength0()
        {
            return Board.GetLength(0);
        }

        /// <summary>
        /// Devuelve la longitud del tablero en la dimensión 1 (columnas).
        /// </summary>
        /// <returns>Entero con la cantidad de columnas del tablero.</returns>
        public static int ReturnLength1()
        {
            return Board.GetLength(1);
        }

        /// <summary>
        /// Devuelve la matriz que representa el tablero completo.
        /// </summary>
        /// <returns>Matriz bidimensional de strings con el tablero.</returns>
        public static string[,] ReturnBoard()
        {
            return Board;
        }
    }
}
