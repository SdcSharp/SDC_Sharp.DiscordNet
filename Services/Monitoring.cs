using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SDC_Sharp.DiscordNet.Types;
using SDC_Sharp.SDC_Sharp;
using SDC_Sharp.DiscordNet;
using static SDC_Sharp.DiscordNet.SdcSharpExtensions;

namespace SDC_Sharp.DiscordNet.Services
{
    public class Monitoring
    {
        private SdcSharpClient _sdcClient;
        internal Monitoring(ref SdcSharpClient client) => _sdcClient = client;

        public async Task<GuildInfo> GetGuild(ulong guildId)
        {
            var res = await _sdcClient.GetRequest<GuildInfo>($"guild/{guildId}");
            res.Id = guildId;

            return res;
        }

        public async Task<GuildPlace> GetGuildPlace(ulong guildId)
        {
            var res = await _sdcClient.GetRequest<GuildPlace>($"guild/{guildId}/place");
            res.Id = guildId;

            return res;
        }

        public async Task<UserRatedServers> GetUserRatedServers(ulong userId, bool fetch = false)
        {
            var raw = await _sdcClient.GetRequest<Dictionary<string, byte>>($"user/{userId}/rated");
            var list = new LinkedList<UserRate>();

            await Task.Run(async () =>
            {
                foreach (var (id, rate) in raw)
                {
                    list.AddLast(new UserRate
                    {
                        Id = ulong.Parse(id),
                        Rate = rate,
                        Guild = !fetch ? null : await _sdcClient.Wrapper.GetGuild(ulong.Parse(id))
                    });
                }
            });

            return new UserRatedServers
            {
                RatedServersList = list.ToArray()
            };
        }

        public async Task<GuildRatedUsers> GetGuildRated(ulong guildId, bool fetch = false)
        {
            var raw = await _sdcClient.GetRequest<Dictionary<string, byte>>($"guild/{guildId}/rated");
            var list = new LinkedList<UserRate>();

            await Task.Run(async () =>
            {
                foreach (var (id, rate) in raw)
                {
                    list.AddLast(new UserRate
                    {
                        Id = ulong.Parse(id),
                        Rate = rate,
                        User = !fetch ? null : await _sdcClient.Wrapper.GetUser(ulong.Parse(id))
                    });
                }
            });

            return new GuildRatedUsers
            {
                RatedUsersList = list.ToArray()
            };
        }
    }
}