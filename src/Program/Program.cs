namespace Program;
using Library;
using Library.Core;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Ingrese su nombre: ");
        string Nombre = Console.ReadLine();
        new Player(Nombre);
    }
}
