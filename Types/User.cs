#nullable enable
using Discord;

namespace SDC_Sharp.DiscordNet.Types
{
	public struct User
	{
		public ulong Id { get; set; }
		public IUser? Instance { get; set; }

		public User(ulong id, IUser? instance = null)
		{
			Id = id;
			Instance = instance;
		}
	}
}