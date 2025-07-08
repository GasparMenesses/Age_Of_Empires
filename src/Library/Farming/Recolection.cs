using Library.Interfaces;
namespace Library.Farming
{
    /// <summary>
    /// Clase abstracta que representa un recolectable en el juego, como una mina de oro, cantera o granja.
    /// Contiene propiedades y métodos comunes para todos los tipos de recolectables, como la cantidad de recurso disponible y la tasa de recolección.
    /// </summary>
    public abstract class Recolection : IRecolection
    {
        /// <summary>
        /// Cantidad total de recurso disponible para recolectar.
        /// </summary>
        public static int CantidadRecursoDisponible { get; set; }

        /// <summary>
        /// Tasa máxima de recolección por acción.
        /// </summary>
        public static int TasaDeRecoleccion { get; set; }

        /// <summary>
        /// Símbolo que representa el recolectable en el mapa (puede ser sobreescrito por clases derivadas).
        /// </summary>
        public virtual string Symbol { get; set; }

        /// <summary>
        /// Constructor que inicializa la cantidad inicial de recurso y la tasa de recolección.
        /// </summary>
        /// <param name="cantidadinicial">Cantidad inicial de recurso disponible.</param>
        /// <param name="tasarecoleccion">Tasa máxima de recolección.</param>
        public Recolection(int cantidadinicial, int tasarecoleccion)
        {
            CantidadRecursoDisponible = cantidadinicial;
            TasaDeRecoleccion = tasarecoleccion;
        }

        /// <summary>
        /// Recolecta la cantidad especificada de recurso, respetando la cantidad disponible y la tasa máxima.
        /// </summary>
        /// <param name="cantidad">Cantidad que se desea recolectar.</param>
        /// <returns>Cantidad realmente recolectada.</returns>
        public int Recolectar(int cantidad)
        {
            int recursoExtraido;
            if (cantidad <= CantidadRecursoDisponible)
            {
                recursoExtraido = TasaDeRecoleccion;
            }
            else
            {
                // Evalúa si la cantidad solicitada es menor o igual a la disponible; si es así, permite recolectar la tasa máxima de recolección.
                // Si no, permite extraer solo lo que queda.
                recursoExtraido = CantidadRecursoDisponible;
            }

            CantidadRecursoDisponible -= recursoExtraido;
            return recursoExtraido;
        }
    }
}
