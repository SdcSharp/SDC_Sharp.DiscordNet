using System.Threading.Tasks;
using SDC_Sharp.DiscordNet.Types;
using SDC_Sharp.Services;

namespace SDC_Sharp.DiscordNet.Services
{
    public class BlacklistService : BaseBlacklistService
    {
        private readonly IClientConfig _clientConfig;

        public BlacklistService(SdcSharpClient client, SdcServices sdcServices) : base(client)
        {
            _clientConfig = sdcServices.Client;
        }

        public async Task<UserWarns> GetWarns(ulong userId, bool fetch = false)
        {
            var warns = await GetWarns<UserWarns>(userId);
            if (fetch)
                warns.User = await _clientConfig.Rest.GetUserAsync(warns.Id);
            return warns;
        }
    }
}