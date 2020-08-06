using System.Threading.Tasks;
using Discord.Commands;

namespace lofee.Commands
{
    public class TestingModule : ModuleBase<SocketCommandContext>
    {
        [Command("ping")]
        public async Task PingAsync(string msg)
        {
            await ReplyAsync(msg);
        }
    }
}