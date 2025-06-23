using Library.Core;
namespace Library.Buildings;

public class StoneStorage : Building //herdea de la clase building
{
    public new static string Symbol => "SS";
    public int Stone { get;  set; } //propiedad que define la cantidad de piedra almacenado
    public int Capacity { get; set; }
    public   new static int StoneCost => 55; //costo de piedra para construir el almacén
    public Player _player;


    public StoneStorage(Player player, (int x, int y) position) :
        base(position, woodCost: 25, stoneCost: 55,
            constructionTime: 30) //constructor que define los costos de construccion del almacén, gastando piedra y madera.
    //Tambien define el tiempo que demora
    {
        _player = player;
        Stone = 0; //inicializa la cantidad de food almacenado en 0
        Capacity = 1000; //define la capacidad del almacén
        player.Resources.AddLimitResources(stone: true); //aumenta el limite de food en 1000
        player.Buildings.Add(this); //agrega el edificio al jugador
    }

    public void AddStone(int stone)
    {
        if (!IsBuilt)
            throw new InvalidOperationException(
                "El almacén aún no está construido."); // esto se hace por si el jugador quiere 
        // guardar recursos antes de que finalice la construccion del almacén
        if ((Stone + stone) > Capacity)
        {
            stone = Capacity - Stone;
            Stone = Capacity;
        }
        else
            Stone += stone;

        _player.Resources.AddResources(stone: stone);
    }
}
