using Library.Interfaces;

namespace Library.Farming;

public  abstract class Recolection : IRecolection
{
    public  static int CantidadRecursoDisponible { get; set; }
    public  static int TasaDeRecoleccion { get; set; }
    public Dictionary<string, int> Position { get; set; }
    
    public Recolection((int x, int y )position, int cantidadinicial, int tasarecoleccion)
    {
        
        Position = new Dictionary<string, int>
        {
            { "x", position.x },
            { "y", position.y }
        };
        CantidadRecursoDisponible = cantidadinicial;
        TasaDeRecoleccion = tasarecoleccion;
    }
 

 
    public int Recolectar(int cantidad)
    {
        int recursoExtraido;
        if (cantidad <= CantidadRecursoDisponible)
        {
            recursoExtraido = TasaDeRecoleccion;
        }
        else
        {                                                        //Evalúa si la cantidad solicitada es menor o igual a la disponible; si es así, permite recolectar la tasa máxima de recolección. 
                                                                //Si no, permite extraer solo lo que queda. Luego, descuenta lo extraído del total disponible y retorna la cantidad recolectada.
            recursoExtraido = CantidadRecursoDisponible;
        }

        CantidadRecursoDisponible -= recursoExtraido;
        return recursoExtraido;
    }

    
}