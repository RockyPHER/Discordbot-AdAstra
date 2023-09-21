
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


            // _client.MessageUpdated += MessageUpdated;
            // _client.MessageReceived += MessageReceived;
            // _client.ReactionAdded += ReactionAdded;//async (userMsg, channelMsg, reaction) => {}; LAMBDA
            _client.MessageReceived += MessageResponse;


            await Task.Delay(-1);

        }

        private async Task MessageResponse(SocketMessage msg)
        {
            if(msg.Interaction == null) return;
            Console.WriteLine(msg.Interaction.User.Username);
        }


        // private async Task ReactionAdded(Cacheable<IUserMessage, ulong> userMsg, Cacheable<IMessageChannel, ulong> channelMsg, SocketReaction reaction)
        // {
        //     if (reaction.Emote.Name != "✅") return;

        //     var message = await userMsg.GetOrDownloadAsync();
        //     if ( != message.){

        //     }
        //     //if (message.Reactions.GetValueOrDefault(reaction.Emote).ReactionCount < 3) return;

        //     var channel = _client.GetChannel(message.Channel.Id) as IMessageChannel;
        //     await channel.SendMessageAsync("A seguinte mensagem teve mais de 3 votos: "+message.Content);


        // Console.WriteLine($"-----------------------\nAutor da Mensagem: {message.Author.Username} \nREAÇÃO: {reaction.Emote}\nqtd: {message.Reactions.GetValueOrDefault(reaction.Emote).ReactionCount} \nConteúdo: {message.Content} \nUser: {reaction.User.Value.Username}");
        //

        // private async Task MessageUpdated(Cacheable<IMessage, ulong> before, SocketMessage after, ISocketMessageChannel channel)
        // {

        //     var message = await before.GetOrDownloadAsync();
        //     Console.WriteLine($"Uma mensagem foi editada! {message} -> {after}");

        // }



        // private async Task MessageReceived(SocketMessage msg)
        // {

        //     if (msg.Author.Id == _client.CurrentUser.Id) return;

        //     var channel = _client.GetChannel(msg.Channel.Id) as IMessageChannel;
        //     await channel.SendMessageAsync("É verdade linda rsrs, passa o zap");

        //     // var channel = msg.Channel as SocketGuildChannel;
        //     // Console.WriteLine($"[{msg.CreatedAt.DateTime.ToLongTimeString()}] No servidor: {channel.Guild.Name}\nNo canal: {channel.Name}\n {msg.Author}: {msg.Content}");


        // }

        private Task Log(LogMessage msg)
        {

            Console.WriteLine("[DiscordBot] " + msg.ToString());
            return Task.CompletedTask;

        }
    }
}