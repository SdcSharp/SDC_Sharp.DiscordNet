using System;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using SDC_Sharp.DiscordNet.Services;
using SDC_Sharp.DiscordNet.Types;

namespace SDC_Sharp.DiscordNet
{
    public sealed class SdcServices
    {
        internal readonly IClientConfig Client;

        public SdcServices(IServiceProvider services)
        {
            if (!(services.GetService<DiscordShardedClient>() is null))
                Client = new ShardedConfig(services.GetRequiredService<DiscordShardedClient>());
            else if (!(services.GetService<DiscordSocketClient>() is null))
                Client = new DefaultConfig(services.GetRequiredService<DiscordSocketClient>());
        }
    }

    public static class Extensions
    {
        public static IServiceCollection InitializeSdcServices(this IServiceCollection services)
        {
            services.AddSingleton<MonitoringService>();
            services.AddSingleton<BlacklistService>();
            services.AddSingleton<BotsService>();

            return services;
        }
    }
}