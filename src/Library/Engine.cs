using Library.Actions;
using Library.Exceptions;
using Library.Core;
using Library.Farming;
using Library.Units;
using Library.Buildings;


public class Engine
{
    
    //  INICIALIZO VARIABLES
    public DateTime HoraInicio { get; private set; }  // Hora de inicio del juego
    public int CantidadJugadores { get; private set; } // Cantidad de jugadores en la partida
    public List<Player> Jugadores { get; private set; } = new List<Player>(); // Lista de jugadores en la partida
    private static Random rand = new Random();
    
    // Ruta del archivo HTML del mapa
    private static string RelativeMapURL = "../../../../../MapaHtml/mapa_generado.html";
    private static string AbstoluteMapURL = Path.GetFullPath(RelativeMapURL).Replace("\\", "/");

    public void CreateNewGameMap()
    {
        new Map();
    }
/// 
/// 
///
    public void PlaceResourcesRandomInGameMap(List<Player> jugadores, Dictionary<Recolection, (int x, int y)> recolection) 
    {
        
        foreach (var jugador in jugadores)
        {
            // Por cada jugador agrego 3 minas de oro al mapa
            for (int i = 0; i < 3; i++)
            {
                var minaoro = new GoldMine((0, 0), 500); 
                PlaceRandom(recolection, minaoro);
            }
            
            // Por cada jugador agrego 5 woods al mapa
            for (int i = 0; i < 5; i++)
            {
                var wood = new Woods((0, 0), 500); 
                PlaceRandom(recolection, wood);
            }
            
            // Por cada jugador agrego 5 minas de piedra al mapa
            for (int i = 0; i < 5; i++)
            {
                var minapiedra = new Quarry((0, 0), 500); 
                PlaceRandom(recolection, minapiedra);
            }
            
            // Por cada jugador agrego 5 granjas al mapa
            for (int i = 0; i < 5; i++)
            {
                var granja = new Farm((0, 0), 500);
                PlaceRandom(recolection, granja);
            }
            
        }
        
    } // Crea un nuevo mapa para el juego, colocando los recursos iniciales


    private static void PlaceRandom(Dictionary<Recolection, (int x, int y)> recolection, Recolection resource)
    {
        int x, y;
        do
        {
            x = rand.Next(1, 100);
            y = rand.Next(1, 100);
        } while (Map.CheckMap(x,y) != "..");

        Map.ChangeMap((x, y), resource.Symbol);
        
        recolection[resource] = (x, y); // Asigna la posición del recurso en el diccionario de recolección
    }
///
///
/// 

    public void RefreshMap()
    {
        string mapaComoTexto = MapPrinter.PrintMap();
        string ruta = AbstoluteMapURL;
        File.WriteAllText(ruta, mapaComoTexto);
    }
    
    
    public void AsignarTresAldeanosPorJugador(List<Player> jugadores) // Asigna 3 aldeanos a cada jugador al inicio del juego
    {
        foreach (Player jugador in jugadores)
        {
            // Cada jugador comienza con 3 aldeanos
            for (int i = 0; i < 3; i++)
            {
                jugador.Units.Add(new Villager(jugador,(jugador.Buildings.Values.First().x,jugador.Buildings.Values.First().y))); // Asigna un aldeano al centro cívico del jugador
            }
        }
 
    } // Asigna 3 aldeanos a cada jugador al inicio del juego

    public async Task Recolectar(Player _player, string resource)
    {
        var villager = _player.Units.OfType<Villager>().FirstOrDefault();
        if (villager != null)
        {
            // Recolecta el recurso especificado por un aldeano e hinabilita al aldeano hasta que se complete la recolección
            _player.Units.Remove(villager); // Elimina al aldeano de la lista de unidades del jugador ya que no puede realizar ninguna acción hasta que termine de recolectar
            await _player.Actions.Farmear(villager, resource);//Recolecta el recurso especificado por un aldeano
            _player.Units.Add(villager); // Vuelve a agregar al aldeano a la lista de unidades del jugador una vez que ha terminado de recolectar
        }
        else
        {
            throw new UnidadNoDisponibleException("No tienes ningun aldeano disponible para farmear.");
        }
    }
    

}