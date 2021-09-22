using Discord.WebSocket;

namespace SDC_Sharp.DiscordNet.Types
{
    public interface IDiscordClientWrapper : SDC_Sharp.Types.IDiscordClientWrapper
    {
        internal BaseSocketClient Client { get; }
    }
}