using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using SDC_Sharp.DiscordNet.Services;
using SDC_Sharp.DiscordNet.Types;
using SDC_Sharp.SDC_Sharp;
using IDiscordClientWrapper = SDC_Sharp.SDC_Sharp.Types.IDiscordClientWrapper;

namespace SDC_Sharp.DiscordNet
{
    public static class SdcSharpExtensions
    {
        private static SdcSharpClient _sdcClient;
        
        private static Bots _bots;
        private static Monitoring _monitoring;
        private static Blacklist _blacklist;
        
        private static BaseSocketClient _discord;
        private static DiscordSocketRestClient DiscordRest => _discord.Rest;
        

        public static Bots GetBots(this SdcSharpClient client) => _bots;
        public static Monitoring GetMonitoring(this SdcSharpClient client) => _monitoring;
        public static Blacklist GetBlacklist(this SdcSharpClient client) => _blacklist;
        
        public static IServiceCollection AddSdcClient(this IServiceCollection serviceCollection,
            SdcClientConfig config)
        {
            _discord = config._wrapper.Client;
            _sdcClient = new SdcSharpClient(config);

            _bots = new Bots(ref _sdcClient);
            _monitoring = new Monitoring(ref _sdcClient);
            _blacklist = new Blacklist(ref _sdcClient);

            serviceCollection.AddSingleton(_sdcClient);
            return serviceCollection;
        }

        public static async Task<SocketUser> GetUser(this IDiscordClientWrapper client, ulong id)
        {
            return (_discord.GetUser(id) ?? await DiscordRest.GetUserAsync(id) as IUser) as SocketUser;
        }

        public static async Task<SocketGuild> GetGuild(this IDiscordClientWrapper client, ulong id)
        {
            return (_discord.GetGuild(id) ?? await DiscordRest.GetGuildAsync(id) as IGuild) as SocketGuild;
        }
    }
}