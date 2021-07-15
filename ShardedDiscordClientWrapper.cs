using System;
using Discord.WebSocket;
using SDC_Sharp.DiscordNet.Types;

namespace SDC_Sharp
{
    public class ShardedDiscordClientWrapper : DiscordClientWrapperBase
    {
        private readonly DiscordShardedClient _client;

        public ShardedDiscordClientWrapper(DiscordShardedClient client)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
            SdcSharpExtensions.Discord = _client;
        }

        public override ulong CurrentUserId => _client.CurrentUser.Id;
        public override int ServersCount => _client.Guilds.Count;
        public override int ShardCount => _client.Shards.Count;
    }
}