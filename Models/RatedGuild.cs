#nullable enable
using Discord;

namespace SDC_Sharp.DiscordNet.Models;

public record struct RatedGuild(ulong Id, IGuild? Instance = null)
{
	public ulong Id { get; init; } = Id;
	public IGuild? Instance { get; init; } = Instance;
}