using Discord.Commands;
using Library.Core;
using Facade;

public class GeneralModule : ModuleBase<SocketCommandContext>
{

    private Fachada fachada = new Fachada();
    //
    // [Command("Comenzar")]
    // public async Task StartNewGameAsync()
    // {
    //     var username = Context.User.Username;
    //     await ReplyAsync($"🎮 Bienvenido a **AGE OF EMPIRES**, {username}! Vamos a configurar la partida ⚔️");
    //     fachada.Comenzar();
    //
    // }
    private int phase = 1; // Fase del juego, 1: Generacion de la partida, 2: Construcción de edificios, 3: Recolección de recursos
    static List<Player> jugadores = new List<Player>();
    static Dictionary<string,TaskCompletionSource<string>> selections = new Dictionary<string,TaskCompletionSource<string>>();
    [Command("Comenzar")]
    public async Task StartNewGameAsync()
    {
        new Map();
        string username = Context.User.Username;
        await ReplyAsync($"🎮 Bienvenido a **AGE OF EMPIRES**, {username}! Vamos a configurar la partida ⚔️");
    }

    
    [Command("Mapa")]
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
    [Command("Unirse")]
    public async Task JoinAsync()
    {
        string userId = Context.User.Id.ToString();

        foreach (Player player in jugadores)
        {
            if (player.Nombre == userId)
            {
                await ReplyAsync($"El jugador {Context.User.Username} ya se encuentra en la partida.");
                return;
            }
        }

        await ReplyAsync(
            $"Bienvenido {Context.User.Username} a Age of Empires, por favor, selecciona una civilización:\n" +
            "1.Cordobeses\n2.Romanos\n3.Vikingos\n(Por favor, ingrese su número)");

        var tcs = new TaskCompletionSource<string>();
        selections[userId] = tcs;

        // Lanzamos la tarea que espera la selección sin bloquear JoinAsync
        WaitJoinAsync(Context, userId, tcs);
    }

    private async Task WaitJoinAsync(SocketCommandContext context, string userId, TaskCompletionSource<string> tcs)
    {
        string selection = await tcs.Task;

        string civilization = selection switch
        {
            "1" => "Cordobeses",
            "2" => "Romanos",
            "3" => "Vikingos"
        };
        jugadores.Add(new Player(userId, civilization));
        await context.Channel.SendMessageAsync($"El jugador {context.User.Username} se ha unido a la partida con la civilización {civilization}.");
        selections.Remove(userId);
    }
///    
    
    [Command("sape")]
    public async Task HolaxdAsync()
    {
        await ReplyAsync("Mama");
    }


    [Command("1")]
    public async Task Selection1() => await Selection("1");
    [Command("2")]
    public async Task Selection2() => await Selection("2");
    [Command("3")]
    public async Task Selection3() => await Selection("3");
    private async Task Selection(string selection)
    {
        string userId = Context.User.Id.ToString();
        if (!selections.ContainsKey(Context.User.Id.ToString()))
        {
            await ReplyAsync("No tiene nada por elegir");
        }
        selections[Context.User.Id.ToString()].SetResult(selection);
    }
}