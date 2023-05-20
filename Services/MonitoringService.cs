#nullable enable
using System.Collections.Generic;
using System.Threading.Tasks;
using Discord;
using SDC_Sharp.DiscordNet.Interfaces;
using SDC_Sharp.DiscordNet.Models;
using SDC_Sharp.Services;
using SDC_Sharp.Types.Enums;
using SDC_Sharp.Types.Monitoring;
using Rates = System.Collections.Generic.Dictionary<ulong, SDC_Sharp.Types.Enums.Rate>;

namespace SDC_Sharp.DiscordNet.Services;

public sealed class MonitoringService : BaseMonitoringService
{
	private readonly IClientConfig m_clientConfig;

	public MonitoringService(ISdcSharpClient client, ISdcServices sdcServices) : base(client)
	{
		m_clientConfig = sdcServices.Client;
	}

	public async Task<ulong> GetGuildPlace(ulong guildId)
	{
		return (await GetGuildPlace<GuildPlace>(guildId)).Place;
	}

	public async Task<Guild> GetGuild(ulong guildId, bool fetch = false)
	{
		var guild = await GetGuild<Guild>(guildId);
		guild.Id = guildId;
		guild.Avatar = "https://cdn.discordapp.com/icons/" + guildId + "/" + guild.Avatar + ".png";
		guild.Url = "https://server-discord.com/" + guildId;
		if (fetch)
			guild.Instance = await m_clientConfig.Rest.GetGuildAsync(guildId);

		return guild;
	}

	public async Task<Dictionary<User, Rate>> GetGuildRates(ulong guildId, bool fetch = false)
	{
		var result = new Dictionary<User, Rate>();
		var rates = await GetGuildRates<Rates>(guildId);

		if (fetch)
		{
			foreach (var (k, v) in rates)
			{
				IUser? instance = null;

				try
				{
					instance = await m_clientConfig.Rest.GetUserAsync(k);
				}
				catch
				{
					// ignored
				}

				var user = new User(k, instance);
				result[user] = v;
			}

			return result;
		}

		foreach (var (k, v) in rates)
		{
			var user = new User(k);
			result[user] = v;
		}

		return result;
	}

	public async Task<Dictionary<RatedGuild, Rate>> GetUserRates(ulong userId, bool fetch = false)
	{
		var result = new Dictionary<RatedGuild, Rate>();
		var rates = await GetUserRates<Rates>(userId);
		if (fetch)
		{
			foreach (var (k, v) in rates)
				result[new RatedGuild(k, await m_clientConfig.Rest.GetGuildAsync(k))] = v;

			return result;
		}

		foreach (var (k, v) in rates)
			result[new RatedGuild(k)] = v;

		return result;
	}
}