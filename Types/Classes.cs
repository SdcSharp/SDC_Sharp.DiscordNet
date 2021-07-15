using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;
using static SDC_Sharp.SdcSharpExtensions;

namespace SDC_Sharp.DiscordNet.Types
{
    public class BotsResponse
    {
        public bool status { get; set; }
        public BotResponseError error { get; set; }

        public override string ToString()
        {
            return
                $"{GetType()}: " + (error == null
                    ? "{ " +
                      $"status: {(!status ? "null" : true.ToString())}, " +
                      " }"
                    : error.ToString());
        }
    }

    public class BotResponseError
    {
        public string msg { get; set; }
        public string type { get; set; }
        public int code { get; set; }

        public Embed ToEmbed()
        {
            var embed = new EmbedBuilder();
            embed.AddField("Type: ", $"`{type}`", true);
            embed.AddField("Code: ", $"`{code}`", true);
            embed.AddField("Message: ", $"`{msg}`", true);

            return embed.Build();
        }

        public override string ToString()
        {
            return $"{GetType()}: " +
                   "{ " +
                   $"Type: {type}, " +
                   $"Code: {code.ToString()}, " +
                   $"Message: {msg}" +
                   " }";
        }
    }

    public class BlacklistResponse
    {
        public BotResponseError error { get; set; }
        public ulong id { get; set; }
        public string type { get; set; }
        public sbyte warns { get; set; }

        public override string ToString()
        {
            return $"{GetType()}: " + (error == null
                ? "{ " +
                  $"id: {id.ToString()}, " +
                  $"type: {type}, " +
                  $"warns: {warns.ToString()}" +
                  " }"
                : error.ToString());
        }

        public async Task<Embed> ToEmbed()
        {
            var embed = new EmbedBuilder();
            embed.Title = "Варны";
            embed.Description = "\n";

            if (id != 0)
                try
                {
                    await SdcClient.RateLimiter();
                    embed.Title += " пользователя";
                    embed.Description += $"{await SdcClient.Wrapper.GetUser(id)}\n";
                }
                catch
                {
                }

            embed.Description += warns;

            return embed.Build();
        }
    }

    public class UserRatedServers
    {
        public BotResponseError error { get; set; }
        public UserRate[] RatedServersList { get; set; }
    }

    public class UserRate
    {
        public ulong id { get; set; }
        public byte rate { get; set; }

        public SocketUser user { get; set; }
        public SocketGuild guild { get; set; }
    }

    public class GuildPlace
    {
        public BotResponseError error { get; set; }

        public ulong id { get; set; }
        public uint place { get; set; }
    }

    public class GuildRatedUsers
    {
        public BotResponseError error { get; set; }
        public UserRate[] RatedUsersList { get; set; }
    }

    public class GuildInfo
    {
        public BotResponseError error { get; set; }

        public string avatar { get; set; }
        public string lang { get; set; }
        public string name { get; set; }
        public string des { get; set; }
        public string invite { get; set; }
        public string owner { get; set; }
        public string tags { get; set; }

        public ulong id { get; set; }
        public ulong upCount { get; set; }

        public uint online { get; set; }
        public uint members { get; set; }

        public byte bot { get; set; }

        public BoostLevel boost { get; set; }
        public BadgesEnum status { get; set; }

        public Embed ToEmbed()
        {
            var embed = new EmbedBuilder();

            embed.Description = des;
            embed.Author = new EmbedAuthorBuilder
            {
                Name = name,
                Url = $"https://server-discord.com/{id}",
                IconUrl = $"https://cdn.discordapp.com/icons/{id}/{avatar}.png"
            };
            embed.Footer = new EmbedFooterBuilder
            {
                Text = owner
            };

            embed.AddField("Ап-очки: ", $"[`{upCount}`](https://server-discord.com/faq)");
            embed.AddField("Уровень буста: ", $"[`BOOST {boost.ToString()}`](https://server-discord.com/boost)");
            embed.AddField("Бейдж: ", $"[`{status.ToString()}`](https://server-discord.com/faq)");
            embed.AddField("Теги:",
                tags.Length != 0 && tags.Split(",").Length >= 2 ? $"`{string.Join("`, `", tags.Split(","))}`" :
                tags.Length != 0 ? $"`{tags}`" : "не указаны");
            embed.AddField("Онлайн:", $"`{online}`", true);
            return embed.Build();
        }

        public override string ToString()
        {
            return $"{GetType()}: " + (error == null
                ? "{ " +
                  $"avatar: \"{avatar}\", " +
                  $"lang: \"{lang}\", " +
                  $"name: \"{name}\", " +
                  $"des: \"{des}\", " +
                  $"invite: \"{invite}\", " +
                  $"owner: \"{owner}\", " +
                  $"tags: {tags}, " +
                  $"id: {id.ToString()}, " +
                  $"upCount: {upCount.ToString()}, " +
                  $"online: {online.ToString()}, " +
                  $"members: {members.ToString()}, " +
                  $"bot: {bot.ToString()}, " +
                  $"boost: {((int) boost).ToString()}, " +
                  $"status: {((int) status).ToString()}, " +
                  " }"
                : error.ToString());
        }
    }

    public enum BoostLevel
    {
        none = 0,
        light = 1,
        pro = 2,
        max = 3
    }

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