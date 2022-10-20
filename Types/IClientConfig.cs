using Discord.WebSocket;

namespace SDC_Sharp.DiscordNet.Types
{
	public interface IClientConfig
	{
		public uint GuildsCount { get; }
		public uint ShardsCount { get; }
		public DiscordSocketRestClient Rest { get; }
	}
}