namespace Library
{
    /// <summary>
    /// Representa la civilización Cordobeses en el juego, con su nombre, unidad única y bonificaciones específicas.
    /// </summary>
    public class Cordobeses : Civilization
    {
        /// <summary>
        /// Constructor que inicializa las propiedades específicas de la civilización Cordobeses.
        /// </summary>
        public Cordobeses()
        {
            NombreCivilizacion = "Cordobeses";
            TipoDeUnidadUnica = "Ferneh";
            Bonificacion = new Tuple<int, int, int, int>(0, 0, 0, 100);
            DescripcionBonificacion = "los cordobobeses te otorgan un bonus de 100 de comida al inicio de la partida.";
        }
    }
}