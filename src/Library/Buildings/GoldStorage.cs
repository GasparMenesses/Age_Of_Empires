using Library.Core;

namespace Library.Buildings;

/// <summary>
/// Representa un almacén de oro en el juego, donde se almacena el oro recolectado.
/// Cumple con SRP ya que se encarga exclusivamente de la lógica relacionada con el almacenamiento de oro.
/// </summary>
public class GoldStorage : Building //hereda de la clase Building
{
    /// <summary>
    /// Símbolo del almacén de oro en el mapa.
    /// </summary>
    public override string Symbol => "\ud83d\udcb0";

    /// <summary>
    /// Cantidad actual de oro almacenado.
    /// </summary>
    public int Gold { get; private set; }

    /// <summary>
    /// Capacidad máxima del almacén de oro.
    /// </summary>
    public int Capacity { get; set; }

    /// <summary>
    /// Referencia al jugador propietario del almacén.
    /// </summary>
    public Player _player;
    
    /// <summary>
    /// Costo de piedra necesario para construir el almacén.
    /// </summary>
    public new static int StoneCost => 55;

    /// <summary>
    /// Costo de madera necesario para construir el almacén.
    /// </summary>
    public new static int WoodCost => 50;

    /// <summary>
    /// Constructor del almacén de oro. Define sus costos de construcción y lo registra en el jugador.
    /// </summary>
    /// <param name="player">Jugador propietario del edificio.</param>
    /// <param name="position">Posición del almacén en el mapa.</param>
    public GoldStorage(Player player, (int x, int y) position) :
        base(woodCost: 25, stoneCost: 55, constructionTime: 30) //constructor que define los costos de construcción del almacén
    {
        _player = player;
        Gold = 0; //inicializa la cantidad de oro almacenado en 0
        Capacity = 1000; //define la capacidad del almacén
        player.Resources.AddLimitResources(gold: true); //aumenta el límite de oro en 1000
        player.Buildings.Add(this, position); //agrega el edificio al jugador
    }

    /// <summary>
    /// Agrega oro al almacén, sin superar su capacidad, y actualiza los recursos del jugador.
    /// </summary>
    /// <param name="gold">Cantidad de oro a almacenar.</param>
    /// <exception cref="InvalidOperationException">Se lanza si se intenta almacenar oro antes de que el edificio esté construido.</exception>
    public void AddGold(int gold)
    {
        if (!IsBuilt)
            throw new InvalidOperationException(
                "El almacén aún no está construido."); // esto se hace por si el jugador quiere 
        // guardar recursos antes de que finalice la construcción del almacén

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
