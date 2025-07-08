namespace Library.Interfaces
{
    /// <summary>
    /// Define las propiedades y métodos necesarios para manejar la información de construcción de un edificio en el juego.
    /// </summary>
    public interface IConstructionInfo
    {
        /// <summary>
        /// Costo en madera para construir el edificio.
        /// </summary>
        int WoodCost { get; set; }

        /// <summary>
        /// Costo en piedra para construir el edificio.
        /// </summary>
        int StoneCost { get; set; }

        /// <summary>
        /// Tiempo total requerido para construir el edificio (en segundos).
        /// </summary>
        int ConstructionTime { get; }

        /// <summary>
        /// Tiempo transcurrido en la construcción del edificio (en segundos).
        /// </summary>
        int TimeElapsed { get; }

        /// <summary>
        /// Método para avanzar la construcción del edificio sumando segundos al progreso.
        /// </summary>
        /// <param name="seconds">Segundos a añadir al progreso de construcción.</param>
        void Construyendo(int seconds);

        /// <summary>
        /// Vida actual del edificio.
        /// </summary>
        int Health { get; set; }
    }
}