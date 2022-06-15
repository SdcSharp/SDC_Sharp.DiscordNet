using Discord.WebSocket;

namespace SDC_Sharp.DiscordNet.Types
{
    public sealed class DefaultConfig : IClientConfig
    {
        private readonly DiscordSocketClient _client;

        internal DefaultConfig(DiscordSocketClient client)
        {
            _client = client;
        }

        public uint GuildsCount => (uint) _client.Guilds.Count;
        public uint ShardsCount => 1;
        public DiscordSocketRestClient Rest => _client.Rest;
    }
}