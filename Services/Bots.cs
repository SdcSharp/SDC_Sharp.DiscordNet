using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SDC_Sharp.DiscordNet.Types;
using SDC_Sharp.SDC_Sharp;

namespace SDC_Sharp.DiscordNet.Services
{
    public class Bots
    {
        private SdcSharpClient _sdcClient;
        internal Bots(ref SdcSharpClient client) => _sdcClient = client;

        public async Task<BotsResponse> UpdateStats(int serversCount, int shardsCount, ulong clientId)
        {
            await _sdcClient.RateLimiter();

            return await _sdcClient.PostRequest<BotsResponse>(
                $"bots/{(clientId != 0 ? clientId : _sdcClient.Wrapper.CurrentUserId)}/stats",
                new StringContent(JsonConvert.SerializeObject(new Dictionary<string, int>
                {
                    {
                        "shardsCount",
                        shardsCount != 0
                            ? shardsCount
                            : _sdcClient.Wrapper.ShardCount
                    },
                    {
                        "serversCount",
                        serversCount != 0
                            ? serversCount
                            : _sdcClient.Wrapper.ServersCount
                    }
                }), Encoding.UTF8, "application/json"));
        }

        public async Task AutoUpdateStats(
            int? serversCount = null,
            int? shardsCount = null,
            ulong? clientId = null,
            TimeSpan timeout = default)
        {
            timeout = timeout == default
                ? _sdcClient.DefaultTimeout
                : timeout >= TimeSpan.FromMinutes(30)
                    ? timeout
                    : TimeSpan.FromMinutes(30);

            while (true)
            {
                try
                {
                    var resp = await UpdateStats(
                        serversCount ?? _sdcClient.Wrapper.ServersCount,
                        shardsCount ?? _sdcClient.Wrapper.ShardCount,
                        clientId ?? _sdcClient.Wrapper.CurrentUserId);

                    Console.WriteLine(resp);

                    await Task.Delay(timeout);
                }
                catch
                {
                }
            }
        }

        public void AutoUpdateStatsThreaded(
            int? serversCount = null,
            int? shardsCount = null,
            ulong? clientId = null,
            TimeSpan timeout = default)
        {
            ThreadPool.QueueUserWorkItem((object state) => Task.Run(async () => await AutoUpdateStats(serversCount, shardsCount, clientId, timeout)));
        }
    }
}