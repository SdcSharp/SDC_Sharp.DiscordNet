using Discord;
using Discord.WebSocket;

namespace SDC_Sharp.DiscordNet.Types
{
	public sealed class DefaultConfig : IClientConfig
	{
		private readonly DiscordSocketClient m_client;

		internal DefaultConfig(DiscordSocketClient client)
		{
			m_client = client;
		}

		internal DefaultConfig(IDiscordClient client)
		{
			m_client = client as DiscordSocketClient;
		}

		public uint GuildsCount => (uint)m_client.Guilds.Count;
		public uint ShardsCount => 1;
		public DiscordSocketRestClient Rest => m_client.Rest;
	}
}