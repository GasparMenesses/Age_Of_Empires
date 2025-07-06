using Discord.Commands;
using Library.Core;
// esta clase representa la fachada del juego, que es la interfaz principal para interactuar con el motor del juego
// La fachada simplifica la interacción con el motor, encapsulando la lógica de creación de jugadores y el entorno del juego.
// La clase también maneja la inicialización del motor y el inicio del bucle del juego.
namespace Facade;

public class Fachada
{
    static public List<Player> jugadores = new();
    public Engine engine;
    public Fachada() // Constructor de la fachada que inicializa el motor del juego
    {
        
        engine = new Engine(); // Inicializa el motor del juego
        // engine.CrearJugadores1();
        // engine.CreateNewGameMap(); // Crea un nuevo mapa de juego

    }

    public void CrearJugador(SocketCommandContext context , string selection ) // Método para iniciar el juego
    {
        
        string civilization = selection switch
        {
            "1" => "Cordobeses",
            "2" => "Romanos",
            "3" => "Vikingos"
        };
        Player player = new Player(context.User.Username, civilization,context.User.Id.ToString()); // Crea los jugadores y devuelve un mensaje de bienvenida);
        jugadores.Add(player); // Agrega el jugador a la lista de jugadores
        // Console.WriteLine("Creando entorno de juego...");
        // Thread.Sleep(2000);
        //
        // Console.WriteLine("Cargando mapa...");
        // Thread.Sleep(2000);
        //
        // engine.CreateNewGameMap(); // Llama al método para crear un nuevo mapa de juego
        //
        // engine.EmpezarLoop(); // Inicia el bucle del juego, donde se desarrollará la partida

    }

}