namespace SDC_Sharp.DiscordNet.Interfaces;

public interface IClientConfig
{
	public uint GuildsCount { get; }
	public uint ShardsCount { get; }
	public IRest Rest { get; }
}