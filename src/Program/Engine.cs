using Library.Actions;
using Library.Buildings;
using Library.Core;
using Library.Farming;
using Library.Units;


public class Engine
{
    
    //  INICIALIZO VARIABLES
    public DateTime HoraInicio { get; private set; }
    public int CantidadJugadores { get; private set; }
    public List<Player> Jugadores { get; private set; } = new List<Player>();
    
    public List<GoldMine> MinasDeOro { get; private set; } = new List<GoldMine>(); 
    
    public List<Woods> Bosques { get; private set; } = new List<Woods>();
    
    public List<Quarry> MinasDePiedra { get; private set; } = new List<Quarry>();
    
    public List<Farm> Granjas { get; private set; } = new List<Farm>();

    
    
    // FUNCIONES
    
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
            
        }
        
        foreach (var jugador in Jugadores)
        {
            for (int i = 0; i < 3; i++)
            {
                jugador.Units.Add(new Villager(jugador.Buildings[0])); // cada jugador empieza con 3 aldeanos
            }
        }
    }

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
    }

    public void CreateNewGameMap()
    {
        new Map(); 
        foreach (var jugador in Jugadores)
        {

            // Cada jugador comienza con un centro cívico
            Map.PlaceRandom(jugador.Buildings[0].Symbol);
            
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
    }
    
    public void EmpezarLoop()
    {
        HoraInicio = DateTime.Now;
        Console.WriteLine($"\n¡El juego ha comenzado!     {HoraInicio}");
        
        foreach (var jugador in Jugadores)
        {
            Console.WriteLine($"\nTurno de {jugador.Nombre} ({jugador.Civilization.NombreCivilizacion})");
            Console.WriteLine($"Recursos disponibles:\n Oro: {jugador.Resources.Gold}\n Madera: {jugador.Resources.Wood}\n Comida: {jugador.Resources.Food}\n Piedra: {jugador.Resources.Stone}");
            Thread.Sleep(1500);
            int numberOfVillagers = jugador.Units.OfType<Villager>().Count();
            Console.WriteLine($"\nUnidades disponibles:\n Aldeanos: {numberOfVillagers}");
            Thread.Sleep(1500);
            
            string accion = "0";
            while (accion != "1" && accion != "2" && accion != "3" && accion != "4")
            {
                Console.WriteLine("\nAcciones disponibles:\n 1- Mover unidades\n 2- Recolectar recursos\n 3- Construir edificios\n 4- Atacar unidades");
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

    public void MoverUnidadees(Player jugador)
    {
        Console.WriteLine("Ingrese la posición (x, y) a la que desea mover sus unidades:");
        string[] posicion = Console.ReadLine().Split(',');
        if (posicion.Length == 2 && int.TryParse(posicion[0], out int x) && int.TryParse(posicion[1], out int y))
        {
                        
        }
        else
        {
            Console.WriteLine("Posición inválida.");
        }
    }

    public void RecolectarRecursos(Player jugador)
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
        int tiempoporaldeano=10000; // Tiempo que tarda un aldeano en recolectar un recurso (10 segundos)
        int cantidadRecursos = 0; // Cantidad de recursos recolectados por los aldeanos
        switch (recurso)
        {
            case "1":
                cantidadAldeanos = SeleccionarCantidadAldeanos(jugador, "Madera"); 
                Console.WriteLine("Recolectando madera...");
                var woods = Bosques.FirstOrDefault(); // Obtiene el primer bosque disponible para recolectar madera
                Thread.Sleep(tiempoporaldeano * cantidadAldeanos); // Simula el tiempo de recolección
                if (woods != null)
                {
                    cantidadRecursos = cantidadAldeanos * Woods.TasaDeRecoleccion;
                    jugador.Resources.Wood += cantidadRecursos;
                    Woods.TasaDeRecoleccion -= Woods.CantidadRecursoDisponible; // Actualiza la cantidad de madera disponible en el bosque
                    Console.WriteLine($"Recolectaste {cantidadRecursos} de madera.");
                }
                break;
            case "2":
                cantidadAldeanos = SeleccionarCantidadAldeanos(jugador, "Piedra"); 
                Console.WriteLine("Recolectando piedra...");
                var quarry = MinasDePiedra.FirstOrDefault(); // Obtiene la primera mina de piedra disponible
                Thread.Sleep(tiempoporaldeano * cantidadAldeanos); // Simula el tiempo de recolección
                if (quarry != null)
                {
                    cantidadRecursos = cantidadAldeanos * Quarry.TasaDeRecoleccion;
                    jugador.Resources.Stone += cantidadRecursos;
                    Quarry.TasaDeRecoleccion -= Quarry.CantidadRecursoDisponible; // Actualiza la cantidad de piedra disponible en la mina
                    Console.WriteLine($"Recolectaste {cantidadRecursos} de piedra.");
                }
                
                break;
            case "3":
                cantidadAldeanos = SeleccionarCantidadAldeanos(jugador, "Oro"); 
                Console.WriteLine("Recolectando oro...");
                var goldMine = MinasDeOro.FirstOrDefault(); // Obtiene la primera mina de oro disponible
                Thread.Sleep(tiempoporaldeano * cantidadAldeanos); // Simula el tiempo de recolección
                if (goldMine != null)
                {
                    cantidadRecursos = cantidadAldeanos * GoldMine.TasaDeRecoleccion;
                    jugador.Resources.Gold += cantidadRecursos;
                    GoldMine.TasaDeRecoleccion -= GoldMine.CantidadRecursoDisponible; // Actualiza la cantidad de oro disponible en la mina
                    Console.WriteLine($"Recolectaste {cantidadRecursos} de oro.");
                }
               
                break;
            case "4":
                cantidadAldeanos = SeleccionarCantidadAldeanos(jugador, "Comida");
                Console.WriteLine("Recolectando comida...");
                var farm = Granjas.FirstOrDefault(); // Obtiene la primera granja disponible
                Thread.Sleep(tiempoporaldeano * cantidadAldeanos); // Simula el tiempo de recolección
                if (farm != null)
                {
                    cantidadRecursos = cantidadAldeanos * Farm.TasaDeRecoleccion;
                    jugador.Resources.Food += cantidadRecursos;
                    Farm.TasaDeRecoleccion -= Farm.CantidadRecursoDisponible; // Actualiza la cantidad de comida disponible en la granja
                    Console.WriteLine($"Recolectaste {cantidadRecursos} de comida.");
                }
                break;
            default:
                Console.WriteLine("Recurso inválido.");
                break;
        }
    }

    public void ConstruirEdificios(Player jugador)
    {
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

        switch (edificio)
        {
            case "1":
                Console.WriteLine("Construyendo Almacen de oro...");
                jugador.Resources.Stone -= 55; // Resta el costo de piedra
                jugador.Resources.Wood -= 25; // Resta el costo de madera
              break;
                
        }
        
    }
    
    public void AtacarUnidades(Player jugador)
    {
        Console.WriteLine("Atacando unidades...");
    }
    
    
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
        
        Console.WriteLine($"\n{cantidad} aldeano/s asignado/s a la recolección de {recurso}.");
        return cantidad;
    }

    
    
    
}
