using System;
using Discord.WebSocket;
using SDC_Sharp.DiscordNet.Types;

namespace SDC_Sharp
{
    public class DiscordClientWrapper : DiscordClientWrapperBase
    {
        private readonly DiscordSocketClient _client;

        public DiscordClientWrapper(DiscordSocketClient client)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
            SdcSharpExtensions.Discord = _client;
        }

        public override ulong CurrentUserId => _client.CurrentUser.Id;

        public override int ServersCount => _client.Guilds.Count;

        public override int ShardCount => 1;
    }
}