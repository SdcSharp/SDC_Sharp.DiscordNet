using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;
using SDC_Sharp.SDC_Sharp;

namespace SDC_Sharp.DiscordNet.Types
{
    public class BotResponseError
    {
        public string Msg { get; set; }
        public string Type { get; set; }
        public int Code { get; set; }

        public Embed ToEmbed()
        {
            var embed = new EmbedBuilder();
            embed.AddField("Type: ", $"`{Type}`", true);
            embed.AddField("Code: ", $"`{Code}`", true);
            embed.AddField("Message: ", $"`{Msg}`", true);

            return embed.Build();
        }

        public override string ToString()
        {
            return $"{GetType()}: " +
                   "{ " +
                   $"Type: {Type}, " +
                   $"Code: {Code.ToString()}, " +
                   $"Message: {Msg}" +
                   " }";
        }
    }
    
    public struct BotsResponse
    {
        public bool Status { get; set; }
        public BotResponseError Error { get; set; }

        public override string ToString()
        {
            return
                $"{GetType()}: " + (Error == null
                    ? "{ " +
                      $"status: {(!Status ? "null" : Status.ToString())}, " +
                      " }"
                    : Error.ToString());
        }
    }

    public struct BlacklistResponse
    {
        internal SdcSharpClient SdcClient;
        
        public BotResponseError Error { get; set; }
        public ulong Id { get; set; }
        public string Type { get; set; }
        public sbyte Warns { get; set; }

        public override string ToString()
        {
            return $"{GetType()}: " + (Error == null
                ? "{ " +
                  $"id: {Id.ToString()}, " +
                  $"type: {Type}, " +
                  $"warns: {Warns.ToString()}" +
                  " }"
                : Error.ToString());
        }

        public async Task<Embed> ToEmbed()
        {
            var embed = new EmbedBuilder();
            embed.Title = "Варны";
            embed.Description = "\n";

            if (Id != 0)
                try
                {
                    await SdcClient.RateLimiter();
                    embed.Title += " пользователя";
                    embed.Description += $"{await SdcClient.Wrapper.GetUser(Id)}\n";
                }
                catch
                {
                }

            embed.Description += Warns;

            return embed.Build();
        }
    }

    public struct UserRatedServers
    {
        public BotResponseError Error { get; set; }
        public UserRate[] RatedServersList { get; set; }
        
        public override string ToString()
        {
            return $"{GetType()}: " +
                   (Error == null
                       ? "{ " +
                         (RatedServersList != null && RatedServersList.Length != 0
                             ? (RatedServersList.Length > 1
                                 ? $"[{string.Join(", ", RatedServersList.Select(x => x.ToString()))}]"
                                 : $"[{RatedServersList.First()}]")
                             : "[]") +
                         " }"
                       : Error.ToString());
        }
    }

    public struct UserRate
    {
        public ulong Id { get; set; }
        public byte Rate { get; set; }

        public SocketUser User { get; set; }
        public SocketGuild Guild { get; set; }

        public override string ToString()
        {
            return $"\"{Id}\": {Rate}";
        }
    }

    public struct GuildPlace
    {
        public BotResponseError Error { get; set; }

        public ulong Id { get; set; }
        public uint Place { get; set; }
        
        public override string ToString()
        {
            return $"{GetType()}: " +
                   (Error == null
                       ? "{ " +
                         $"place: {Place}" +
                         " }"
                       : Error.ToString());
        }
    }

    public struct GuildRatedUsers
    {
        public BotResponseError Error { get; set; }
        public UserRate[] RatedUsersList { get; set; }
        
        public override string ToString()
        {
            return $"{GetType()}: " +
                   (Error == null
                       ? "{ " +
                         (RatedUsersList != null && RatedUsersList.Length != 0
                             ? (RatedUsersList.Length > 1
                                 ? $"[{string.Join(", ", RatedUsersList.Select(x => x.ToString()))}]"
                                 : $"[{RatedUsersList.First()}]")
                             : "[]") +
                         " }"
                       : Error.ToString());
        }
    }

