
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Net.WebSockets;
using System.Reflection;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography.X509Certificates;
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

            ulong channelTest = 1136045626312896537;
            ulong channelPost = 1154406126780157952;
            var message = userMsg.GetOrDownloadAsync();
            var channel = channelMsg.GetOrDownloadAsync();
            if (emoji.UserId == _client.CurrentUser.Id) return;
            if (channelMsg.Id != channelTest) return; 
            // if (emoji.Emote.Name != )
            /* TODO: 
            - Add support to images;
            - Add an emoji identifier;
            */
            var WriteMsg = _client.GetChannel(channelPost) as IMessageChannel;
            
            var _embed = new EmbedBuilder{
    
                Author = new EmbedAuthorBuilder{

                    Name = message.Result.Author.Username,
                    IconUrl = message.Result.Author.GetAvatarUrl(ImageFormat.Png)

                },
                Color = Color.DarkRed,
                Timestamp = message.Result.Timestamp.LocalDateTime,

            };

            _embed.AddField("Nº Reactions:"+message.Result.Reactions.Count, message.Result.Content, inline: true);

            await WriteMsg.SendMessageAsync(embed : _embed.Build());
            Console.WriteLine(message.Result.Reactions.Values);

            // await WriteMsg.SendMessageAsync($"**Author:** {message.Result.Author.Username}\n\n**Message:**\"{message.Result.Content}\"\n\n**Num.Reaction:** {message.Result.Reactions.Count}");

            return;

        }

        
    }
}