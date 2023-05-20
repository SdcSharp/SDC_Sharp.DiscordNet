using System;
using System.Threading;
using System.Threading.Tasks;
using SDC_Sharp.DiscordNet.Interfaces;
using SDC_Sharp.Services;
using SDC_Sharp.Types.Bots;

namespace SDC_Sharp.DiscordNet.Services;

public sealed class BotsService : BaseBotsService
{
	private readonly IClientConfig m_clientConfig;

	public BotsService(ISdcSharpClient client, ISdcServices sdcServices) : base(client)
	{
		m_clientConfig = sdcServices.Client;
	}

		public void AutoPostStats(TimeSpan interval, bool logging = false,
			CancellationToken cancellationToken = default)
		{
			AutoPostStats(
				interval,
				m_clientConfig.Rest.CurrentUser.Id,
				m_clientConfig.ShardsCount,
				m_clientConfig.GuildsCount,
				logging,
				cancellationToken);
		}

		public async Task<StatsResponse> PostStats()
		{
			return await PostStats<StatsResponse>(
				m_clientConfig.Rest.CurrentUser.Id,
				m_clientConfig.ShardsCount,
				m_clientConfig.GuildsCount);
		}
	}
}