using System.Text.Json.Serialization;

namespace Library
{
    /// <summary>
    /// Representa la civilización Vikingos en el juego, con su nombre, unidad única y bonificaciones específicas.
    /// </summary>
    public class Vikingos : Civilization
    {
        /// <summary>
        /// Constructor que inicializa las propiedades específicas de la civilización Vikingos.
        /// </summary>
        public Vikingos()
        {
            NombreCivilizacion = "Vikingos";
            TipoDeUnidadUnica = "Thor";
            Bonificacion = new Tuple<int, int, int, int>(100, 0, 0, 0);
            DescripcionBonificacion = "los vikingos tienen un bonus de 100 de madera al inicio de la partida";
        }
    }
}