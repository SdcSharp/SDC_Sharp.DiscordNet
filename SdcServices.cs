using Discord;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using SDC_Sharp.DiscordNet.Interfaces;
using SDC_Sharp.DiscordNet.Services;
using SDC_Sharp.DiscordNet.Types;

namespace SDC_Sharp.DiscordNet;

public sealed class SdcServices
{
	public readonly IClientConfig Client;

	public SdcServices(IClientConfig config)
	{
		Client = config;
	}

	public SdcServices(IDiscordClient client)
	{
		Client = client switch
		{
			DiscordSocketClient socketClient => new DefaultConfig(socketClient),
			DiscordShardedClient shardedClient => new ShardedConfig(shardedClient),
			_ => null
		};
	}

	public SdcServices(DiscordSocketClient socketClient) : this(socketClient as IDiscordClient)
	{
	}

	public SdcServices(DiscordShardedClient shardedClient) : this(shardedClient as IDiscordClient)
	{
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