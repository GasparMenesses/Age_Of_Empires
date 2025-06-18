namespace Library.Interfaces;

public interface IRecolection
{
        (int x, int y) Posicion { get; } 
        int CantidadRecursoDisponible { get; }
        int TasaDeRecoleccion { get; }
        int Recolectar(int cantidad);
}