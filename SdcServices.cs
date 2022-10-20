using Discord;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using SDC_Sharp.DiscordNet.Services;
using SDC_Sharp.DiscordNet.Types;

namespace SDC_Sharp.DiscordNet
{
	public sealed class SdcServices
	{
		internal readonly IClientConfig Client;

		public SdcServices(IDiscordClient client)
		{
			if (client is DiscordSocketClient socketClient)
				Client = new DefaultConfig(socketClient);
			else if (client is DiscordShardedClient shardedClient)
				Client = new ShardedConfig(shardedClient);
		}

		public SdcServices(DiscordSocketClient socketClient)
		{
			Client = new DefaultConfig(socketClient);
		}

		public SdcServices(DiscordShardedClient shardedClient)
		{
			Client = new ShardedConfig(shardedClient);
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