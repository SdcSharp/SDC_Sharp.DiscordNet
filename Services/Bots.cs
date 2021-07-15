using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SDC_Sharp.DiscordNet.Types;
using static SDC_Sharp.SdcSharpExtensions;

namespace SDC_Sharp.DiscordNet.Services
{
    public class Bots
    {
        internal Bots()
        {
        }

        public async Task<BotsResponse> UpdateStats(int serversCount, int shardsCount, ulong clientId)
        {
            await SdcClient.RateLimiter();

            return await SdcClient.PostRequest<BotsResponse>(
                $"bots/{(clientId != 0 ? clientId : SdcClient.Wrapper.CurrentUserId)}/stats",
                new StringContent(JsonConvert.SerializeObject(new Dictionary<string, int>
                {
                    {
                        "shardsCount",
                        shardsCount != 0
                            ? shardsCount
                            : SdcClient.Wrapper.ShardCount
                    },
                    {
                        "serversCount",
                        serversCount != 0
                            ? serversCount
                            : SdcClient.Wrapper.ServersCount
                    }
                }), Encoding.UTF8, "application/json"));
        }

        public async Task AutoUpdateStats(int? serversCount, int? shardsCount, ulong? clientId,
            TimeSpan timeout = default)
        {
            timeout = timeout == default
                ? SdcClient.DefaultTimeout
                : timeout >= TimeSpan.FromMinutes(30)
                    ? timeout
                    : TimeSpan.FromMinutes(30);

            while (true)
                try
                {
                    await UpdateStats(
                        serversCount ?? SdcClient.Wrapper.ServersCount,
                        shardsCount ?? SdcClient.Wrapper.ShardCount,
                        clientId ?? SdcClient.Wrapper.CurrentUserId);
                    await Task.Delay(timeout);
                }
                catch
                {
                    return;
                }
        }
    }
}