using System;
using Library;

// esta clase representa la fachada del juego, que es la interfaz principal para interactuar con el motor del juego
// La fachada simplifica la interacción con el motor, encapsulando la lógica de creación de jugadores y el entorno del juego.
// La clase también maneja la inicialización del motor y el inicio del bucle del juego.

namespace Facade
{
    public class Fachada
    {
        public Engine engine;

        public Fachada() // Constructor de la fachada que inicializa el motor del juego
        {
            
            engine = new Engine(); // Inicializa el motor del juego
            // engine.CrearJugadores1();
            // engine.CreateNewGameMap(); // Crea un nuevo mapa de juego

        }

        public string Comenzar( string username, string civilization ) // Método para iniciar el juego
        {
                    
            string text = engine.CrearJugadores( username, civilization); // Crea los jugadores y devuelve un mensaje de bienvenida);

            return text;
            // Console.WriteLine("Creando entorno de juego...");
            // Thread.Sleep(2000);
            //
            // Console.WriteLine("Cargando mapa...");
            // Thread.Sleep(2000);
            //
            // engine.CreateNewGameMap(); // Llama al método para crear un nuevo mapa de juego
            //
            // engine.EmpezarLoop(); // Inicia el bucle del juego, donde se desarrollará la partida

        }
    
    }
}
