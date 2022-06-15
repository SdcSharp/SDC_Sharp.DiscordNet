#nullable enable
using Discord;

namespace SDC_Sharp.DiscordNet.Types
{
    public struct RatedGuild
    {
        public ulong Id { get; set; }
        public IGuild? Instance { get; set; }

        public RatedGuild(ulong id, IGuild? instance = null)
        {
            Id = id;
            Instance = instance;
        }
    }
}