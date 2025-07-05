using System;
using System.Threading;
using Ucu.Poo.DiscordBot.Services;

// Esta clase representa la fachada del juego, que es la interfaz principal para interactuar con el motor del juego..
class Program
{
    static void Main(string[] args)
    {
        DemoBot();
        // Primero crea el mapa del juego
        new Fachada();

    }
    
    private static void DemoBot()
    {
        BotLoader.LoadAsync().GetAwaiter().GetResult();
    }
}