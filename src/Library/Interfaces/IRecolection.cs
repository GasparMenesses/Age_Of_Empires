namespace Library.Interfaces;

// Esta interfaz define las operaciones básicas para recolectar recursos en el juego.

public interface IRecolection
{
    static int CantidadRecursoDisponible { get; }  // cantidad de recurso disponible para recolectar
    static int TasaDeRecoleccion { get; } // cantidad de recurso que se recolecta 
    
    int Recolectar(int cantidad); // recolecta la cantidad de recurso indicada, retorna la cantidad recolectada
}