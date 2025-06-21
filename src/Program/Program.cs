using Library.Buildings;
using System.Timers;
using Library;
using Library.Core;

class Program
{
    
    static void Main(string[] args)
    {
        
        Console.WriteLine("Bienvenido a AGE OF EMPIRES");
        Console.WriteLine("Vamos a configurar la partida");
        
        
        Fachada.CreatePLayer(); // Pide un número de jugadores entre 2 y 4 y crea
        // una lista de jugadores con sus respectivos nombres y civilizaciones.
        
        
        Console.WriteLine("\n\nLos jugadores han sido creados correctamente");
        Thread.Sleep(2000);
        Console.WriteLine("creando entorno de juego...");
        Thread.Sleep(2000);
        Console.WriteLine("cargando mapa...");
        Thread.Sleep(2000);
        Console.WriteLine("ya casi estamos ...");
        Thread.Sleep(2000);
        
        // Crea el mapa del juego, coloca los recursos y edificios iniciales
        Fachada.CreateNewGameMap(); 
        
    }
}
