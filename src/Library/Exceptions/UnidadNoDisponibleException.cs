namespace Library.Exceptions
{
    /// <summary>
    /// Excepción que se lanza cuando una unidad no está disponible para realizar una acción.
    /// </summary>
    public class UnidadNoDisponibleException : Exception
    {
        /// <summary>
        /// Constructor que inicializa la excepción con un mensaje específico.
        /// </summary>
        /// <param name="mensaje">Mensaje que describe el error.</param>
        public UnidadNoDisponibleException(string mensaje) : base(mensaje) { }
    }
}