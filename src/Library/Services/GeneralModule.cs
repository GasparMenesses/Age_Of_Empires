using Discord.Commands;
using Library.Core;
using Facade;
using Library.Exceptions;
using Library.Units;
using Microsoft.VisualBasic;

namespace GeneralModule;

public class GeneralModule : ModuleBase<SocketCommandContext>
{
    // ----------------------------
    // Variables y estructuras del juego
    // ----------------------------
    
    private static Fachada fachada = new Fachada(); // Fachada para manejar lógica del juego
    private List<Player> jugadores => Fachada.jugadores; // Jugadores actuales en la partida
    private static int phase = 0; // 0: Sin partida, 1: Esperando jugadores, 2: Juego en curso, 3: Finalizado

    // Diccionarios para manejar selecciones y validaciones
    static Dictionary<string, TaskCompletionSource<string>> selections = new();
    static Dictionary<string, bool> boolPlayers = new();

    // Comandos disponibles por fase
    static Dictionary<int, List<string>> commands = new Dictionary<int, List<string>>()
    {
        {0, new List<string>{"CrearPartida"}},
        {1, new List<string>{"Unirse", "Iniciar"}},
        {2, new List<string>{"Mapa", "Add"}},
        {3, new List<string>{"Resumen"}}
    };

    // Ruta del archivo HTML del mapa
    private static string RelativeMapURL = "../../../../../MapaHtml/mapa_generado.html";
    private static string AbstoluteMapURL = Path.GetFullPath(RelativeMapURL).Replace("\\", "/");
    
    
    //////////////////////////////////////////////////////////////
    //////////////////// Configuarción incial ////////////////////
    //////////////////////////////////////////////////////////////
    
    // ----------------------------
    // Comando: CrearPartida
    // ----------------------------
    [Command("CrearPartida")]
    public async Task CrearJuegoAsync()
    {
        if (phase != 0)
        {
            await ReplyAsync("Ya hay una partida en curso, por favor, espere a que finalice.");
            return;
        }

        phase += 1;
        await ReplyAsync(
            $"Bienvenidos a **⚔️AGE OF EMPIRES⚔️**\n🎮 Es hora de preparar el juego...\n" +
            $"Comandos disponibles:\n{string.Join("\n", commands[1])}"
        );
    }

    // ----------------------------
    // Comando: Iniciar
    // ----------------------------
    [Command("Iniciar")]
    public async Task IniciarJuegoAsync()
    {
        if (phase < 1)
        {
            await ReplyAsync("No hay una partida creada, usá **!crearpartida** primero.");
            return;
        }

        if (jugadores.Count == 0)
        {
            await ReplyAsync("Debe unirse al menos un jugador. Usá **!unirse**.");
            return;
        }

        if (phase > 1)
        {
            await ReplyAsync("Ya hay una partida en curso.");
            return;
        }

        // Si todo está bien, arrancamos
        phase += 1;
        await ReplyAsync("Cargando entorno de juego...");
        Thread.Sleep(1000);
        fachada.CrearEntornoJuego();

        await ReplyAsync("Accede al nuevo mapa (recordá recargar con F5 si ya lo abriste antes):");
        await ReplyAsync("Pega esta URL en tu navegador: **" + AbstoluteMapURL + "**");
    }

    // ----------------------------
    // Comando: Mapa
    // ----------------------------
    [Command("Mapa")]
    public async Task MostrarMapaAsync()
    {
        await ReplyAsync("Pega esta URL en tu navegador: " + AbstoluteMapURL);
    }

    // ----------------------------
    // Comando: Add (testeo, suma dos números)
    // ----------------------------
    [Command("Add")]
    public async Task Add(int n1, int n2)
    {
        int result = n1 + n2;
        await ReplyAsync($"El resultado es: {result}");
    }

    // ----------------------------
    // Comando: Unirse
    // ----------------------------
    [Command("Unirse")]
    public async Task UnirseAsync()
    {
        switch (phase)
        {
            case 0:
                await ReplyAsync("No hay una partida creada, usá **!crearpartida**.");
                return;
            case 2:
                await ReplyAsync("La partida ya está en curso, no podés unirte ahora.");
                return;
            case 3:
                await ReplyAsync("La partida ya terminó, esperá a que empiece una nueva.");
                return;
        }

        string userId = Context.User.Id.ToString();

        // Verificamos si tiene una selección pendiente
        try
        {
            await RespuestaPendiente(Context);
        }
        catch (SeleccionPendienteException e)
        {
            await ReplyAsync(e.Message);
            return;
        }

        // Verificamos si ya está unido
        foreach (Player player in jugadores)
        {
            if (player.Id == userId)
            {
                await ReplyAsync($"El jugador {Context.User.Username} ya está en la partida.");
                return;
            }
        }

        // Enviar mensaje de bienvenida y pedido de civilización
        await ReplyAsync(
            $"Bienvenido {Context.User.Username} a **AGE OF EMPIRES**!\n" +
            "Por favor, seleccioná una civilización:\n" +
            "1. Cordobeses 🕌\n2. Romanos 🏛️\n3. Vikingos 🛶\n*(Ingresá solo el número)*"
        );

        var tcs = new TaskCompletionSource<string>();
        selections[userId] = tcs;

        // Espera la selección del usuario
        WaitUnirseAsync(Context, tcs);
    }

