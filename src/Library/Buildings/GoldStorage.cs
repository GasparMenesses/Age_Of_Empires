using System.Data.SqlTypes;

namespace Library.Buildings;
using Core;

public class GoldStorage : Building //herdea de la clase building
{
    public int AlmacenaOro { get; set; }



    public GoldStorage(Resources resources,(int x, int y)posicion) :
        base(resources, woodCost:25, stoneCost:55,constructionTime:30,posicion) //constructor que define los costos de construccion del almacén, gastando piedra y madera.
                                                                    //Tambien define el tiempo que demora
    {
        AlmacenaOro = 0;
        

    }

    public void AlmacenarOro(int cantidad)
    {
        if (!IsBuilt)
            throw new InvalidOperationException(
                "El almacén aún no está construido."); // esto se hace por si el jugador quiere 
        // guardar recursos antes de que finalice la construccion del almacén

        AlmacenaOro += cantidad;
    }

    
}
   