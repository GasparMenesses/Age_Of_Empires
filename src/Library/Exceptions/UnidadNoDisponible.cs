namespace Library.Exceptions;

public class UnidadNoDisponibleException : Exception
{
    public UnidadNoDisponibleException(string mensaje) : base(mensaje) { }
}