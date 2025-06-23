using Library.Core;
namespace Library.Buildings;

public class GoldStorage : Building //herdea de la clase building
{
    public new static string Symbol => "GS";
    public int Gold { get; private set; } //propiedad que define la cantidad de oro almacenado
    public int Capacity { get; set; }
    public Player _player;
    

    public GoldStorage(Player player,(int x, int y)position) :
        base(position, woodCost:25, stoneCost:55,constructionTime:30) //constructor que define los costos de construccion del almacén, gastando piedra y madera.
                                                                    //Tambien define el tiempo que demora
    {
        _player = player;
        Gold = 0; //inicializa la cantidad de oro almacenado en 0
        Capacity = 1000; //define la capacidad del almacén
        player.Resources.AddLimitResources(gold: true); //aumenta el limite de oro en 1000
        player.Buildings.Add(this); //agrega el edificio al jugador
    }
    public void AddGold(int gold)
    {
        if (!IsBuilt)
            throw new InvalidOperationException(
                "El almacén aún no está construido."); // esto se hace por si el jugador quiere 
        // guardar recursos antes de que finalice la construccion del almacén
        if ((Gold + gold) > Capacity)
        {
            gold = Capacity - Gold;
            Gold = Capacity;
        }
        else
            Gold += gold;
        _player.Resources.AddResources(gold: gold);
    }
}
   