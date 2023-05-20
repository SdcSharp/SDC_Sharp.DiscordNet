using Discord;
using Discord.WebSocket;
using SDC_Sharp.DiscordNet.Interfaces;

namespace SDC_Sharp.DiscordNet.Types;

public sealed class DefaultConfig : IClientConfig
{
	private readonly DiscordSocketClient m_client;

	public DefaultConfig(DiscordSocketClient client)
	{
		m_client = client;
		Rest = new Rest(client.Rest);
	}

	public DefaultConfig(IDiscordClient client) : this(client as DiscordSocketClient)
	{
	}

	public uint GuildsCount => (uint)m_client.Guilds.Count;
	public uint ShardsCount => 1;
	public IRest Rest { get; }
}