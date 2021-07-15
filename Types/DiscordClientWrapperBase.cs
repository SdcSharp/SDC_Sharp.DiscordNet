namespace SDC_Sharp.DiscordNet.Types
{
    public abstract class DiscordClientWrapperBase : IDiscordClientWrapper
    {
        public abstract int ShardCount { get;  }
        public abstract int ServersCount { get; }
        public abstract ulong CurrentUserId { get; }
    }
}