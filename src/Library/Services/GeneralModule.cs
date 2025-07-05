using Discord.Commands;
using Library.Core;

public class GeneralModule : ModuleBase<SocketCommandContext>
{
    static List<Player> jugadores = new List<Player>();
    static Dictionary<string,TaskCompletionSource<string>> selections = new Dictionary<string,TaskCompletionSource<string>>();
    
    [Command("Comenzar")]
    public async Task StartNewGameAsync()
    {
        new Map();
        string username = Context.User.Username;
        await ReplyAsync($"🎮 Bienvenido a **AGE OF EMPIRES**, {username}! Vamos a configurar la partida ⚔️");
    }
    
    [Command("PrintMapa")]
    public async Task HolaAsync()
    {
        await ReplyAsync(("http://localhost:63342/Age_Of_Empires/src/Library/html/index.html?_ijt=gj5tbh1o5snvg6rnpkjdm1u0bo&_ij_reload=RELOAD_ON_SAVE"));
    }

    [Command("Add")]
    
    public async Task Add(int n1, int n2)
    {
        int result = n1 + n2;
        await ReplyAsync($"El resultado es: {result}");
    }
    
///
 
    [Command("Join")]
    public async Task JoinAsync()
    {
        string civilization = "";
        foreach (Player player in jugadores)
        {
            if (player.Nombre == Context.User.Id.ToString())
            {
                await ReplyAsync($"El jugador {Context.User.Username} ya se encuentra en la partida.");
                return;
            }
        }
        await ReplyAsync($"Bienvenido {Context.User.Username} a Age of Empires, por favor, selecciona una civilización:\n 1.Cordobeses\n 2.Romanos\n 3.Vikingos\n (Por favor, ingrese su numero)");
        selections[Context.User.Id.ToString()] = new TaskCompletionSource<string>();
        await ReplyAsync("Esperando que completes la selección (estado inicial: " +
                         selections[Context.User.Id.ToString()].Task.IsCompleted + ")");
        await ReplyAsync("DEBUG - Tu ID es: " + Context.User.Id.ToString());
        string selection = await selections[Context.User.Id.ToString()].Task;
        await ReplyAsync("¡Selección recibida!");
        switch (selection)
        {
            case "1":
                civilization = "Cordobeses";
                break;
            case "2":
                civilization = "Romanos";
                break;
            case "3": 
                civilization = "Vikingos";
                break;
        }
        jugadores.Add(new Player(Context.User.Id.ToString(), civilization));
        await ReplyAsync($"El jugador {Context.User.Username} se ha unido a la partida con la civilizacion {civilization}.");
        selections.Remove(Context.User.Id.ToString());
    }
    
///    
    
    [Command("sape")]
    public async Task HolaxdAsync()
    {
        await ReplyAsync("Mama");
    }

    [Command("1")]
    public async Task SelectionIs1()
    {
        await ReplyAsync("DEBUG - Tu ID es: " + Context.User.Id.ToString());
        string key = Context.User.Id.ToString();

        if (!selections.ContainsKey(key))
        {
            await ReplyAsync("Por favor, únete a la partida primero usando el comando !Join.");
            return;
        }

        var tcs = selections[key];
        if (tcs.Task.IsCompleted)
        {
            await ReplyAsync("Ya completaste tu selección antes.");
            return;
        }

        await ReplyAsync("Seleccionando opción 1...");
        tcs.SetResult("1");
    }


    [Command("2")]
    public async Task SelectionIs2()
    {
        if (!selections.ContainsKey(Context.User.Id.ToString()))
        {
            await ReplyAsync("Por favor, únete a la partida primero usando el comando !Join.");
            return;
        }
        selections[Context.User.Id.ToString()].SetResult("2");
    }
    [Command("3")]
    public async Task SelectionIs3()
    {
        if (!selections.ContainsKey(Context.User.Id.ToString()))
        {
            await ReplyAsync("Por favor, únete a la partida primero usando el comando !Join.");
            return;
        }
        selections[Context.User.Id.ToString()].SetResult("3");
    }
}