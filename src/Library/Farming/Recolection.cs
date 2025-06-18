using Library.Interfaces;

namespace Library.Farming;

public  abstract class Recolection : IRecolection
{
    public (int x, int y )Posicion { get; }
    public int CantidadRecursoDisponible { get; set; }
    public int TasaDeRecoleccion { get; set; }
    
    public Recolection((int x, int y )posicion, int cantidadinicial, int tasarecoleccion)
    {
        Posicion = posicion;
        CantidadRecursoDisponible = cantidadinicial;
        TasaDeRecoleccion = tasarecoleccion;
    }
 

 
    public int Recolectar(int cantidad)
    {
        int recurso_extraido;
        if (cantidad <= CantidadRecursoDisponible)
        {
            recurso_extraido = TasaDeRecoleccion;
        }
        else
        {                                                        //Evalúa si la cantidad solicitada es menor o igual a la disponible; si es así, permite recolectar la tasa máxima de recolección. 
                                                                //Si no, permite extraer solo lo que queda. Luego, descuenta lo extraído del total disponible y retorna la cantidad recolectada.
            recurso_extraido = CantidadRecursoDisponible;
        }

        CantidadRecursoDisponible -= recurso_extraido;
        return recurso_extraido;
    }

    
}