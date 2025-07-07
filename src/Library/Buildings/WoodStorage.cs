using Library.Core;
namespace Library.Buildings;

// Representa un almacén de madera en el juego, donde se almacenan recursos de madera
// Cumple con SRP ya que se encarga exclusivamente de la lógica relacionada con el almacenamiento de madera

public class WoodStorage : Building
{
    public override string Symbol => "🪵🏚️";
    public int Wood { get; private set; } //propiedad que define la cantidad de food almacenado
    public int Capacity { get; set; }
    public Player _player;


    public WoodStorage(Player player, (int x, int y) position) :
        base(position, woodCost: 25, stoneCost: 55,
            constructionTime: 30) //constructor que define los costos de construccion del almacén, gastando piedra y madera.
    //Tambien define el tiempo que demora
    {
        _player = player;
        Wood = 0; //inicializa la cantidad de food almacenado en 0
        Capacity = 1000; //define la capacidad del almacén
        player.Resources.AddLimitResources(wood: true); //aumenta el limite de food en 1000
        player.Buildings.Add(this); //agrega el edificio al jugador
    }

    public void AddWood(int wood)
    {
        if (!IsBuilt)
            throw new InvalidOperationException(
                "El almacén aún no está construido."); // esto se hace por si el jugador quiere 
        // guardar recursos antes de que finalice la construccion del almacén
        if ((Wood + wood) > Capacity)
        {
            wood = Capacity - Wood;
            Wood = Capacity;
        }
        else
            Wood += wood;

        _player.Resources.AddResources(wood: wood);
    }
    
}