using System;
using System.Threading;
using Ucu.Poo.DiscordBot.Services;

// Esta clase representa la fachada del juego, que es la interfaz principal para interactuar con el motor del juego..
class Program
{
    static void Main(string[] args)
    {
        DemoBot();
        new Fachada(); // Inicializa la fachada del juego
    }
    
    private static void DemoBot()
    {
        BotLoader.LoadAsync().GetAwaiter().GetResult();
    }
}