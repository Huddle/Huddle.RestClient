namespace Huddle.Clients.Authentication
{
    public interface IRequestAuthenticationMechanism
    {
        string GetAuthenticationHeader();
    }
}