namespace Library.Farming
{
    /// <summary>
    /// Representa una cantera en el juego, que es un tipo de recolectable.
    /// La cantera tiene una posición en el mapa, una cantidad inicial de recursos y un tiempo de recolección específico.
    /// </summary>
    public class Quarry : Recolection
    {
        /// <summary>
        /// Símbolo que representa la cantera en el mapa.
        /// </summary>
        public override string Symbol => "⛏️🪨";

        /// <summary>
        /// Constructor que inicializa la cantera con una posición y cantidad inicial de recursos.
        /// </summary>
        /// <param name="posicion">Posición en el mapa (x, y).</param>
        /// <param name="cantidadinicial">Cantidad inicial de recursos disponibles en la cantera.</param>
        public Quarry((int x, int y) posicion, int cantidadinicial)
            : base(cantidadinicial, 120)
        {
        }
    }
}