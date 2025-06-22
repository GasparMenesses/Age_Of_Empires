using System;

public class Fachada
{
    private Engine engine;

    public Fachada() // Constructor de la fachada que inicializa el motor del juego
    {
        engine = new Engine(); // Inicializa el motor del juego
        
        Console.WriteLine("Bienvenido a AGE OF EMPIRES");
        Console.WriteLine("Vamos a configurar la partida");
        
        engine.CrearJugadores(); // Llama al método para crear jugadores
        
        Console.WriteLine("\n\nLos jugadores han sido creados correctamente");
        Thread.Sleep(2000);
        
        Console.WriteLine("Creando entorno de juego...");
        Thread.Sleep(2000);
        
        Console.WriteLine("Cargando mapa...");
        Thread.Sleep(2000);

        engine.CreateNewGameMap(); // Llama al método para crear un nuevo mapa de juego
        
        engine.EmpezarLoop(); // Inicia el bucle del juego, donde se desarrollará la partida
        
    }


}