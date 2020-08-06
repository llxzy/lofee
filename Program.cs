using System;
using System.IO;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.Configuration;

namespace lofee
{
    public class Program
    {
        private DiscordSocketClient _client;
        private IConfiguration _configuration;

        private readonly string _configPath =
            AppContext.BaseDirectory + Path.DirectorySeparatorChar + "appsettings.json";

        private Task Log(LogMessage msg)
        {
            Console.WriteLine(msg.ToString());
            return Task.CompletedTask;
        }
        public static void Main(string[] args)
            => new Program().MainAsync().GetAwaiter().GetResult();

        public async Task MainAsync()
        {
            _configuration = new ConfigurationBuilder().SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json").Build();

            var botConnectionString = _configuration[$"BotConnectionString"];

            _client = new DiscordSocketClient();
            _client.Log += Log;
            
            var handler = new CommandHandler(_client, new CommandService());
            await handler.InstallCommandsAsync();
            
            await _client.LoginAsync(TokenType.Bot, botConnectionString);
            await _client.StartAsync();
            
            //TODO include closeasync to end bot session
            
            //Delay Task ending while connection is active
            await Task.Delay(-1);

        }
    }
}