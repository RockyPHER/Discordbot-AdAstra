
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Net.WebSockets;
using System.Reflection;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography.X509Certificates;
using Discord;
using Discord.Rest;
using Discord.WebSocket;
using Microsoft.VisualBasic;

namespace DiscordBot
{
    public class Program
    {
        public DiscordSocketClient? _client;
        public IEmote? _desiredEmote;

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
            _client.MessageReceived += UserCommand;

            await Task.Delay(-1);

        }



        private Task Log(LogMessage msg)
        {

            Console.WriteLine("[DiscordBot] " + msg.ToString());
            return Task.CompletedTask;

        }

        public async Task UserCommand(SocketMessage msg){
            
            var messageContent = msg.Content;
            Random random = new Random();
            int randomNumb = random.Next(0, 4);
            String[] starText = new String[5]{"₊⊹", "⋆⭒˚", "⋆⁺₊⋆", "-࣭-⭑", "⊹˖"};
            var channelId = msg.Channel.Id;

            if (messageContent != "*estrelas") return;

            var editChannel = _client?.GetChannel(channelId) as IGuildChannel;

            var channelName = editChannel?.Name;
            var newName = $"{channelName}{starText[randomNumb]}";


            await editChannel.ModifyAsync(properties => properties.Name = newName);

        }

        public async Task ReactionAdded(Cacheable<IUserMessage, ulong> userMsg, Cacheable<IMessageChannel, ulong> channelMsg, SocketReaction emoji){

            var _desiredEmote = new Emoji("🤣");
            ulong channelTest = 1136045626312896537;
            ulong channelPost = 1154406126780157952;
            var message = userMsg.GetOrDownloadAsync();
            var channel = channelMsg.GetOrDownloadAsync();
            if (emoji.UserId == _client.CurrentUser.Id) return;
            // if (channelMsg.Id != channelTest) return;
            if (emoji.Emote.Name != _desiredEmote.Name) return;
            
            var emojiCount = message.Result.Reactions.GetValueOrDefault<IEmote, ReactionMetadata>(_desiredEmote, default).ReactionCount;
            var messageContent = message.Result.Content;
            var messageImage = message.Result;
            var authorName = message.Result.Author.Username;
            var authorPfp = message.Result.Author.GetAvatarUrl(ImageFormat.Png);
            var timeOfTheMessage = message.Result.Timestamp.LocalDateTime;
            var idOfTheMessage = channel;
            /* TODO: 
            - Add support to images;
            - Add Costum Phrases
            

            */
            var WriteMsg = _client.GetChannel(channelPost) as IMessageChannel;
            
            var _embed = new EmbedBuilder{
                
                Title = "# 🤣"+emojiCount,
                Author = new EmbedAuthorBuilder{

                    Name = authorName,
                    IconUrl = authorPfp

                },
                Color = Color.DarkRed,
                Timestamp = timeOfTheMessage,
                Description = messageContent
                
            

            };

            // _embed.AddField("# 🤣"+message.Result.Reactions.Count+, message.Result.Content, inline: true);

            await WriteMsg.SendMessageAsync(embed : _embed.Build());
            Console.WriteLine(message.Result.Reactions.Values);

            // await WriteMsg.SendMessageAsync($"**Author:** {message.Result.Author.Username}\n\n**Message:**\"{message.Result.Content}\"\n\n**Num.Reaction:** {message.Result.Reactions.Count}");

            return;

        }

        
    }
}