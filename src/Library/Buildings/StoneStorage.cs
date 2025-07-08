using Library.Core;

namespace Library.Buildings;

/// <summary>
/// Representa un almacén de piedra en el juego, donde se almacenan recursos de piedra.
/// Cumple con SRP ya que se encarga exclusivamente de la lógica relacionada con el almacenamiento de piedra.
/// </summary>
public class StoneStorage : Building // hereda de la clase Building
{
    /// <summary>
    /// Símbolo que representa el almacén de piedra en el mapa.
    /// </summary>
    public override string Symbol => "🪨🏚️";

    /// <summary>
    /// Cantidad actual de piedra almacenada.
    /// </summary>
    public int Stone { get; set; }

    /// <summary>
    /// Capacidad máxima del almacén de piedra.
    /// </summary>
    public int Capacity { get; set; }

    /// <summary>
    /// Costo de piedra necesario para construir el almacén.
    /// </summary>
    public new static int StoneCost => 55;

    /// <summary>
    /// Costo de madera necesario para construir el almacén.
    /// </summary>
    public new static int WoodCost => 50;

    /// <summary>
    /// Referencia al jugador propietario del almacén.
    /// </summary>
    public Player _player;

    /// <summary>
    /// Constructor del almacén de piedra. Define sus costos, capacidad y lo asocia al jugador.
    /// </summary>
    /// <param name="player">Jugador que construye el almacén.</param>
    /// <param name="position">Posición del almacén en el mapa.</param>
    public StoneStorage(Player player, (int x, int y) position) :
        base(woodCost: 25, stoneCost: 55, constructionTime: 30)
    {
        _player = player;
        Stone = 0; // inicializa la piedra almacenada en 0
        Capacity = 1000; // capacidad máxima del almacén
        player.Resources.AddLimitResources(stone: true); // aumenta el límite de almacenamiento de piedra
        player.Buildings.Add(this, position); // registra el edificio en el jugador
    }

    /// <summary>
    /// Agrega piedra al almacén, respetando la capacidad y actualiza los recursos del jugador.
    /// </summary>
    /// <param name="stone">Cantidad de piedra a almacenar.</param>
    /// <exception cref="InvalidOperationException">Se lanza si se intenta almacenar antes de finalizar la construcción.</exception>
    public void AddStone(int stone)
    {
        if (!IsBuilt)
            throw new InvalidOperationException(
                "El almacén aún no está construido."); // evita almacenar recursos antes de estar construido

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
