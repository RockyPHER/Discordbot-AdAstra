
using System.Net.WebSockets;
using System.Reflection.Metadata.Ecma335;
using Discord;
using Discord.Rest;
using Discord.WebSocket;

namespace DiscordBot
{

    public class Program
    {

        private DiscordSocketClient _client;
        public static Task Main(String[] args) => new Program().MainAsync();


        public async Task MainAsync()
        {

            var config = new DiscordSocketConfig
            {
                GatewayIntents = GatewayIntents.All,
            };

            _client = new DiscordSocketClient(config);
            _client.Log += Log;

            DotNetEnv.Env.Load();

            var token = Environment.GetEnvironmentVariable("token");

            await _client.LoginAsync(TokenType.Bot, token);
            await _client.StartAsync();

            //event manager:

            await Task.Delay(-1);

        }

        private Task Log(LogMessage msg)
        {

            Console.WriteLine("[DiscordBot] " + msg.ToString());
            return Task.CompletedTask;

        }


        
    }
}