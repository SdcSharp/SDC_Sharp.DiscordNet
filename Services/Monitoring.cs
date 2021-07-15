using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SDC_Sharp.DiscordNet.Types;
using static SDC_Sharp.SdcSharpExtensions;

namespace SDC_Sharp.DiscordNet.Services
{
    public class Monitoring
    {
        internal Monitoring()
        {
        }

        public async Task<GuildInfo> GetGuild(ulong guildId)
        {
            var res = await SdcClient.GetRequest<GuildInfo>($"guild/{guildId}");
            res.id = guildId;

            return res;
        }

        public async Task<GuildPlace> GetGuildPlace(ulong guildId)
        {
            var res = await SdcClient.GetRequest<GuildPlace>($"guild/{guildId}/place");
            res.id = guildId;

            return res;
        }

        public async Task<UserRatedServers> GetUserRatedServers(ulong userId, bool fetch = false)
        {
            var raw = await SdcClient.GetRequest<Dictionary<string, byte>>($"user/{userId}/rated");
            var list = new LinkedList<UserRate>();

            foreach (var (id, rate) in raw)
                list.AddLast(new UserRate
                {
                    id = ulong.Parse(id),
                    rate = rate,
                    guild = !fetch ? null : await SdcClient.Wrapper.GetGuild(ulong.Parse(id))
                });

            return new UserRatedServers
            {
                RatedServersList = list.ToArray()
            };
        }

        public async Task<GuildRatedUsers> GetGuildRated(ulong guildId, bool fetch = false)
        {
            var raw = await SdcClient.GetRequest<Dictionary<string, byte>>($"guild/{guildId}/rated");
            var list = new LinkedList<UserRate>();

            foreach (var (id, rate) in raw)
                list.AddLast(new UserRate
                {
                    id = ulong.Parse(id),
                    rate = rate,
                    user = !fetch ? null : await SdcClient.Wrapper.GetUser(ulong.Parse(id))
                });

            return new GuildRatedUsers
            {
                RatedUsersList = list.ToArray()
            };
        }
    }
}