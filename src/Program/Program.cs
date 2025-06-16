namespace Program;
using Library;
using Library.Core;

class Program
{
    
    static void Main(string[] args)
    {
        Console.WriteLine("Bienvenido a AGE OF EMPIRES");
        Console.WriteLine("Vamos a configurar la partida");
        Console.WriteLine("Ingrese su nombre: ");
        
        string Nombre1 = Console.ReadLine();
        Console.WriteLine( "Bienvenido " + Nombre1);
        
        string Selector = "0";
        while (Selector != "1" && Selector != "2" && Selector !="3")
        {
            Console.WriteLine("Por favor escoge tu civilización:\n1 - Cordobeses \n2 - Romanos \n3 - Vikingos");
            Selector = Console.ReadLine();

            if (Selector != "1" && Selector != "2" && Selector != "3")
            {
                Console.WriteLine("ERROR, ingrese una opción válida\n ");
            }
        }

        string civilization = "";
        switch (Selector)
        {
            case "1":
                Console.WriteLine("Elegiste los Cordobeses");
                civilization = "Cordobeses";
                break;
            case "2":
                Console.WriteLine("Elegiste los Romanos");
                civilization = "Romanos";
                break;
            case "3":
                Console.WriteLine("Elegiste los Vikingos");
                civilization = "Vikingos";
                break;
        }
        
        Player Jugador_1 = new Player(Nombre1, civilization);
    }
}
