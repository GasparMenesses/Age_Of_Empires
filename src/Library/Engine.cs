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
        string ruta = @"C:\proyectosP2\Age_Of_Empires\MapaHtml\mapa_generado.html";
        File.WriteAllText(ruta, mapaComoTexto);
    }
    
    
    public void AsignarTresAldeanosPorJugador(List<Player> jugadores) // Asigna 3 aldeanos a cada jugador al inicio del juego
    {
        foreach (Player jugador in jugadores)
        {
            // Cada jugador comienza con 3 aldeanos
            for (int i = 0; i < 3; i++)
            {
                jugador.Units.Add(new Villager(jugador,jugador.Buildings.Keys.First())); // Asigna un aldeano al centro cívico del jugador
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
    
    

    
    
 ///////////////////////////////////////////////////////////////////////////////////////////////////////
 //////////////////////////////////////////////////////////////////////////////////////////////////////
 //////////////////////////////////////////////////////////////////////////////////////////////////////  
 
    
    
    // FUNCIONES
    
     public string CrearJugadores( string username, string civilization = "Cordobeses") // Crea un jugador con el nombre y civilización especificados
    {
        Jugadores.Add(new Player(username, civilization));
        return "Se ha creado el jugador " + username + " con la civilización " + civilization + ".";
    }
    
    /// <summary>
    /// Permite al jugador seleccionar una civilización de entre las disponibles.
    /// </summary>
    private string SeleccionarCivilizacion()
    {
        while (true)
        {
            Console.WriteLine("\nSeleccione su civilización:\n1 - Cordobeses\n2 - Romanos\n3 - Vikingos");
            string op = Console.ReadLine();
            switch (op)
            {
                case "1": return "Cordobeses";
                case "2": return "Romanos";
                case "3": return "Vikingos";
                default: Console.WriteLine("Opción inválida.\n"); break;
            }
        }
    }  // Permite al jugador seleccionar su civilización


    /// <summary>
    /// Crea un nuevo mapa de juego, colocando edificios y recursos iniciales para cada jugador.
    /// </summary>
    
    
    /// <summary>
    /// Permite al jugador mover sus unidades a una nueva posición ingresada por coordenadas.
    /// </summary>
    public void MoverUnidadees(Player jugador) // Permite al jugador mover sus unidades a una nueva posición en el mapa
    {
        Console.WriteLine("\nIngrese la posición (x, y) a la que desea mover sus unidades:");
        int x = -1, y = -1;
        bool posicionValida = false;

        // Validación de la posición ingresada, asegurando que sea un par de números enteros separados por coma
        while (!posicionValida)
        {
            string input = Console.ReadLine();
            string[] posicion = input.Split(','); // Separa la entrada por coma

            if (posicion.Length == 2 &&
                int.TryParse(posicion[0], out x) &&
                int.TryParse(posicion[1], out y))
            {
                posicionValida = true;
            }
            else
            {
                Console.WriteLine("Posición inválida, ingrese una posición válida (x, y) separada por coma.");
            }
        }
        
        
        // Acá movemos unidades
        Console.WriteLine($"Unidades movidas a la posición ({x}, {y}).");
    }
    
    /// <summary>
    /// Permite al jugador construir un edificio especificando el tipo y la ubicación.
    /// </summary>
    public async Task ConstruirEdificios(Player jugador)
    {
        Console.WriteLine("Construyendo edificios...");
        
        Console.WriteLine("Edificios disponibles:\n 1 - Centro Cívico\n 2 - Cuartel\n 3 - Establo");
        string edificio = "0";
        
        
        while (edificio!= "1" && edificio != "2" && edificio != "3" && edificio != "4")
        {
            Console.WriteLine("\nIngrese el edificio a construir:\n 1 - Almacen de oro\n 2 - Almacen de piedra\n 3 - Almacen de madera\n 4 - Molino \n 5 - Cuartel \n 6 - Casa");
            edificio = Console.ReadLine();
            if (edificio != "1" && edificio != "2" && edificio != "3" && edificio != "4" && edificio != "5" && edificio != "6")
            {
                Console.WriteLine("\nEdificio inválido. Por favor, ingrese un número del 1 al 6.");
            }
            
        }
        string nombreedificio= edificio switch
        {
            "1" => "GoldStorage",
            "2" => "StoneStorage",
            "3" => "WoodStorage",
            "4" => "Mill",
            "5" => "Barracks",
            "6" => "House",
            _ => throw new InvalidOperationException("Edificio no válido")
        };
        Console.WriteLine("ingrese la posición (x, y) donde desea construir el edificio:");
        int x= int.Parse(Console.ReadLine());
        int y = int.Parse(Console.ReadLine());
        var actions = new Actions(jugador);
        Console.WriteLine("\nConstruyendo edificio...");
        bool construido =  await actions.Build(nombreedificio, (x, y)); // Llama al método Build de la clase Actions para construir el edificio en la posición especificada
        if (construido)
        {
            Console.WriteLine($"\nEdificio {nombreedificio} construido exitosamente en la posición ({x}, {y}).");
        }
        else
        {
            Console.WriteLine("\nNo se pudo construir el edificio. Verifique los recursos o la posición.");
        }
        
        
    } // Permite al jugador construir edificios en el mapa, verificando los recursos necesarios y la posición
    
    
    
    /// <summary>
    /// Acción de atacar unidades enemigas. No implementado.
    /// </summary>
    public void AtacarUnidades(Player jugador)
    {
        Console.WriteLine("Atacando unidades..."); // En desarrollo...
    } // Permite al jugador atacar unidades enemigas, aunque en esta versión no se implementa la lógica de ataque
    
}