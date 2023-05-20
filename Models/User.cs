#nullable enable
using Discord;

namespace SDC_Sharp.DiscordNet.Models;

public record struct User(ulong Id, IUser? Instance = null)
{
	public IUser? Instance { get; init; } = Instance;
	public ulong Id { get; init; } = Id;
}