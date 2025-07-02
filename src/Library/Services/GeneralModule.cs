using Discord.Commands;
using Library.Core;

public class GeneralModule : ModuleBase<SocketCommandContext>
{
    List<Player> jugadores = new List<Player>();
    [Command("Map")]
    public async Task HolaAsync()
    {
        await ReplyAsync("Que hace loco");
        new Map();
        await ReplyAsync(MapPrinter.PrintMap());
    }

    [Command("Join")]
    public async Task JoinAsync()
    {
        await ReplyAsync($"Muy buenas {Context.User.Username}, bienvenido al juego de Age of Empires" +
                         "Para comenzar, por favor, elige una civilización de las siguientes opciones:\n" +
                         "1. Cordobeses\n" + "2. Romanos\n" + "3. Vikingos\n" + "(Escriba el número de la civilización deseada)");
        var response = await NextMessageAsync();
        
    }
    
    [Command("sape")]
    public async Task HolaxdAsync()
    {
        await ReplyAsync("Mama");
    }
}