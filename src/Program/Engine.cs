using System;
using System.Collections.Generic;
using System.Threading;
using Library.Buildings;
using Library.Core;

public class Engine
{
    public int CantidadJugadores { get; private set; }
    public List<Player> Jugadores { get; private set; } = new List<Player>();

    public void CrearJugadores()
    {
        Console.WriteLine("Indique cuántos jugadores van a participar (2-4): ");
        int cantidad;
        while (!int.TryParse(Console.ReadLine(), out  cantidad) || cantidad < 2 || cantidad > 4)
        {
            Console.WriteLine("Número inválido. Ingrese entre 2 y 4 jugadores.");
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
                    Console.WriteLine("Nombre ya usado, elija otro.");
                }
            } while (!nombreValido);

            string civilizacion = SeleccionarCivilizacion();

            Jugadores.Add(new Player(nombre, civilizacion));
            Console.WriteLine($"\nBienvenido {nombre}, ¡elegiste la civilización {civilizacion}!");
        }
    }

    private string SeleccionarCivilizacion()
    {
        while (true)
        {
            Console.WriteLine("Seleccione su civilización:\n1 - Cordobeses\n2 - Romanos\n3 - Vikingos");
            string op = Console.ReadLine();
            switch (op)
            {
                case "1": return "Cordobeses";
                case "2": return "Romanos";
                case "3": return "Vikingos";
                default: Console.WriteLine("Opción inválida."); break;
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
        Console.WriteLine("\n¡El juego ha comenzado!");

        foreach (var jugador in Jugadores)
        {
            Console.WriteLine($"\nTurno de {jugador.Nombre} ({jugador.Civilization.NombreCivilizacion})");
            
            
            Console.WriteLine($"Recursos disponibles:\n Oro: {jugador.Resources.Gold}\n Madera: {jugador.Resources.Wood}\n Comida: {jugador.Resources.Food}\n Piedra: {jugador.Resources.Stone}");
            
            
            Thread.Sleep(1500);
            

        }

        Console.WriteLine("\nFin de ronda. (A futuro, este sería el loop principal del juego)");
    }
}
