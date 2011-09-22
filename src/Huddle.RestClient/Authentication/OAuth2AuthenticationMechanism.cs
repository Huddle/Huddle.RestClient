namespace Huddle.Clients.Authentication
{
    public class OAuth2AuthenticationMechanism : IRequestAuthenticationMechanism
    {
        private readonly string _token;
        const string _headerPrefix = "OAuth2";

        public OAuth2AuthenticationMechanism(string token)
        {
            if(token.StartsWith(_headerPrefix))
            {
                token = token.Remove(0, _headerPrefix.Length + 1);
            }
            
            _token = token;
        }

        public string GetAuthenticationHeader()
        {
            return string.Format("{0} {1}", _headerPrefix, _token);
        }
    }
}