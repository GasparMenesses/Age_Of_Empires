namespace Library.Exceptions
{
    /// <summary>
    /// Excepción que se lanza cuando un jugador no tiene recursos suficientes para realizar una acción.
    /// </summary>
    public class RecursosInsuficientesException : Exception
    {
        /// <summary>
        /// Constructor que inicializa la excepción con un mensaje específico.
        /// </summary>
        /// <param name="mensaje">Mensaje que describe el error.</param>
        public RecursosInsuficientesException(string mensaje) : base(mensaje) { }
    }
}