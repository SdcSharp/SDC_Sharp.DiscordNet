using System.Linq;
using Discord.WebSocket;

namespace SDC_Sharp.DiscordNet.Types
{
    public sealed class ShardedConfig : IClientConfig
    {
        private readonly DiscordShardedClient _client;

        internal ShardedConfig(DiscordShardedClient client)
        {
            _client = client;
        }

        public uint GuildsCount => (uint) _client.Shards.Select(x => x.Guilds.Count).Sum();
        public uint ShardsCount => (uint) _client.Shards.Count;
        public DiscordSocketRestClient Rest => _client.Rest;
    }
}