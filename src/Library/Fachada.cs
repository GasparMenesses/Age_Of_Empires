using Library.Buildings;

namespace Library;
using Library.Core;
public static class Fachada
{
    // La fachada es la clase que se encarga de la creacion de una nueva partida,
    // llama todas las funciones necesarias para la creacion de un entorno
    // verificando por ejemplo, al momento de crear algo, que no haya ya algo en ese punto del mapa
    
    // QUE HACE?
    // Agrupa subsistemas complejos.
    // Oculta detalles internos como qué clase hace qué.
    // Permite a quien use el sistema (controlador, UI, inteligencia artificial) trabajar con un punto de acceso simple.
    
    // DE QUE SE ENCARGA?
    // Coordina llamadas entre distintos sistemas del juego.
    // Reduce el acoplamiento entre el código del "jugador" y las clases internas.
    // Hace que el código sea más limpio, fácil de mantener y de testear.
    
    private static Map mapa;
    
    public static List<Player> Jugadores { get; private set; } = new List<Player>(); // Lista para almacenar los jugadores

    public static int CantidadJugadores;
    
    public static void CreatePLayer()
    {
        Console.WriteLine("Indique cuantos jugadores van a participar (2-4): ");
        
         // TryParse para validar la entrada del usuario 
        while (!int.TryParse(Console.ReadLine(), out CantidadJugadores) || CantidadJugadores < 2 || CantidadJugadores > 4)
        {
            Console.WriteLine("Por favor, ingrese un número válido entre 2 y 4:");
        }
        
        
        for (int i = 0; i < CantidadJugadores; i++)
        {
            Console.WriteLine($"\nEs turno del jugador N°{i + 1}");
            Console.WriteLine("Ingrese su nombre: ");
            string nombre;
            bool nombreValido;
            do
            {
                nombre = Console.ReadLine();
                nombreValido = true;
                foreach (var jugador in Jugadores)
                {
                    if (jugador.Nombre == nombre)
                    {
                        Console.WriteLine("Por favor, ingrese un nombre distinto al de los jugadores anteriores.");
                        nombreValido = false;
                        break;
                    }
                }
            } while (!nombreValido);

            Console.WriteLine("\nBienvenido " + nombre);

            string selector = "0";
            while (selector != "1" && selector != "2" && selector != "3")
            {
                Console.WriteLine("Por favor escoge tu civilización:\n1 - Cordobeses \n2 - Romanos \n3 - Vikingos");
                selector = Console.ReadLine();

                if (selector != "1" && selector != "2" && selector != "3")
                {
                    Console.WriteLine("\nERROR, ingrese una opción válida\n ");
                }
            }

            string civilizacion = "";
            switch (selector)
            {
                case "1":
                    Console.WriteLine("Elegiste los Cordobeses");
                    civilizacion = "Cordobeses";
                    break;
                case "2":
                    Console.WriteLine("Elegiste los Romanos");
                    civilizacion = "Romanos";
                    break;
                case "3":
                    Console.WriteLine("Elegiste los Vikingos");
                    civilizacion = "Vikingos";
                    break;
            }

            Jugadores.Add(new Player(nombre, civilizacion));
        }
    }
    

    public static void CreateNewGameMap()
    {

        new Map();
        
        // Colocamos los edificios en el mapa
        Map.PlaceBuildings( 2, CivicCenter.Symbol); // Civic Center
        MapPrinter.PrintMap();


        
    }
}