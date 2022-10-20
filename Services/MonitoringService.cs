using System.Collections.Generic;
using System.Threading.Tasks;
using SDC_Sharp.DiscordNet.Types;
using SDC_Sharp.Services;
using SDC_Sharp.Types.Enums;
using SDC_Sharp.Types.Monitoring;
using Rates = System.Collections.Generic.Dictionary<ulong, SDC_Sharp.Types.Enums.Rate>;

namespace SDC_Sharp.DiscordNet.Services
{
	public class MonitoringService : BaseMonitoringService
	{
		private readonly IClientConfig m_clientConfig;

		public MonitoringService(SdcSharpClient client, SdcServices sdcServices) : base(client)
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
			User user;
			var result = new Dictionary<User, Rate>();
			var rates = await GetGuildRates<Rates>(guildId);

			foreach (var (k, v) in rates)
			{
				user = fetch ? new User(k, await m_clientConfig.Rest.GetUserAsync(k)) : new User(k);
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
}