using System.Threading.Tasks;
using Discord.Rest;

namespace SDC_Sharp.DiscordNet.Interfaces;

public interface IRest
{
	public ulong CurrentUserId { get; }
	public Task<RestUser> GetUserAsync(ulong userId);
	public Task<RestGuild> GetGuildAsync(ulong guildId);
}