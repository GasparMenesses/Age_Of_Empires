namespace Library.Interfaces;

public interface IRecolection
{
        (int x, int y) Posicion { get; } // indica la posición del recurso a recolectar
        int CantidadRecursoDisponible { get; }  // cantidad de recurso disponible para recolectar
        int TasaDeRecoleccion { get; } // cantidad de recurso que se recolecta 
        int Recolectar(int cantidad); // recolecta la cantidad de recurso indicada, retorna la cantidad recolectada
}