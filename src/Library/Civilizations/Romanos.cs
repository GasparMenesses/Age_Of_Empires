namespace Library
{
    /// <summary>
    /// Representa la civilización Romanos en el juego, con su nombre, unidad única y bonificaciones específicas.
    /// </summary>
    public class Romanos : Civilization
    {
        /// <summary>
        /// Constructor que inicializa las propiedades específicas de la civilización Romanos.
        /// </summary>
        public Romanos()
        {
            NombreCivilizacion = "Romanos";
            TipoDeUnidadUnica = "JulioCesar";
            Bonificacion = new Tuple<int, int, int, int>(0, 0, 50, 0);
            DescripcionBonificacion = "los romanos tienen un bonus de 50 de oro al inicio de la partida";
        }
    }
}