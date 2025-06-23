using System;
using Library;
using Library.Buildings;
using Library.Core;
using Library.Farming;

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
            jugador.Villager = new Villager(3); // cada jugador empieza con 3 aldeanos
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
            var centroCivico = new CivicCenter(jugador);
            var CivicCenterCoords = Map.PlaceBuildings(CivicCenter.Symbol);
            centroCivico.Ubicacion(CivicCenterCoords);
            jugador.Buildings.Add(centroCivico);
            
            
            // Por cada jugador agrego 3 minas de oro al mapa
            for (int i = 0; i < 3; i++)
            {
                var coords = Map.PlaceBuildings(GoldMine.Symbol);
                int x = int.Parse(coords[0, 0]);
                int y = int.Parse(coords[0, 1]);

                var minaoro = new GoldMine((x, y), 500); 
                MinasDeOro.Add(minaoro);
            }
            
            // Por cada jugador agrego 5 woods al mapa
            for (int i = 0; i < 5; i++)
            {
                var coords = Map.PlaceBuildings(Woods.Symbol);
                int x = int.Parse(coords[0, 0]);
                int y = int.Parse(coords[0, 1]);

                var wood = new Woods((x, y), 250); 
                Bosques.Add(wood);
            }
            
            // Por cada jugador agrego 5 minas de piedra al mapa
            for (int i = 0; i < 5; i++)
            {
                var coords = Map.PlaceBuildings(Quarry.Symbol);
                int x = int.Parse(coords[0, 0]);
                int y = int.Parse(coords[0, 1]);

                var minapiedra = new Quarry((x, y), 200); 
                MinasDePiedra.Add(minapiedra);
            }
            
            // Por cada jugador agrego 5 granjas al mapa
            for (int i = 0; i < 5; i++)
            {
                var coords = Map.PlaceBuildings(Farm.Symbol);
                int x = int.Parse(coords[0, 0]);
                int y = int.Parse(coords[0, 1]);

                var granja = new Farm((x, y), 200); 
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
            
            Console.WriteLine($"\nUnidades disponibles:\n Aldeanos: {jugador.Villager.Villagers}");
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
        
        
        switch (recurso)
        {
            case "1":
                cantidadAldeanos = SeleccionarCantidadAldeanos(jugador, "Madera"); 
                Console.WriteLine("Recolectando madera...");
                break;
            case "2":
                cantidadAldeanos = SeleccionarCantidadAldeanos(jugador, "Piedra"); 
                Console.WriteLine("Recolectando piedra...");
                break;
            case "3":
                cantidadAldeanos = SeleccionarCantidadAldeanos(jugador, "Oro"); 
                Console.WriteLine("Recolectando oro...");
                break;
            case "4":
                Console.WriteLine("Recolectando comida...");
                break;
            default:
                cantidadAldeanos = SeleccionarCantidadAldeanos(jugador, "Comida"); 
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
    
    
    private int SeleccionarCantidadAldeanos(Player jugador, string recurso)
    {
        Console.WriteLine($"\nIndique cuántos aldeanos quiere destinar a la recolección de {recurso}.");
        Console.WriteLine($"Dispone de {jugador.Villager.Villagers} aldeanos:");

        string input = "0";
        int cantidad;

        while (!int.TryParse(input, out cantidad) || cantidad < 1 || cantidad > jugador.Villager.Villagers)
        {
            input = Console.ReadLine();
            if (!int.TryParse(input, out cantidad) || cantidad < 1 || cantidad > jugador.Villager.Villagers)
            {
                Console.WriteLine($"\nCantidad inválida. Debe ingresar un número entre 1 y {jugador.Villager.Villagers}.");
            }
        }

        jugador.Villager.Villagers -= cantidad; // Descuento los aldeanos asignados
        Console.WriteLine($"\n{cantidad} aldeanos asignados a la recolección de {recurso}.");
        return cantidad;
    }

    
    
    
}
