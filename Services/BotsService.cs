using System;
using System.Threading.Tasks;
using SDC_Sharp.DiscordNet.Types;
using SDC_Sharp.Services;
using SDC_Sharp.Types.Bots;

namespace SDC_Sharp.DiscordNet.Services
{
    public class BotsService : BaseBotsService
    {
        private readonly IClientConfig _clientConfig;

        public BotsService(SdcSharpClient client, SdcServices sdcServices) : base(client)
        {
            _clientConfig = sdcServices.Client;
        }

        public void AutoPostStats(TimeSpan interval, bool logging = false)
        {
            AutoPostStats(
                interval,
                _clientConfig.Rest.CurrentUser.Id,
                _clientConfig.ShardsCount,
                _clientConfig.GuildsCount,
                logging);
        }

        public async Task<StatsResponse> PostStats()
        {
            return await PostStats<StatsResponse>(
                _clientConfig.Rest.CurrentUser.Id,
                _clientConfig.ShardsCount,
                _clientConfig.GuildsCount);
        }
    }
}