using Library.Actions;
using Library.Buildings;
using Library.Core;
using Library.Farming;
using Library.Units;


public class Engine
{
    
    //  INICIALIZO VARIABLES
    public DateTime HoraInicio { get; private set; }  // Hora de inicio del juego
    public int CantidadJugadores { get; private set; } // Cantidad de jugadores en la partida
    public List<Player> Jugadores { get; private set; } = new List<Player>(); // Lista de jugadores en la partida
    
    public List<GoldMine> MinasDeOro { get; private set; } = new List<GoldMine>();  // Lista de minas de oro en el mapa
    
    public List<Woods> Bosques { get; private set; } = new List<Woods>(); // Lista de bosques en el mapa
    
    public List<Quarry> MinasDePiedra { get; private set; } = new List<Quarry>(); // Lista de minas de piedra en el mapa
    
    public List<Farm> Granjas { get; private set; } = new List<Farm>(); // Lista de granjas en el mapa

    
    
    // FUNCIONES
    
    /// <summary>
    /// Crea los jugadores de la partida, solicitando sus nombres y civilizaciones.
    /// Asigna recursos iniciales y unidades iniciales a cada jugador.
    /// </summary>
    public void CrearJugadores()
    {
        Console.WriteLine("Indique cuántos jugadores van a participar (2-4): ");
        int cantidad;
        while (!int.TryParse(Console.ReadLine(), out  cantidad) || cantidad < 2 || cantidad > 4)
        {
            Console.WriteLine("\nNúmero inválido. Ingrese entre 2 y 4 jugadores.");
        }

        CantidadJugadores = cantidad;

        for (int i = 0; i < CantidadJugadores; i++)
        {
            Console.WriteLine($"\nEs turno del jugador N°{i + 1}");
            string nombre;
            bool nombreValido;
            do
            {
                Console.WriteLine("Ingrese su nombre:");
                nombre = Console.ReadLine();
                nombreValido = !Jugadores.Exists(j => j.Nombre == nombre);

                if (!nombreValido)
                {
                    Console.WriteLine("\nNombre ya usado, elija otro.\n");
                }
            } while (!nombreValido);

            string civilizacion = SeleccionarCivilizacion();

            Jugadores.Add(new Player(nombre, civilizacion));
            Console.WriteLine($"\nBienvenido {nombre}, ¡elegiste la civilización {civilizacion}!");
            Console.WriteLine($"\n{nombre}, {Jugadores.Last().Civilization.DescripcionBonificacion}");
            
        }
        
        foreach (var jugador in Jugadores)
        {
            // Cada civilización tiene una bonificación inicial de recursos
            jugador.Resources.AddResources( 
                wood: jugador.Civilization.Bonificacion.Item1,
                stone: jugador.Civilization.Bonificacion.Item2,
                gold: jugador.Civilization.Bonificacion.Item3,
                food: jugador.Civilization.Bonificacion.Item4
            );
            
            // Cada jugador empieza con 3 aldeanos
            for (int i = 0; i < 3; i++)
            {
                jugador.Units.Add(new Villager(jugador.Buildings[0])); 
            }
        }
    } // Se encarga de la creación de los jugadores
    
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
    public void CreateNewGameMap()
    {
        new Map(); 
        foreach (var jugador in Jugadores)
        {

            // Cada jugador comienza con un centro cívico
            Map.PlaceRandom(CivicCenter.Symbol, jugador.Buildings[0]);
            
            // Por cada jugador agrego 3 minas de oro al mapa
            for (int i = 0; i < 3; i++)
            {
                var minaoro = new GoldMine((0, 0), 500); 
                Map.PlaceRandom(GoldMine.Symbol);
                MinasDeOro.Add(minaoro);
            }
            
            // Por cada jugador agrego 5 woods al mapa
            for (int i = 0; i < 5; i++)
            {
                var wood = new Woods((0, 0), 500); 
                Map.PlaceRandom(Woods.Symbol);
                Bosques.Add(wood);
            }
            
            // Por cada jugador agrego 5 minas de piedra al mapa
            for (int i = 0; i < 5; i++)
            {
                var minapiedra = new Quarry((0, 0), 500); 
                Map.PlaceRandom(Quarry.Symbol);
                MinasDePiedra.Add(minapiedra);
            }
            
            // Por cada jugador agrego 5 granjas al mapa
            for (int i = 0; i < 5; i++)
            {
                var granja = new Farm((0, 0), 500);
                Map.PlaceRandom(Farm.Symbol);
                Granjas.Add(granja);
            }
            
        }
        MapPrinter.PrintMap();
        
    } // Crea un nuevo mapa para el juego, colocando los edificios y recursos iniciales
    
    
    /// <summary>
    /// Inicia el bucle principal del juego, gestionando los turnos y acciones de los jugadores.
    /// </summary>
    public void EmpezarLoop() // Inicia el bucle principal del juego
    {
        HoraInicio = DateTime.Now;
        Console.WriteLine($"\n¡El juego ha comenzado!     {HoraInicio}");  // Muestra la hora de inicio del juego
        
        foreach (var jugador in Jugadores)
        {
            Console.WriteLine($"\nTurno de {jugador.Nombre} ({jugador.Civilization.NombreCivilizacion})");
            Console.WriteLine($"Recursos disponibles:\n Oro: {jugador.Resources.Gold}\n Madera: {jugador.Resources.Wood}\n Comida: {jugador.Resources.Food}\n Piedra: {jugador.Resources.Stone}");
            Thread.Sleep(1500);
            int numberOfVillagers = jugador.Units.OfType<Villager>().Count(); // Cuenta la cantidad de aldeanos del jugador
            Console.WriteLine($"\nUnidades disponibles:\n Aldeanos: {numberOfVillagers}");
            Thread.Sleep(1500);
            
            string accion = "0";
            while (accion != "1" && accion != "2" && accion != "3" && accion != "4")
            {
                Console.WriteLine("\nAcciones disponibles:\n 1- Mover unidades \n 2- Recolectar recursos\n 3- Construir edificios\n 4- Atacar unidades");
                accion = Console.ReadLine();       
                if (accion != "1" && accion != "2" && accion != "3" && accion != "4")
                {
                    Console.WriteLine("\nAcción inválida. Por favor, ingrese una acción válida (1-4):");
                }
            }
            
            switch (accion)
            {
                case "1":
                    MoverUnidadees(jugador);
                    break;
                case "2":
                    RecolectarRecursos(jugador);
                    break;
                case "3":
                    ConstruirEdificios(jugador);
                    break;
                case "4":
                    AtacarUnidades(jugador);
                    break;
            }

        }

        Console.WriteLine("\nFin de ronda. (A futuro este sería el loop principal del juego)");
    }

    
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
    /// Permite al jugador asignar aldeanos a la recolección de un recurso específico.
    /// </summary>
    public async Task RecolectarRecursos(Player jugador) // Permite al jugador asignar aldeanos a la recolección de recursos
    {
        string recurso = "0";
        int cantidadAldeanos = 0;
        
        while (recurso != "1" && recurso != "2" && recurso != "3" && recurso != "4")
        {
            Console.WriteLine("\nIngrese el recurso a recolectar:\n 1 - madera\n 2 - piedra\n 3 - oro\n 4 - comida:");
            recurso = Console.ReadLine();
            if (recurso != "1" && recurso != "2" && recurso != "3" && recurso != "4")
            {
                Console.WriteLine("\nRecurso inválido. Por favor, ingrese un número del 1 al 4.");
            }
            
        }
            
        string recursos = recurso switch

        {
            "1" => "madera",
            "2" => "piedra",
            "3" => "oro",
            "4" => "comida",
            _ => throw new InvalidOperationException("Recurso no válido")
        };
        if (string.IsNullOrEmpty(recurso))
            return;
        int cantidad = SeleccionarCantidadAldeanos(jugador, recursos); // Selecciona la cantidad de aldeanos a asignar a la recolección del recurso
        var aldeanos = jugador.Units.OfType<Villager>().Take(cantidad).ToList();  // Toma los aldeanos disponibles del jugador
        var actions = new Actions(jugador); // Crea una instancia de la clase Actions para realizar acciones con los aldeanos
        foreach (var aldeano in aldeanos)
        {
             await actions.Farmear(jugador, aldeano, recursos); // Asigna cada aldeano a la recolección del recurso seleccionado
        }
        Console.WriteLine($"\n{cantidad} aldeano/s asignado/s a la recolección de {recursos}.");
        Console.WriteLine($"\nRecursos actuales de {jugador.Nombre}: Oro: {jugador.Resources.Gold}, Madera: {jugador.Resources.Wood}, Comida: {jugador.Resources.Food}, Piedra: {jugador.Resources.Stone}");


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
    
    
    /// <summary>
    /// Solicita al jugador cuántos aldeanos quiere asignar a una tarea específica.
    /// </summary>
    private int SeleccionarCantidadAldeanos(Player jugador, string recurso)
    {
        Console.WriteLine($"\nIndique cuántos aldeanos quiere destinar a la recolección de {recurso}.");
        Console.WriteLine($"Dispone de {jugador.Units.OfType<Villager>().Count()} aldeanos:");

        string input = "0";
        int cantidad;

        while (!int.TryParse(input, out cantidad) || cantidad < 1 || cantidad > jugador.Units.OfType<Villager>().Count())
        {
            input = Console.ReadLine();
            if (!int.TryParse(input, out cantidad) || cantidad < 1 || cantidad > jugador.Units.OfType<Villager>().Count())
            {
                Console.WriteLine($"\nCantidad inválida. Debe ingresar un número entre 1 y {jugador.Units.OfType<Villager>().Count()}.");
            }
        }
        
        return cantidad;
    } // Permite al jugador seleccionar la cantidad de aldeanos a asignar a la recolección de un recurso específico

    
    
    
}
