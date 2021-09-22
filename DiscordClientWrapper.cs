using System;
using Discord.WebSocket;
using SDC_Sharp.DiscordNet.Types;

namespace SDC_Sharp.DiscordNet
{
    public class DiscordClientWrapper : IDiscordClientWrapper
    {
        internal readonly DiscordSocketClient _client;

        public DiscordClientWrapper(DiscordSocketClient client)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
        }

        BaseSocketClient IDiscordClientWrapper.Client => _client;

        public ulong CurrentUserId => _client.CurrentUser.Id;

        public int ServersCount => _client.Guilds.Count;

        public int ShardCount => 1;
    }
}