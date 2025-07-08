using Library.Core;

namespace Library.Buildings;

/// <summary>
/// Representa un almacén de madera en el juego, donde se almacenan recursos de madera.
/// Cumple con SRP ya que se encarga exclusivamente de la lógica relacionada con el almacenamiento de madera.
/// </summary>
public class WoodStorage : Building
{
    /// <summary>
    /// Símbolo que representa el almacén de madera en el mapa.
    /// </summary>
    public override string Symbol => "🪵🏚️";

    /// <summary>
    /// Cantidad actual de madera almacenada.
    /// </summary>
    public int Wood { get; private set; }

    /// <summary>
    /// Capacidad máxima del almacén de madera.
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
    /// Constructor del almacén de madera. Define los costos, la capacidad y lo asocia al jugador.
    /// </summary>
    /// <param name="player">Jugador propietario del almacén.</param>
    /// <param name="position">Posición del almacén en el mapa.</param>
    public WoodStorage(Player player, (int x, int y) position) :
        base(woodCost: 25, stoneCost: 55, constructionTime: 30)
    {
        _player = player;
        Wood = 0; // inicializa la cantidad de madera almacenada en 0
        Capacity = 1000; // define la capacidad del almacén
        player.Resources.AddLimitResources(wood: true); // aumenta el límite de madera
        player.Buildings.Add(this, position); // agrega el edificio al jugador
        Map.ChangeMap(position, Symbol); // actualiza el mapa con el símbolo del almacén
    }

    /// <summary>
    /// Agrega madera al almacén respetando su capacidad y actualiza los recursos del jugador.
    /// </summary>
    /// <param name="wood">Cantidad de madera a almacenar.</param>
    /// <exception cref="InvalidOperationException">Se lanza si el almacén aún no está construido.</exception>
    public void AddWood(int wood)
    {
        if (!IsBuilt)
            throw new InvalidOperationException(
                "El almacén aún no está construido."); // previene almacenar antes de terminar la construcción

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
