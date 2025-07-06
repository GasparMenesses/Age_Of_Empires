using Discord.Commands;
using Library.Core;
using Facade;
using Library.Exceptions;
using Microsoft.VisualBasic;

namespace GeneralModule; 
public class GeneralModule : ModuleBase<SocketCommandContext>
{
    private static Fachada fachada = new Fachada();
    private List<Player> jugadores => Fachada.jugadores; // Lista de jugadores, se obtiene de la fachada
    private static int phase = 0; // Fases del juego, 1: Generacion de la partida, 2: Acciones en partida, 3: Finalizacion de la partida
    static Dictionary<string,TaskCompletionSource<string>> selections = new Dictionary<string,TaskCompletionSource<string>>();
    static Dictionary<string,bool> boolPlayers = new Dictionary<string,bool>();
    static Dictionary<int, List<string>> commands = new Dictionary<int, List<string>>()
    {
        {0, new List<string>{"CrearPartida"}},
        {1, new List<string>{"Unirse", "Iniciar"}},
        {2, new List<string>{"Mapa" , "Add"}},
        {3, new List<string>{"Resumen"}}
    };

    private static string RelativeMapURL = "../../../../../MapaHtml/mapa_generado.html"; // URL relativa del mapa generado
    private static string AbstoluteMapURL = Path.GetFullPath(RelativeMapURL).Replace("\\", "/");
    
    
    [Command("CrearPartida")]
    public async Task CrearJuegoAsync()
    {
        if (phase != 0)
        {
            await ReplyAsync("Ya hay una partida en curso, por favor, espere a que finalice.");
            return;
        }
        phase += 1; // Cambia la fase a 1, indicando que se está creando una partida
        await ReplyAsync(
            $"Bienvenidos a **⚔️AGE OF EMPIRES⚔️**\n🎮Es hora de preparar el juego...\nCuentan con los siguientes comandos:\n" +
            $"{string.Join("\n", commands[1])}\n");
    }
    
    [Command("Iniciar")]
    public async Task IniciarJuegoAsync()
    {

        if (phase < 1)
        {
            await ReplyAsync("No hay una partida creada, por favor, crea una partida primero: **!crearpartida**");
            return;
        }
        else if (jugadores.Count == 0)
        {
            await ReplyAsync("Debe unirse al menos un jugador, por favor, crea un jugador primero: **!unirse**");
            return;
        } 
        else if (phase > 1)
        {
            await ReplyAsync("Ya hay una partida en curso, por favor, espere a que finalice.");
            return;
        }
        else
        {
            // Código para phase == 1
            phase += 1; // Cambia la fase a 2, indicando que se está iniciando una partida
            await ReplyAsync("Cargando entonrno de juego...");
            Thread.Sleep(1000);
            fachada.CrearEntornoJuego(); // Crea el entorno del juego, incluyendo el mapa y los edificios
            await ReplyAsync("Accede al nuevo mapa, recuerda que debes recargarlo con F5");
            await ReplyAsync("Pega esta URL en tu navegador: **" + AbstoluteMapURL + "**");
            
        }
    }

    
    [Command("Mapa")]
    public async Task HolaAsync()
    {
        await ReplyAsync("Pega esta URL en tu navegador: " + AbstoluteMapURL);
    }
    
    
    [Command("Add")]
    
    public async Task Add(int n1, int n2)
    {
        int result = n1 + n2;
        await ReplyAsync($"El resultado es: {result}");
    }

    ///
    [Command("Unirse")]
    public async Task UnirseAsync()
    {
        switch (phase)
        {
            case 0:
                await ReplyAsync("No hay una partida creada, por favor, crea una partida primero: **!crearpartida**");
                return;
            case 2:
                await ReplyAsync("La partida esta en curso, no puedes unirte.");
                return;
            case 3:
                await ReplyAsync("La partida ha finalizado, espera a que se cree una nueva partida.");
                return;
        }
        string userId = Context.User.Id.ToString();
        try 
        {
            await RespuestaPendiente(Context);
        }
        catch (SeleccionPendienteException e)
        {
            await ReplyAsync(e.Message);
            return;
        }
        foreach (Player player in jugadores)
        {
            if (player.Id == userId)
            {
                await ReplyAsync($"El jugador {Context.User.Username} ya se encuentra en la partida.");
                return;
            }
        }
        await ReplyAsync(
            $"Bienvenido {Context.User.Username} a **AGE OF EMPIRES**, por favor, selecciona una civilización:\n" +
            "1.Cordobeses\n2.Romanos\n3.Vikingos\n(Por favor, ingrese su número)");

        var tcs = new TaskCompletionSource<string>();
        selections[userId] = tcs;

        // Lanzamos la tarea que espera la selección sin bloquear JoinAsync
        WaitUnirseAsync(Context, tcs);
    }

    private async Task WaitUnirseAsync(SocketCommandContext context, TaskCompletionSource<string> tcs)
    {
        string selection = await tcs.Task;
        fachada.CrearJugador(context, selection);
        await context.Channel.SendMessageAsync($"El jugador {context.User.Username} se ha unido a la partida con la civilización {jugadores[jugadores.Count-1].Civilization.NombreCivilizacion}.");
        selections.Remove(context.User.Id.ToString());
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

    private async Task RespuestaPendiente(SocketCommandContext context)
    {
        if (selections.ContainsKey(context.User.Id.ToString()))
        {
            throw new SeleccionPendienteException($"El jugador {Context.User.Username} tiene una seleccion pendiente por elegir");
        }
    }
}