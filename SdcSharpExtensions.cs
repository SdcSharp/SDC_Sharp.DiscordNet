using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using SDC_Sharp.DiscordNet.Services;

namespace SDC_Sharp
{
    public static class SdcSharpExtensions
    {
        internal static SdcSharpClient SdcClient;
        internal static BaseSocketClient Discord;
        internal static DiscordSocketRestClient DiscordRest => Discord.Rest;

        public static IServiceCollection AddSdcClient(this IServiceCollection serviceCollection,
            SdcSharpClient sdcSharpClient)
        {
            SdcClient = sdcSharpClient;

            serviceCollection.AddSingleton(sdcSharpClient);
            return serviceCollection;
        }

        public static async Task<SocketUser> GetUser(this IDiscordClientWrapper client, ulong id)
        {
            return (Discord.GetUser(id) ?? await DiscordRest.GetUserAsync(id) as IUser) as SocketUser;
        }

        public static async Task<SocketGuild> GetGuild(this IDiscordClientWrapper client, ulong id)
        {
            return (Discord.GetGuild(id) ?? await DiscordRest.GetGuildAsync(id) as IGuild) as SocketGuild;
        }

        public static Bots GetBots(this SdcSharpClient client)
        {
            return new Bots();
        }

        public static Monitoring GetMonitoring(this SdcSharpClient client)
        {
            return new Monitoring();
        }

        public static Blacklist GetBlacklist(this SdcSharpClient client)
        {
            return new Blacklist();
        }
    }
}