using System.Net.Sockets;
using System.Xml;
using Library.Interfaces;

namespace Library.Buildings;
using Core;

public class WoodStorage : Building
{
    public int AlmacenaMadera { get; set; }
    

public WoodStorage(Resources resources) : base(resources,120,75,60) 
    {
        AlmacenaMadera=0 ;
         
    }

    public void AlmacenarMadera(int cantidad)
    {   
        if (!IsBuilt)
            throw new InvalidOperationException(
                "El almacén aún no está construido."); // esto se hace por si el jugador quiere 
        // guardar recursos antes de que finalice la construccion del almacén
        AlmacenaMadera += cantidad;
    }

}