    // ----------------------------
    // Espera la selección de civilización
    // ----------------------------
    private async Task WaitUnirseAsync(SocketCommandContext context, TaskCompletionSource<string> tcs)
    {
        string selection = await tcs.Task;
        fachada.CrearJugador(context, selection);

        await context.Channel.SendMessageAsync(
            $"El jugador {context.User.Username} se ha unido con la civilización " +
            $"{jugadores[jugadores.Count - 1].Civilization.NombreCivilizacion}."
        );

        selections.Remove(context.User.Id.ToString());
    }
    
    //////////////////////////////////////////////////////////////
    ///////////////////// Post Configuarción /////////////////////
    //////////////////////////////////////////////////////////////
    
    // ----------------------------
    // Comando: Recolectar
    // ----------------------------
    [Command("RecolectarRecursos")]
    public async Task RecolectarRecursosAsync()
    {
        if (phase < 2)
        {
            await ReplyAsync("No hay una partida en curso, usá **!iniciar**.");
            return;
        }
        
        // Verificamos si tiene una selección pendiente
        try
        {
            await RespuestaPendiente(Context);
        }
        catch (SeleccionPendienteException e)
        {
            await ReplyAsync(e.Message);
            return;
        }
        
        string userId = Context.User.Id.ToString();
        await ReplyAsync("Que elemento deseas recolectar " + Context.User.Username + "?");
        await ReplyAsync($"Ingrese **!N** donde N = **\n 1 - Recolectar Madera\n 2 - Recolectar Piedra\n 3 - Recolectar Oro\n 4 - Recolectar Comida**" );
        
        
        int numeroAldeanos = jugadores.FirstOrDefault(j => j.Id == userId)?.Units.OfType<Villager>().Count() ?? 0;

        var tcs = new TaskCompletionSource<string>();
        selections[userId] = tcs;

        // Espera la selección del usuario
        _ = WaitRecolectarAsync(Context, tcs, numeroAldeanos);
        
        await ReplyAsync(
            $"El jugador {Context.User.Username} dispone de los recursos:\n" + jugadores.FirstOrDefault(j => j.Id == Context.User.Id.ToString()).Resources
        );
    }
    
    private async Task WaitRecolectarAsync(SocketCommandContext context, TaskCompletionSource<string> tcs, int numeroAldeanos )
    {
        // Verifica si el jugador tiene aldeanos disponibles
        if (numeroAldeanos <= 0)
        {
            await context.Channel.SendMessageAsync(
                $"El jugador {context.User.Username} no tiene aldeanos disponibles para recolectar recursos, dispone de {numeroAldeanos} aldeanos."
            );
            selections.Remove(context.User.Id.ToString());
            return;
        }
        
        // Espera la selección del usuario  
        string selection = await tcs.Task;
        fachada.Recolectar(selection, jugadores.FirstOrDefault(j => j.Id == context.User.Id.ToString()));

        await context.Channel.SendMessageAsync(
            $"El jugador {context.User.Username} ha seleccionado recolectar recursos con la opción: {selection}."
        );

        selections.Remove(context.User.Id.ToString()); // Elimina la selección pendiente del jugador
    }
    
    
        
        
    //////////////////////////////////////////////////////////////
    ////////////////// Manejadores de selección //////////////////
    //////////////////////////////////////////////////////////////
    
    // ----------------------------
    // Comandos de selección: 1, 2, 3
    // ----------------------------
    [Command("1")]
    public async Task Selection1() => await Selection("1");

    [Command("2")]
    public async Task Selection2() => await Selection("2");

    [Command("3")]
    public async Task Selection3() => await Selection("3");
    
    [Command("4")]
    public async Task Selection4() => await Selection("4");

    // Manejo genérico de selección de civilización
    private async Task Selection(string selection)
    {
        string userId = Context.User.Id.ToString();

        if (!selections.ContainsKey(userId))
        {
            await ReplyAsync("No tenés ninguna selección pendiente.");
            return;
        }

        selections[userId].SetResult(selection);
    }

    // ----------------------------
    // Validación de selección pendiente
    // ----------------------------
    private async Task RespuestaPendiente(SocketCommandContext context)
    {
        if (selections.ContainsKey(context.User.Id.ToString()))
        {
            throw new SeleccionPendienteException(
                $"El jugador {context.User.Username} ya tiene una selección pendiente."
            );
        }
    }
}
