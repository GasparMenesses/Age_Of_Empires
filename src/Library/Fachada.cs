using Discord.Commands;
using Library;
using Library.Buildings;
using Library.Core;
using Library.Exceptions;
using Library.Units;
using Library.Interfaces;
using Library.Actions;
using Library.Farming;
using Library.Buildings;

// esta clase representa la fachada del juego, que es la interfaz principal para interactuar con el motor del juego
// La fachada simplifica la interacción con el motor, encapsulando la lógica de creación de jugadores y el entorno del juego.
// La clase también maneja la inicialización del motor y el inicio del bucle del juego.
namespace Facade;

public class Fachada
{
    static public List<Player> jugadores = new();
    public Dictionary<Recolection, (int x, int y)> recolection = new();
    public Engine engine;
    
    public Fachada() // Constructor de la fachada que inicializa el motor del juego
    {
        
        engine = new Engine(); // Inicializa el motor del juego
        // engine.CrearJugadores1();
        // engine.CreateNewGameMap(); // Crea un nuevo mapa de juego

    }

    public async Task CrearJugador(SocketCommandContext context , string selection ) // Método para iniciar el juego
    {
        string civilization = selection switch
        {
            "1" => "Cordobeses",
            "2" => "Romanos",
            "3" => "Vikingos",
            _ => throw new ArgumentException("Selección inválida de civilización.")
        };
        Player player = new Player(context.User.Username, civilization,context.User.Id.ToString()); // Crea los jugadores y devuelve un mensaje de bienvenida);
        player.Resources.AddLimitResources(true, true, true, true); // Aumenta el límite de cada recurso
        var rand = new Random();
        int x, y;
        do
        {
            x = rand.Next(1, 100);
            y = rand.Next(1, 100);
        } while (Map.CheckMap(x,y) != "..");
        player.Buildings[player.Buildings.Keys.First()] = (x,y);
        jugadores.Add(player); // Agrega el jugador a la lista de jugadores
    }
    
    public void CrearEntornoJuego()
    {
        engine.CreateNewGameMap(  ); // Crea un nuevo mapa de juego
        engine.PlaceResourcesRandomInGameMap(jugadores, recolection); // Coloca edificios aleatorios en el mapa del juego, depende de la cantidad de jugadores
        engine.AsignarTresAldeanosPorJugador(jugadores); // Asigna tres aldeanos por jugador, para que puedan recolectar recursos
        engine.RefreshMap();
    }

    public void Recolectar(string selection, Player _player) // Método para recolectar recursos según la selección del jugador
    {
        string resource = selection switch
        {
            "1" => "madera",
            "2" => "piedra",
            "3" => "oro",
            "4" => "comida",
            _ => throw new InvalidOperationException("Recurso no válido")
        };
        engine.Recolectar(_player, resource);
    }


    public void ConstruirAlmacenPiedra( int x, int y, Player _player) // Método para construir un almacén de piedra
    {
        if (_player.Resources.Stone >= StoneStorage.StoneCost && _player.Resources.Wood >= StoneStorage.WoodCost) // Verifica si el jugador tiene suficientes recursos para construir un almacén de piedra
        {
            _player.Actions.Build("StoneStorage", (x, y)); // Construye un almacén de piedra en la posición especificada
        }
        else
        {
            throw new RecursosInsuficientesException("No tienes suficientes recursos para construir un almacén de piedra. Cuesta 55 de piedra y 50 de madera.");
        }
    }
    
    public void ConstruirAlmacenMadera( int x, int y, Player _player) // Método para construir un almacén de piedra
    {
        if (_player.Resources.Stone >= WoodStorage.StoneCost && _player.Resources.Wood >= WoodStorage.WoodCost) // Verifica si el jugador tiene suficientes recursos para construir un almacén de piedra
        {
            _player.Actions.Build("WoodStorage", (x, y)); // Construye un almacén de piedra en la posición especificada
        }
        else
        {
            throw new RecursosInsuficientesException("No tienes suficientes recursos para construir un almacén de madera. Cuesta 55 de piedra y 50 de madera.");
        }
    }
    
    public void ConstruirAlmacenOro( int x, int y, Player _player) // Método para construir un almacén de piedra
    {
        if (_player.Resources.Stone >= GoldStorage.StoneCost && _player.Resources.Wood >= GoldStorage.WoodCost) // Verifica si el jugador tiene suficientes recursos para construir un almacén de piedra
        {
            _player.Actions.Build("GoldStorage", (x, y)); // Construye un almacén de oro en la posición especificada
        }
        else
        {
            throw new RecursosInsuficientesException("No tienes suficientes recursos para construir un almacén de oro. Cuesta 55 de piedra y 50 de madera.");
        }
    }

    

    public void ActualizarMapa()
    {
        engine.RefreshMap();
    }
    
    public void AtacarUnidades(List<IUnit> atacantes, List<IUnit> atacados)
    {
        var jugador = jugadores.FirstOrDefault(j => atacantes.All(a => j.Units.Contains(a)));
        if (jugador != null)
        {
            var actions = new Actions(jugador);
            actions.AtacarUnidades(atacantes, atacados);
        }
    }
    
    
}

