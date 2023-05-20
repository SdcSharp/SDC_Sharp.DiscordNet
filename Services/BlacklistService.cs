using System.Threading.Tasks;
using SDC_Sharp.DiscordNet.Interfaces;
using SDC_Sharp.DiscordNet.Models;
using SDC_Sharp.Services;

namespace SDC_Sharp.DiscordNet.Services;

public sealed class BlacklistService : BaseBlacklistService
{
	private readonly IClientConfig m_clientConfig;

	public BlacklistService(ISdcSharpClient client, ISdcServices sdcServices) : base(client)
	{
		m_clientConfig = sdcServices.Client;
	}

	public async Task<UserWarns> GetWarns(ulong userId, bool fetch = false)
	{
		var warns = await GetWarns<UserWarns>(userId);
		if (fetch)
			warns.User = await m_clientConfig.Rest.GetUserAsync(warns.Id);
		return warns;
	}
}