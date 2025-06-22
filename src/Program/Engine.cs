using System;
using System.Collections.Generic;
using System.Threading;
using Library;
using Library.Buildings;
using Library.Core;

public class Engine
{
    public DateTime HoraInicio { get; private set; }
    public int CantidadJugadores { get; private set; }
    public List<Player> Jugadores { get; private set; } = new List<Player>();

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

        Map.PlaceBuildings(CantidadJugadores, CivicCenter.Symbol);
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
                    Console.WriteLine("Ingrese la posición (x, y) a la que desea mover sus unidades:");
                    string[] posicion = Console.ReadLine().Split(',');
                    if (posicion.Length == 2 && int.TryParse(posicion[0], out int x) && int.TryParse(posicion[1], out int y))
                    {
                        
                    }
                    else
                    {
                        Console.WriteLine("Posición inválida.");
                    }
                    break;
                case "2":
                    Console.WriteLine("Ingrese el recurso a recolectar (madera, piedra, oro, comida):");
                    string recurso = Console.ReadLine().ToLower();
                    break;
                case "3":
                    Console.WriteLine("Construyendo edificios...");
                    break;
                case "4":
                    Console.WriteLine("Atacando unidades...");
                    break;
            }

        }

        Console.WriteLine("\nFin de ronda. (A futuro, este sería el loop principal del juego)");
    }
}
