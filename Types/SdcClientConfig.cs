using SDC_Sharp.SDC_Sharp.Types;

namespace SDC_Sharp.DiscordNet.Types
{
    public class SdcClientConfig : ISdcClientConfig
    {
        private string _token;
        internal IDiscordClientWrapper _wrapper;

        public SDC_Sharp.Types.IDiscordClientWrapper Wrapper
        {
            get => _wrapper;
            set => _wrapper = value as IDiscordClientWrapper;
        }

        public virtual string Token
        {
            get => _token;
            set => _token = value.StartsWith("SDC ") ? value : $"SDC {value}";
        }
    }
}