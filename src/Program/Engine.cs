using Library.Core;
using Library.Farming;
using Library.Units;


public class Engine
{
    public DateTime HoraInicio { get; private set; }
    public int CantidadJugadores { get; private set; }
    public List<Player> Jugadores { get; private set; } = new List<Player>();
    public List<GoldMine> MinasDeOro { get; private set; } = new List<GoldMine>();
    public List<Woods> Woods { get; private set; } = new List<Woods>();

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
            for (int i = 0; i >= 3; i++)
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
            Map.PlaceRandom(CantidadJugadores, jugador.Buildings[0]);
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
        while (recurso != "1" && recurso != "2" && recurso != "3" && recurso != "4")
        {
            Console.WriteLine("\nIngrese el recurso a recolectar:\n 1 - madera\n 2 - piedra\n 3 - oro\n 4 - comida):");
            recurso = Console.ReadLine();
            if (recurso != "1" && recurso != "2" && recurso != "3" && recurso != "4")
            {
                Console.WriteLine("\nRecurso inválido. Por favor, ingrese un número del 1 al 4.");
            }
        }
        
        
        switch (recurso)
        {
            case "1":
                Console.WriteLine("Recolectando madera...");
                break;
            case "2":
                Console.WriteLine("Recolectando piedra...");
                break;
            case "3":
                Console.WriteLine("Recolectando oro...");
                break;
            case "4":
                Console.WriteLine("Recolectando comida...");
                break;
            default:
                Console.WriteLine("Recurso inválido.");
                break;
        }
    }

    public void ConstruirEdificios(Player jugador)
    {
        Console.WriteLine("Construyendo edificios...");
    }
    
    public void AtacarUnidades(Player jugador)
    {
        Console.WriteLine("Atacando unidades...");
    }
    
}
