using System.Threading.Tasks;
using SDC_Sharp.DiscordNet.Types;
using SDC_Sharp.SDC_Sharp;
using static SDC_Sharp.DiscordNet.SdcSharpExtensions;

namespace SDC_Sharp.DiscordNet.Services
{
    public class Blacklist
    {
        private SdcSharpClient _sdcClient;
        internal Blacklist(ref SdcSharpClient client) => _sdcClient = client;

        public async Task<BlacklistResponse> GetWarns(ulong id)
        {
            var res = await _sdcClient.GetRequest<BlacklistResponse>($"warns/{id}");
            res.SdcClient = _sdcClient;
            if (res.Id == 0)
                res.Id = id;

            return res;
        }
    }
}