namespace Library.Farming;

public  abstract class Recolection
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
        {
            recurso_extraido = CantidadRecursoDisponible;
        }

        CantidadRecursoDisponible -= recurso_extraido;
        return recurso_extraido;
    }

    
}