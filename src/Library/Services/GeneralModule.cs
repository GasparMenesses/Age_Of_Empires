using Discord.Commands;
using System.Threading.Tasks;
using Library.Core;




public class GeneralModule : ModuleBase<SocketCommandContext>
{
    List<Player> jugadores = new List<Player>();
    [Command("Mapa")]
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
    
    [Command("sape")]
    public async Task HolaxdAsync()
    {
        await ReplyAsync("Mama");
    }
}