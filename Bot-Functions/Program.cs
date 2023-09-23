
using System.Net.WebSockets;
using System.Reflection.Metadata.Ecma335;
using Discord;
using Discord.Rest;
using Discord.WebSocket;

namespace DiscordBot
{
    public class Program
    {
        public DiscordSocketClient? _client;
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


            _client.ReactionAdded += ReactionAdded;

            await Task.Delay(-1);

        }

        private Task Log(LogMessage msg)
        {

            Console.WriteLine("[DiscordBot] " + msg.ToString());
            return Task.CompletedTask;

        }


        public async Task ReactionAdded(Cacheable<IUserMessage, ulong> userMsg, Cacheable<IMessageChannel, ulong> channelMsg, SocketReaction emoji){

            ulong channelTest = 1154406126780157952;
            var message = userMsg.GetOrDownloadAsync();
            var channel = channelMsg.GetOrDownloadAsync();
            if (emoji.UserId == _client.CurrentUser.Id) return;
            if (channelMsg.Id != channelTest) return; 

            var WriteMsg = _client.GetChannel(channelTest) as IMessageChannel;

            await WriteMsg.SendMessageAsync("Reaction Detected");

            Console.WriteLine($"---------------------\n{message.Result.Author.Username}: {message.Result.Content}");
            return;

        }

        
    }
}