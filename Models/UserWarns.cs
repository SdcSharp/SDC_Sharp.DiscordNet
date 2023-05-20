#nullable enable
using Discord;
using SDC_Sharp.Types.Blacklist;

namespace SDC_Sharp.DiscordNet.Models;

public sealed record UserWarns(ulong Id, sbyte Warns, string Type = "user", IUser? User = null)
	: BaseUserWarns(Id, Warns, Type)
{
	public IUser? User { get; internal set; } = User;
}