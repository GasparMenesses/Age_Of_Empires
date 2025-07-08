namespace Library.Interfaces
{
    /// <summary>
    /// Define las operaciones básicas para un edificio en el juego, como la construcción y el estado de construcción.
    /// </summary>
    public interface IBuildable
    {
        /// <summary>
        /// Indica si el edificio está construido.
        /// </summary>
        bool IsBuilt { get; }

        /// <summary>
        /// Inicia o avanza el proceso de construcción del edificio.
        /// </summary>
        /// <param name="time">Tiempo que se suma al progreso de construcción (en segundos).</param>
        void Construyendo(int time);
    }
}