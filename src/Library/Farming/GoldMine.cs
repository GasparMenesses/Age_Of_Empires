namespace Library.Farming
{
    /// <summary>
    /// Representa una mina de oro en el juego, que es un tipo de recolectable.
    /// La mina de oro tiene una posición en el mapa, una cantidad inicial de recursos y un tiempo de recolección específico.
    /// </summary>
    public class GoldMine : Recolection
    {
        /// <summary>
        /// Símbolo que representa la mina de oro en el mapa.
        /// </summary>
        public override string Symbol => "⛏️💰";

        /// <summary>
        /// Constructor que inicializa la mina de oro con una posición y cantidad inicial de recursos.
        /// </summary>
        /// <param name="posicion">Posición en el mapa (x, y).</param>
        /// <param name="cantidadinicial">Cantidad inicial de recursos disponibles en la mina.</param>
        public GoldMine((int x, int y) posicion, int cantidadinicial)
            : base(cantidadinicial, 120)
        {
        }
    }
}