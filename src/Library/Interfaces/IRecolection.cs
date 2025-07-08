namespace Library.Interfaces
{
    /// <summary>
    /// Define las operaciones básicas para recolectar recursos en el juego.
    /// </summary>
    public interface IRecolection
    {
        /// <summary>
        /// Cantidad de recurso disponible para recolectar.
        /// </summary>
        static int CantidadRecursoDisponible { get; }

        /// <summary>
        /// Cantidad máxima de recurso que se puede recolectar por acción.
        /// </summary>
        static int TasaDeRecoleccion { get; }

        /// <summary>
        /// Recolecta la cantidad indicada de recurso.
        /// </summary>
        /// <param name="cantidad">Cantidad que se desea recolectar.</param>
        /// <returns>Cantidad realmente recolectada.</returns>
        int Recolectar(int cantidad);
    }
}