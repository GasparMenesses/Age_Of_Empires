using Discord.Commands;
using System.Threading.Tasks;

public class GeneralModule : ModuleBase<SocketCommandContext>
{
    [Command("hola")]
    public async Task HolaAsync()
    {
        await ReplyAsync("Que hace loco");
    }

    [Command("ping")]
    public async Task PingAsync()
    {
        await ReplyAsync("Pong");
    }
}