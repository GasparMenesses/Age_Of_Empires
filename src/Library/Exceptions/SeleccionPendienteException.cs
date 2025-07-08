namespace Library.Exceptions
{
    /// <summary>
    /// Excepción que se lanza cuando una selección está pendiente y se intenta realizar una acción inválida.
    /// </summary>
    public class SeleccionPendienteException : Exception
    {
        /// <summary>
        /// Constructor que inicializa la excepción con un mensaje específico.
        /// </summary>
        /// <param name="mensaje">Mensaje que describe el error.</param>
        public SeleccionPendienteException(string mensaje) : base(mensaje) { }
    }
}