using Discord.Commands;
using Library.Core;
using Facade;
using Library.Exceptions;
using Library.Units;
using Library.Interfaces;

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
        {0, new List<string>{"!CrearPartida"}},
        {1, new List<string>{"!Unirse", "!Iniciar"}},
        {2, new List<string>{"!Mapa", ""}},
        {3, new List<string>{"!Resumen"}}
    };

    // Ruta del archivo HTML del mapa
    private static string RelativeMapURL = "../../../../../MapaHtml/mapa_generado.html";
    private static string AbstoluteMapURL = Path.GetFullPath(RelativeMapURL).Replace("\\", "/");
    
    //////////////////////////////////////////////////////////////
    //////////////////// Comandos universales ////////////////////
    //////////////////////////////////////////////////////////////
    //
    // Comando: Comandos
    //
    [Command("Comandos")]
    public async Task ComandosAsync()
    {
        if (phase == 0)
        {
            await ReplyAsync("No hay una partida creada, usá **!crearpartida**.");
            return;
        }

        if (phase == 1)
        {
            await ReplyAsync($"Comandos disponibles:\n{string.Join("\n", commands[1])}");
            return;
        }

        if (phase == 2)
        {
            await ReplyAsync($"Comandos disponibles:\n{string.Join("\n", commands[2])}");
            return;
        }

        if (phase == 3)
        {
            await ReplyAsync($"Comandos disponibles:\n{string.Join("\n", commands[3])}");
            return;
        }
    }
    
    
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
            $"Bienvenidos a **⚔️AGE OF EMPIRES⚔️**\n🎮 Es hora de preparar el juego...\n");
        ComandosAsync();
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
        
        boolPlayers[Context.User.Id.ToString()] = true; // Marca al jugador como listo
        
        foreach (var boolPlayer in boolPlayers)
        {
            if (boolPlayer.Value == false)
            {
                await ReplyAsync("Aun hay jugadores que no estan listos para iniciar la partida.");
                return;
            }
        }
        var keys = boolPlayers.Keys.ToList();
        foreach (var key in keys)
        {
            boolPlayers[key] = false;
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
        if (phase != 2)
        {
            await ReplyAsync("Todavía no podés ver el mapa, usá **!iniciar** para comenzar la partida.");
            return;
        }
        await ReplyAsync("Pega esta URL en tu navegador: " + AbstoluteMapURL);
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
        boolPlayers.Add(userId, false); // Marca al jugador como no listo
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
        List<string> options = new List<string>{"1", "2", "3"};
        if (!options.Contains(selection))
        {
            await ReplyAsync("Opción inválida. Por favor, ingresá 1, 2 o 3.");
    
            // Vuelvo a crear otra espera para que elija bien
            var nuevoTCS = new TaskCompletionSource<string>();
            selections[context.User.Id.ToString()] = nuevoTCS;
            WaitUnirseAsync(context, nuevoTCS); // <-- reintento
            return;
        }
        try
        {
            await fachada.CrearJugador(context, selection);
        }
        catch (Exception e)
        {
            await ReplyAsync(e.Message);
            return;
        }
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

        var tcs = new TaskCompletionSource<string>();
        selections[userId] = tcs;

        // Espera la selección del usuario
        _ = WaitRecolectarAsync(Context, tcs);
        
    }
    
    private async Task WaitRecolectarAsync(SocketCommandContext context, TaskCompletionSource<string> tcs )
    {
        // Espera la selección del usuario  
        string selection = await tcs.Task;
        Player jugador = jugadores.FirstOrDefault(j => j.Id == Context.User.Id.ToString());
        try
        {
            fachada.Recolectar(selection, jugador);
        }
        catch (UnidadNoDisponibleException e)
        {
            await ReplyAsync(e.Message);
            return;
        }
        catch (InvalidOperationException e)
        {
            await ReplyAsync(e.Message);
            return;
        }
        await context.Channel.SendMessageAsync(
            $"El jugador {context.User.Username} ha seleccionado recolectar recursos con la opción: {selection}."
        );
        selections.Remove(context.User.Id.ToString()); // Elimina la selección pendiente del jugador
        
    }
    // ----------------------------
    // Comando: Ver Recursos
    // ----------------------------
    [Command("RecursosDisponibles")]
    public async Task MostrarRecursosAsync()
    {
        Player jugador = jugadores.FirstOrDefault(j => j.Id == Context.User.Id.ToString());
        await ReplyAsync( 
            $"El jugador {Context.User.Username} dispone de los recursos:\n " +
            $"ORO: {jugador.Resources.Gold.ToString()}\n MADERA: {jugador.Resources.Wood.ToString()}\n PIEDRA: {jugador.Resources.Stone.ToString()}\n COMIDA: {jugador.Resources.Food.ToString()}\n "
        );
    }
    // ----------------------------
    // Comando: Atacar unidades
    // ----------------------------
    [Command("Atacar")]
    public async Task AtacarAsync()
    {
        // Verifica que la partida esté en curso
        if (phase < 2)
        {
            await ReplyAsync("No hay una partida en curso. Usá **!iniciar**.");
            return;
        }

        string userId = Context.User.Id.ToString();

        // Busca al jugador según su ID de Discord
        var jugador = jugadores.FirstOrDefault(j => j.Id == userId);
        if (jugador == null)
        {
            await ReplyAsync("No estás en la partida.");
            return;
        }

        // Verifica si el jugador tiene unidades
        if (jugador.Units.Count == 0)
        {
            await ReplyAsync("No tenés unidades disponibles para atacar.");
            return;
        }

        // Muestra las unidades disponibles del jugador para elegir como atacante
        await ReplyAsync("Seleccioná la unidad atacante. Usá **!N** donde N es el índice de tu unidad:");
        for (int i = 0; i < jugador.Units.Count; i++)
        {
            var unidad = jugador.Units[i];
            await ReplyAsync($"**{i}** - Unidad con vida: {unidad.Life}, posición: ({unidad.Position["x"]},{unidad.Position["y"]})");
        }

        // Crea una tarea pendiente de respuesta y la guarda por ID de usuario
        var tcs = new TaskCompletionSource<string>();
        selections[userId] = tcs;

        // Espera que el jugador elija el atacante
        _ = WaitIndiceAtacanteAsync(Context, jugador, tcs);
    }

    private async Task WaitIndiceAtacanteAsync(SocketCommandContext context, Player jugador, TaskCompletionSource<string> tcs)
    {
        string userId = context.User.Id.ToString();

        // Espera la respuesta del jugador (índice del atacante)
        string input = await tcs.Task;

        // Verifica que el input sea un número válido y dentro del rango de unidades del jugador
        if (!int.TryParse(input, out int indiceAtacante) || indiceAtacante < 0 || indiceAtacante >= jugador.Units.Count)
        {
            await context.Channel.SendMessageAsync(" Índice de atacante inválido.");
            selections.Remove(userId);
            return;
        }

        var atacante = jugador.Units[indiceAtacante];

        // Busca todas las unidades enemigas (de otros jugadores)
        var enemigos = jugadores
            .Where(j => j != jugador)
            .SelectMany(j => j.Units)
            .ToList();

        if (enemigos.Count == 0)
        {
            await context.Channel.SendMessageAsync("No hay unidades enemigas para atacar.");
            selections.Remove(userId);
            return;
        }

        // Muestra las unidades enemigas disponibles para atacar
        await context.Channel.SendMessageAsync("Seleccioná al objetivo enemigo. Usá **!N** donde N es el índice:");
        for (int i = 0; i < enemigos.Count; i++)
        {
            var u = enemigos[i];
            await context.Channel.SendMessageAsync($"**{i}** - Enemigo con vida: {u.Life}, posición: ({u.Position["x"]},{u.Position["y"]})");
        }

        // Crea otra tarea pendiente para que el jugador elija al objetivo
        var tcsObjetivo = new TaskCompletionSource<string>();
        selections[userId] = tcsObjetivo;

        // Espera la selección del objetivo
        _ = WaitIndiceObjetivoAsync(context, jugador, atacante, enemigos, tcsObjetivo);
    }

    private async Task WaitIndiceObjetivoAsync(SocketCommandContext context, Player jugador, IUnit atacante, List<IUnit> enemigos, TaskCompletionSource<string> tcs)
    {
        string userId = context.User.Id.ToString();

        // Espera la respuesta del jugador (índice del objetivo)
        string input = await tcs.Task;

        // Verifica que el input sea un número válido y dentro del rango de enemigos
        if (!int.TryParse(input, out int indiceObjetivo) || indiceObjetivo < 0 || indiceObjetivo >= enemigos.Count)
        {
            await context.Channel.SendMessageAsync(" Índice de objetivo inválido.");
            selections.Remove(userId);
            return;
        }

        var objetivo = enemigos[indiceObjetivo];

        // Ejecuta el ataque usando la fachada
        fachada.AtacarUnidades(new List<IUnit> { atacante }, new List<IUnit> { objetivo });

        // Muestra el resultado del ataque
        await context.Channel.SendMessageAsync($" ¡Ataque realizado! La unidad enemiga ahora tiene {objetivo.Life} de vida.");

        // Si la vida del objetivo llegó a 0 o menos, se eliminó
        if (objetivo.Life <= 0)
        {
            await context.Channel.SendMessageAsync($" La unidad enemiga ha sido eliminada.");
        }

        // Limpia la selección pendiente del jugador
        selections.Remove(userId);
    }

    
    // ----------------------------
    // Comando: VerUnidades
    // ----------------------------
    [Command("MisUnidades")]
    public async Task MostrarUnidadesAsync()
    {
        var jugador = jugadores.FirstOrDefault(j => j.Id == Context.User.Id.ToString());
        if (jugador == null)
        {
            await ReplyAsync("No estás en la partida.");
            return;
        }

        if (jugador.Units.Count == 0)
        {
            await ReplyAsync("No tenés unidades disponibles.");
            return;
        }

        var mensaje = "Tus unidades:\n";
        for (int i = 0; i < jugador.Units.Count; i++)
        {
            var unidad = jugador.Units[i];
            mensaje += $"[{i}] {unidad.GetType().Name} - Vida: {unidad.Life}, Ataque: {unidad.Attack}, Defensa: {unidad.Defense}\n";
        }

        await ReplyAsync(mensaje);
    }

    // ----------------------------
    // Comando: Construir
    // ----------------------------
    [Command("Construir")]
    public async Task ConstruirAsync([Remainder] string coords = null)
    {
        if (phase != 2)
        {
            await ReplyAsync("Todavía no podés construir, usá *!iniciar* para comenzar la partida.");
            return;
        }

        if (coords == null)
        {
            await ReplyAsync("Faltan las coordenadas. Usá !Construir x,y (ej: !Construir 4,6).");
            return;
        }
        var parts = coords.Split(',');
        if (parts.Length != 2 || !int.TryParse(parts[0], out var x) || !int.TryParse(parts[1], out var y))
        {
            await ReplyAsync("Formato inválido. Usá: !Construir x,y (ej: !Construir 4,6).");
            return;
        }

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
        await ReplyAsync("¿Qué tipo de almacén querés construir?\n" +
                         "*1* - Almacén de Madera 🌲\n" +
                         "*2* - Almacén de Piedra 🪨\n" +
                         "*3* - Almacén de Oro 🪙");

        var tcs = new TaskCompletionSource<string>();
        selections[userId] = tcs;
        _ = WaitTipoAlmacenAsync(Context, tcs, x, y);
    }

    private async Task WaitTipoAlmacenAsync(SocketCommandContext context, TaskCompletionSource<string> tcs, int x, int y)
    {
        string selection = await tcs.Task;
        string userId = context.User.Id.ToString();

        Dictionary<string, string> tipoAlmacen = new()
        {
            {"1", "Madera"},
            {"2", "Piedra"},
            {"3", "Oro"}
        };

        if (!tipoAlmacen.ContainsKey(selection))
        {
            await ReplyAsync("Selección inválida. Ingresá 1, 2 o 3.");
            var nuevoTcs = new TaskCompletionSource<string>();
            selections[userId] = nuevoTcs;
            _ = WaitTipoAlmacenAsync(context, nuevoTcs, x, y);
            return;
        }

        var jugador = jugadores.FirstOrDefault(j => j.Id == userId);

        try
        {
            switch (tipoAlmacen[selection])
            {
                case "Madera":
                    jugador.Actions.Build("WoodStorage", (x, y));
                    break;
                case "Piedra":
                    jugador.Actions.Build("StoneStorage", (x, y));
                    break;
                case "Oro":
                    jugador.Actions.Build("GoldStorage", (x, y));
                    break;
            }
        }
        catch (RecursosInsuficientesException e)
        {
            await ReplyAsync(e.Message);
            selections.Remove(userId);
            return;
        }
        catch (Exception e)
        {
            await ReplyAsync("Error al construir: " + e.Message);
            selections.Remove(userId);
            return;
        }

        await ReplyAsync($"🏗 Almacén de {tipoAlmacen[selection]} construyéndose en ({x},{y}).");
        fachada.ActualizarMapa(); // Actualiza el mapa después de construir
        selections.Remove(userId);
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
                $"El jugador {context.User.Username} tiene una selección pendiente."
            );
        }
    }
}
