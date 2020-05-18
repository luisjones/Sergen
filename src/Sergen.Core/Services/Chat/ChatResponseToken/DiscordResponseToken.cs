using System.Security.Cryptography;
using System;
using System.Threading.Tasks;
using Discord;
using Discord.Rest;
using Discord.WebSocket;
using Sergen.Core.Services.Chat.StaticHelpers;

namespace  Sergen.Core.Services.Chat.ChatResponseToken
{
    public class DiscordResponseToken : IChatResponseToken
    {
        ISocketMessageChannel _responseChannel;

        RestUserMessage _lastMessagedInteractedWith;

        public DiscordResponseToken (ISocketMessageChannel channel)
        {
            _responseChannel = channel;
        }

        public async Task<string> Respond(string response)
        {
            var embed = DiscordHelper.CreateEmbeddedMessage(response);
            
            var mes = await _responseChannel.SendMessageAsync(embed:embed);
            
            _lastMessagedInteractedWith = mes;
            return mes.Id.ToString();
        }

        public async Task Update(string messageID, string update)
        {
            var message = await _responseChannel.GetMessageAsync(Convert.ToUInt64(messageID));
            if(message is RestUserMessage rumess)
            {
                
                var embed = DiscordHelper.CreateEmbeddedMessage(update);
                
                await rumess.ModifyAsync(msg => msg.Embed = embed);
                _lastMessagedInteractedWith = rumess;
            }
        }

        public async Task UpdateLastInteractedWithMessage(string update)
        {
            if(_lastMessagedInteractedWith != null)
            {
                var embed = DiscordHelper.CreateEmbeddedMessage(update);
                
                _lastMessagedInteractedWith.ModifyAsync(msg => msg.Embed = embed);
            }
        }
    }
}