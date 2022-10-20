#nullable enable
using Discord;
using SDC_Sharp.Types.Monitoring;

namespace SDC_Sharp.DiscordNet.Types
{
	public sealed class Guild : BaseGuild
	{
		public ulong Id { get; set; }

		public string Url { get; set; } = default!;

		public IGuild? Instance { get; set; }
	}
}