namespace Library.Exceptions;

public class RecursosInsuficientesException : Exception
{
    public RecursosInsuficientesException(string mensaje) : base(mensaje) { }
}