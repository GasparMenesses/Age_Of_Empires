using Library.Core;

namespace Library.Buildings;

/// <summary>
/// Esta clase representa un edificio de tipo "Mill" (molino) en el juego, que almacena recursos de comida.
/// Cumple con SRP ya que se encarga exclusivamente de la lógica relacionada con el molino y el almacenamiento de comida.
/// </summary>
public class Mill : Building // hereda de la clase Building
{
    /// <summary>
    /// Símbolo que representa el molino en el mapa.
    /// </summary>
    public override string Symbol => "🌾🏠";

    /// <summary>
    /// Cantidad actual de comida almacenada.
    /// </summary>
    public int Food { get; private set; }

    /// <summary>
    /// Capacidad máxima del molino.
    /// </summary>
    public int Capacity { get; set; }

    /// <summary>
    /// Referencia al jugador propietario del molino.
    /// </summary>
    public Player _player;

    /// <summary>
    /// Constructor del molino. Define los costos de construcción, capacidad inicial y asocia el molino al jugador.
    /// </summary>
    /// <param name="player">Jugador que construye el molino.</param>
    /// <param name="position">Posición del molino en el mapa.</param>
    public Mill(Player player, (int x, int y) position) :
        base(woodCost: 25, stoneCost: 55, constructionTime: 30)
    {
        _player = player;
        Food = 0; // inicializa la comida almacenada en 0
        Capacity = 1000; // define la capacidad máxima
        player.Resources.AddLimitResources(food: true); // aumenta el límite de comida
        player.Buildings.Add(this, position); // registra el edificio en el jugador
    }

    /// <summary>
    /// Agrega comida al molino, respetando el límite de capacidad, y actualiza los recursos del jugador.
    /// </summary>
    /// <param name="food">Cantidad de comida a almacenar.</param>
    /// <exception cref="InvalidOperationException">Se lanza si el molino aún no está construido.</exception>
    public void AddFood(int food)
    {
        if (!IsBuilt)
            throw new InvalidOperationException(
                "El almacén aún no está construido."); // evita almacenar antes de terminar la construcción

        if ((Food + food) > Capacity)
        {
            food = Capacity - Food;
            Food = Capacity;
        }
        else
            Food += food;

        _player.Resources.AddResources(food: food);
    }
}