    public struct GuildInfo
    {
        public BotResponseError Error { get; set; }

        public string Avatar { get; set; }
        public string Lang { get; set; }
        public string Name { get; set; }
        public string Des { get; set; }
        public string Invite { get; set; }
        public string Owner { get; set; }
        public string Tags { get; set; }

        public ulong Id { get; set; }
        public ulong UpCount { get; set; }

        public uint Online { get; set; }
        public uint Members { get; set; }

        public byte Bot { get; set; }

        public BoostLevelEnum Boost { get; set; }
        public BadgesEnum Status { get; set; }

        private BadgesEnum[] _bages;
        public BadgesEnum[] Bages
        {
            get
            {
                if (_bages == null || _bages.Length < 1)
                    _bages = GetBadgesEnums(Status).GetAwaiter().GetResult();

                return _bages;
            }

            set
            {
                if (value.Length >= 1)
                    _bages = value;
                else
                    _bages = Array.Empty<BadgesEnum>();
            }
        }

        private static Task<BadgesEnum[]> GetBadgesEnums(BadgesEnum bage)
        {
            var res = new LinkedList<BadgesEnum>();

            return Task.Run(() =>
            {
                for (var i = 1; i <= 0x200; i *= 2)
                {
                    if (Enum.TryParse<BadgesEnum>(i.ToString(), out var status) && (bage & status) == status)
                        res.AddLast(bage);
                }
                
                return res.ToArray();
            });
        }

        public EmbedBuilder ToEmbed()
        {
            var embed = new EmbedBuilder();

            embed.Description = Des;
            embed.Author = new EmbedAuthorBuilder
            {
                Name = Name,
                Url = $"https://server-discord.com/{Id}",
                IconUrl = $"https://cdn.discordapp.com/icons/{Id}/{Avatar}.png"
            };
            embed.Footer = new EmbedFooterBuilder
            {
                Text = Owner
            };

            embed.AddField("Ап-очки: ", $"[`{UpCount}`](https://server-discord.com/faq)");
            embed.AddField("Уровень буста: ", $"[`BOOST {Boost.ToString()}`](https://server-discord.com/boost)");
            embed.AddField("Бейдж: ", $"[`{(Bages.Length != 0 ? (Bages.Length > 1 ? string.Join(", ", Bages) : Bages.First().ToString()) : "нет")}`](https://server-discord.com/faq)");
            embed.AddField("Теги:",
                Tags.Length != 0 && Tags.Split(",").Length >= 2 ? $"`{string.Join("`, `", Tags.Split(","))}`" :
                Tags.Length != 0 ? $"`{Tags}`" : "не указаны");
            embed.AddField("Онлайн:", $"`{Online}`", true);
            return embed;
        }

        public override string ToString()
        {
            return $"{GetType()}: " + (Error == null
                ? "{ " +
                  $"avatar: \"{Avatar}\", " +
                  $"lang: \"{Lang}\", " +
                  $"name: \"{Name}\", " +
                  $"des: \"{Des}\", " +
                  $"invite: \"{Invite}\", " +
                  $"owner: \"{Owner}\", " +
                  $"tags: {Tags}, " +
                  $"id: {Id.ToString()}, " +
                  $"upCount: {UpCount.ToString()}, " +
                  $"online: {Online.ToString()}, " +
                  $"members: {Members.ToString()}, " +
                  $"bot: {Bot.ToString()}, " +
                  $"boost: {((int) Boost).ToString()}, " +
                  $"status: {((int) Status).ToString()}, " +
                  " }"
                : Error.ToString());
        }
    }
    
    [Flags]
    public enum BoostLevelEnum
    {
        none = 0,
        light = 1,
        pro = 2,
        max = 3
    }

    [Flags]
    public enum BadgesEnum
    {
        sitedev = 0x1,
        verefied = 0x2,
        partner = 0x4,
        favorite = 0x8,
        bughunter = 0x10,
        easteregg = 0x20,
        botdev = 0x40,
        youtube = 0x80,
        twitch = 0x100,
        spamhunt = 0x200
    }
}