using System;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;
using Microsoft.Extensions.Configuration;

namespace lofee
{
    public class Program
    {
        private DiscordSocketClient _client;
        private IConfiguration _configuration;

        private Task Log(LogMessage msg)
        {
            Console.WriteLine(msg.ToString());
            return Task.CompletedTask;
        }
        public static void Main(string[] args)
            => new Program().MainAsync().GetAwaiter().GetResult();

        public async Task MainAsync()
        {
            // TODO creating appsettings if it doesnt exist
            _configuration = new ConfigurationBuilder().SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json").Build();

            var botConnectionString = _configuration[$"BotConnectionString"];
            
            _client = new DiscordSocketClient();
            _client.Log += Log;
            
            await _client.LoginAsync(TokenType.Bot, botConnectionString);
            await _client.StartAsync();
            
            //TODO include closeasync to end bot session
            
            //Delay Task ending while connection is active
            await Task.Delay(-1);

        }
    }
}