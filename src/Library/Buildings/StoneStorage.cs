namespace Library.Buildings;
using Core;
public class StoneStorage : Building
{
    public int AlmacenaPiedra { get; set; }
   
    public StoneStorage(Resources resources) : base(resources,woodCost:50,stoneCost:100,60) //base le pasa a CC el  player
    {
        AlmacenaPiedra=0 ;
        
    }

    public void AlmacenarPiedra(int cantidad) // metodo para almacenar piedra
    {
        if (!IsBuilt)
            throw new InvalidOperationException(
                "El almacén aún no está construido."); // esto se hace por si el jugador quiere 
        // guardar recursos antes de que finalice la construccion del almacén
        AlmacenaPiedra += cantidad;
    }
    
   
}