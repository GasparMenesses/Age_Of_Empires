using Library.Buildings;

namespace Program;
using System.Timers;
using Library.Core;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Ingrese su nombre: ");
        string Nombre = Console.ReadLine();
        Player jugador= new  Player(Nombre);
        Mill mill = new Mill(jugador);
        Farm farm = new Farm(mill);
        farm.GeneraciondeComida += cantidad =>
        {
            Console.WriteLine($"[Farm] Se generaron {cantidad} unidades de comida.");
            Console.WriteLine($"[Mill] Total almacenado: {mill.AlmacenComida}");
        };
        Console.WriteLine("Presioná Enter para finalizar.");
        Console.ReadLine();
    
    }
}
