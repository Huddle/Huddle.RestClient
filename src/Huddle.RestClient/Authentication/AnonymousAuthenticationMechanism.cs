namespace Huddle.Clients.Authentication
{
    public class AnonymousAuthenticationMechanism : IRequestAuthenticationMechanism
    {
        public string GetAuthenticationHeader()
        {
            return string.Empty;
        }
    }
}