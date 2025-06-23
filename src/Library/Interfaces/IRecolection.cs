namespace Library.Interfaces;

public interface IRecolection
{
        public Dictionary<string, int> Position { get; set; } // indica la posición del recurso a recolectar
         static int CantidadRecursoDisponible { get; }  // cantidad de recurso disponible para recolectar
         static int TasaDeRecoleccion { get; } // cantidad de recurso que se recolecta 
        int Recolectar(int cantidad); // recolecta la cantidad de recurso indicada, retorna la cantidad recolectada
}