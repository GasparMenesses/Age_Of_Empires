using Library.Buildings;
using System.Timers;
using Library;
using Library.Core;

class Program
{
    
    static void Main(string[] args)
    {

        Map x = new Map();
        MapPrinter xy = new MapPrinter(x);
        
        
        
        
        Console.WriteLine("Bienvenido a AGE OF EMPIRES");
        Console.WriteLine("Vamos a configurar la partida");
        
        // PLAYER 1
        
        Console.WriteLine("Ingrese su nombre: ");
        
        string Nombre_1 = Console.ReadLine();
        Console.WriteLine( "Bienvenido " + Nombre_1);
        
        string Selector_1 = "0";
        while (Selector_1 != "1" && Selector_1 != "2" && Selector_1 !="3")
        {
            Console.WriteLine("Por favor escoge tu civilización:\n1 - Cordobeses \n2 - Romanos \n3 - Vikingos");
            Selector_1 = Console.ReadLine();

            if (Selector_1 != "1" && Selector_1 != "2" && Selector_1 != "3")
            {
                Console.WriteLine("ERROR, ingrese una opción válida\n ");
            }
        }

        string civilization_1 = "";
        switch (Selector_1)
        {
            case "1":
                Console.WriteLine("Elegiste los Cordobeses");
                civilization_1 = "Cordobeses";
                break;
            case "2":
                Console.WriteLine("Elegiste los Romanos");
                civilization_1 = "Romanos";
                break;
            case "3":
                Console.WriteLine("Elegiste los Vikingos");
                civilization_1 = "Vikingos";
                break;
        }
        
        Player Jugador_1 = new Player(Nombre_1, civilization_1);
        
        
        // PLAYER 2
        
        Console.WriteLine("\n\nEs tunro del jugador N°2");
        Console.WriteLine("Ingrese su nombre: ");
        
        string Nombre_2 = Nombre_1;
        
        while (Nombre_1 == Nombre_2)
        {
            Nombre_2 = Console.ReadLine();

            if ( Nombre_2 == Nombre_1)
            {   
                Console.WriteLine("Por favor, ingrese un nombre disntino al del Jugador N°1 ");
            }
        }
        
        Console.WriteLine( "Bienvenido " + Nombre_2);
        
        string Selector_2 = "0";
        while (Selector_2 != "1" && Selector_2 != "2" && Selector_2 !="3")
        {
            Console.WriteLine("Por favor escoge tu civilización:\n1 - Cordobeses \n2 - Romanos \n3 - Vikingos");
            Selector_2 = Console.ReadLine();

            if (Selector_2 != "1" && Selector_2 != "2" && Selector_2 != "3")
            {
                Console.WriteLine("ERROR, ingrese una opción válida\n ");
            }
        }

        string civilization_2 = "";
        switch (Selector_2)
        {
            case "1":
                Console.WriteLine("Elegiste los Cordobeses");
                civilization_2 = "Cordobeses";
                break;
            case "2":
                Console.WriteLine("Elegiste los Romanos");
                civilization_2 = "Romanos";
                break;
            case "3":
                Console.WriteLine("Elegiste los Vikingos");
                civilization_2 = "Vikingos";
                break;
        }
        
        Player Jugador_2 = new Player(Nombre_2, civilization_2);
    }
}

