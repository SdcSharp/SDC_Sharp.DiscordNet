using System.Linq;
using Discord.WebSocket;

namespace SDC_Sharp.DiscordNet.Types
{
	public sealed class ShardedConfig : IClientConfig
	{
		private readonly DiscordShardedClient m_client;

		internal ShardedConfig(DiscordShardedClient client)
		{
			m_client = client;
		}

		internal ShardedConfig(IDiscordClient client)
		{
			m_client = client as DiscordShardedClient;
		}

		public uint GuildsCount => (uint)m_client.Shards.Select(x => x.Guilds.Count).Sum();
		public uint ShardsCount => (uint)m_client.Shards.Count;
		public DiscordSocketRestClient Rest => m_client.Rest;
	}
}