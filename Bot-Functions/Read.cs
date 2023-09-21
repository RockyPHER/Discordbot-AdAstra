using Discord;
using Discord.WebSocket;

namespace DiscordBot{
    public class Read{

        private Program? client;
        public async Task Message(SocketMessage message){

            if (message.Author.Id == client?._client?.CurrentUser.Id) return;

        }

        public async Task Reaction(Cacheable<IUserMessage, ulong> msg, Cacheable<IMessageChannel, ulong> chnl, SocketReaction reaction){
            
            var message = msg.GetOrDownloadAsync().Result;
            var channel = chnl.GetOrDownloadAsync().Result;

            //limitando o escopo para o chat principal.
            //caso não seja enviado no chat principal, não será lido.
            if (message.Channel.Id != 1136045626312896537) return;

            if (message.Reactions.Count < 3) return;

            await 

        }

    }
}