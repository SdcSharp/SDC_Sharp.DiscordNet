using System.Linq;
using Discord;
using Discord.WebSocket;
using SDC_Sharp.DiscordNet.Interfaces;

namespace SDC_Sharp.DiscordNet.Types;

public sealed class ShardedConfig : IClientConfig
{
	private readonly DiscordShardedClient m_client;

	public ShardedConfig(DiscordShardedClient client)
	{
		m_client = client;
		Rest = new Rest(client.Rest);
	}

	public ShardedConfig(IDiscordClient client) : this(client as DiscordShardedClient)
	{
	}

	public uint GuildsCount => (uint)m_client.Shards.Select(x => x.Guilds.Count).Sum();
	public uint ShardsCount => (uint)m_client.Shards.Count;
	public IRest Rest { get; }
}