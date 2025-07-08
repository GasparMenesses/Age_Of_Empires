namespace Library.Core;
using Buildings;
using Interfaces;
using Actions;

/// <summary>
/// Representa un jugador en el juego, con su nombre, recursos, edificios, civilización y unidades.
/// El jugador puede pertenecer a una civilización específica y tiene un límite de población, así como acciones disponibles.
/// Además, el jugador comienza con un centro cívico y puede construir otros edificios y unidades a lo largo del juego.
/// </summary>
public class Player
{
    /// <summary>
    /// Nombre del jugador.
    /// </summary>
    public string Nombre { get; set; }

    /// <summary>
    /// Identificador único del jugador.
    /// </summary>
    public string Id { get; set; }

    /// <summary>
    /// Recursos del jugador (comida, madera, piedra, oro, etc.).
    /// </summary>
    public Resources Resources { get; }

    /// <summary>
    /// Diccionario que asocia edificios con su posición en el mapa.
    /// </summary>
    public Dictionary<Building, (int x, int y)> Buildings { get; }

    /// <summary>
    /// Civilización a la que pertenece el jugador.
    /// </summary>
    public Civilization Civilization { get; set; }

    /// <summary>
    /// Lista de unidades que posee el jugador.
    /// </summary>
    public List<IUnit> Units { get; set; }

    /// <summary>
    /// Objeto interno para gestionar la civilización seleccionada.
    /// </summary>
    private Civilization _society;

    /// <summary>
    /// Límite máximo de población permitido para el jugador.
    /// </summary>
    public int PoblacionLimite { get; set; }

    /// <summary>
    /// Acciones que el jugador puede realizar en el juego.
    /// </summary>
    public Actions Actions { get; set; }

    /// <summary>
    /// Constructor que inicializa un nuevo jugador con nombre, civilización e id opcional.
    /// </summary>
    /// <param name="nombre">Nombre del jugador.</param>
    /// <param name="civilization">Nombre de la civilización seleccionada.</param>
    /// <param name="id">Identificador opcional del jugador.</param>
    /// <exception cref="Exception">Lanza excepción si la civilización es desconocida.</exception>
    public Player(string nombre, string civilization, string id = null)
    {
        switch (civilization)
        {
            case "Cordobeses":
                _society = new Cordobeses();
                break;
            case "Romanos":
                _society = new Romanos();
                break;
            case "Vikingos":
                _society = new Vikingos();
                break;
            default:
                throw new Exception("Civilización desconocida");
        }
        Id = id;
        Nombre = nombre;
        Buildings = new Dictionary<Building, (int x, int y)>();
        Resources = new Resources();
        Civilization = _society;
        Units = new List<IUnit>();
        Actions = new Actions(this);
        PoblacionLimite = 10;

        // Se añade un centro cívico en la posición (0,0) al inicio
        Buildings.Add(new CivicCenter(), (0, 0));    
    }
}
