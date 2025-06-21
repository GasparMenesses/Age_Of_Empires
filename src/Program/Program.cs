using System;
using System.Threading;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Bienvenido a AGE OF EMPIRES");
        Console.WriteLine("Vamos a configurar la partida");

        Fachada fachada = new Fachada();

        fachada.CrearJugadores();

        Console.WriteLine("\n\nLos jugadores han sido creados correctamente");
        Thread.Sleep(2000);
        Console.WriteLine("Creando entorno de juego...");
        Thread.Sleep(2000);
        Console.WriteLine("Cargando mapa...");
        Thread.Sleep(2000);
        Console.WriteLine("Ya casi estamos...");
        Thread.Sleep(2000);

        fachada.IniciarJuego();
    }
}