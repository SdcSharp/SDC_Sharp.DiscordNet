#nullable enable
using Discord;
using SDC_Sharp.Types.Blacklist;

namespace SDC_Sharp.DiscordNet.Types
{
    public sealed class UserWarns : BaseUserWarns
    {
        public IUser? User { get; internal set; }
    }
}