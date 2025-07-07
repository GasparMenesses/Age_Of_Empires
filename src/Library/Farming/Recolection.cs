using Library.Interfaces;
namespace Library.Farming;

// Esta clase abstracta representa un recolectable en el juego, como una mina de oro, cantera o granja.
// Contiene propiedades y métodos comunes para todos los tipos de recolectables, como la posición, cantidad de recursos disponibles y tasa de recolección.

public  abstract class Recolection : IRecolection
{
    public  static int CantidadRecursoDisponible { get; set; }
    public  static int TasaDeRecoleccion { get; set; }
    public virtual string Symbol { get; set; }
    
    public Recolection(int cantidadinicial, int tasarecoleccion)
    {
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