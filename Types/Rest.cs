using System.Threading.Tasks;
using Discord.Rest;
using SDC_Sharp.DiscordNet.Interfaces;

namespace SDC_Sharp.DiscordNet.Types;

public class Rest : IRest
{
	private readonly DiscordRestClient m_client;

	public Rest(DiscordRestClient client)
	{
		m_client = client;
	}

	public ulong CurrentUserId => m_client.CurrentUser.Id;

	public Task<RestUser> GetUserAsync(ulong userId)
	{
		return m_client.GetUserAsync(userId);
	}

	public Task<RestGuild> GetGuildAsync(ulong guildId)
	{
		return m_client.GetGuildAsync(guildId);
	}
}