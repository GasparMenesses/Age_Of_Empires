using Discord.Commands;
using System.Threading.Tasks;
using Library.Core;

public class GeneralModule : ModuleBase<SocketCommandContext>
{
    // private Fachada fachada = new Fachada();
    //
    // [Command("Comenzar")]
    // public async Task StartNewGameAsync()
    // {
    //     var username = Context.User.Username;
    //     await ReplyAsync($"🎮 Bienvenido a **AGE OF EMPIRES**, {username}! Vamos a configurar la partida ⚔️");
    //     fachada.Comenzar();
    //
    // }
    
    
    [Command("PrintMapa")]
    public async Task HolaAsync()
    {
        new Map();
        await ReplyAsync(("http://localhost:63342/Age_Of_Empires/src/Library/html/index.html?_ijt=gj5tbh1o5snvg6rnpkjdm1u0bo&_ij_reload=RELOAD_ON_SAVE"));
    }
    
    
    [Command("Add")]
    
    public async Task Add(int n1, int n2)
    {
        int result = n1 + n2;
        await ReplyAsync($"El resultado es: {result}");
    }

}