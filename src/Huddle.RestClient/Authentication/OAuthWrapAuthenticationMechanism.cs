namespace Huddle.Clients.Authentication
{
    public class OAuthWrapAuthenticationMechanism : IRequestAuthenticationMechanism
    {
        private readonly string _token;

        public OAuthWrapAuthenticationMechanism(string token)
        {
            const string headerPrefix = "WRAP access_token=\"";

            if(token.StartsWith(headerPrefix))
            {
                token = token.Substring(headerPrefix.Length, token.Length - headerPrefix.Length).TrimEnd('\"');
            }
            
            _token = token;
        }

        public string GetAuthenticationHeader()
        {
            return string.Format("WRAP access_token=\"{0}\"", _token);
        }
    }
}